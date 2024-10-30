using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TuNamespace;

namespace AceStreamPlayer
{
    public partial class MainWindow : Window
    {
        private List<Channel> channels = new List<Channel>();
        private List<Channel> filteredChannels = new List<Channel>(); // Nueva lista para los canales filtrados
        private const string ACE_BASE_URL = "http://127.0.0.1:6878/ace/getstream?id=";
        private const string CHANNELS_URL = "https://raw.githubusercontent.com/yalerooo/listaparatv/refs/heads/main/lista2";
        private readonly HttpClient client = new HttpClient();

        private string vlcPath = @"C:\Program Files\VideoLAN\VLC\vlc.exe";
        public MainWindow()
        {
            InitializeComponent();
            CheckAceStream();
            LoadChannelsFromUrl();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(vlcPath);
            if (settingsWindow.ShowDialog() == true)
            {
                vlcPath = settingsWindow.VlcPath; // Actualizar la ruta de VLC
            }
        }

        private async void LoadChannelsFromUrl()
        {
            try
            {
                string content = await client.GetStringAsync(CHANNELS_URL);
                ParseChannels(content);
                filteredChannels = new List<Channel>(channels);
                RefreshChannelList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar canales: {ex.Message}");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            filteredChannels = channels
                .Where(channel => channel.Name.ToLower().Contains(searchText))
                .ToList();
            RefreshChannelList();
        }

        private void QuestionIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.ShowDialog(); // Abre la ventana de información como un diálogo
        }




        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                filteredChannels = channels
                    .Where(channel => channel.Name.ToLower().Contains(searchText.ToLower()))
                    .ToList();
                RefreshChannelList();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un texto para buscar.");
            }
        }

        private void ParseChannels(string m3uContent)
        {
            channels.Clear();
            var lines = m3uContent.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string name = null, aceId = null, imageUrl = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    // Extrae el nombre del canal
                    var nameMatch = Regex.Match(line, @"tvg-id=""[^""]*"",\s*(.*)");
                    if (nameMatch.Success)
                    {
                        name = nameMatch.Groups[1].Value.Trim();
                    }

                    // Extrae la URL de la imagen
                    var logoMatch = Regex.Match(line, @"tvg-logo=""([^""]*)""");
                    if (logoMatch.Success)
                    {
                        imageUrl = logoMatch.Groups[1].Value.Trim();
                    }
                }
                else if (line.StartsWith("http"))
                {
                    // Extrae el AceId desde la URL
                    var aceIdMatch = Regex.Match(line, @"id=([a-fA-F0-9]+)");
                    if (aceIdMatch.Success)
                    {
                        aceId = aceIdMatch.Groups[1].Value;
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(aceId))
                        {
                            channels.Add(new Channel { Name = name, AceId = aceId, ImageUrl = imageUrl });
                            name = aceId = imageUrl = null;
                        }
                    }
                }
            }
        }


        private void RefreshChannelList()
        {
            channelsPanel.Children.Clear();
            foreach (var channel in filteredChannels)
            {
                AddChannelToPanel(channel);
            }
        }

        private void AddChannelToPanel(Channel channel)
        {
            // Verificar que la URL de la imagen sea válida
            if (string.IsNullOrEmpty(channel.ImageUrl))
            {
                // Puedes establecer una imagen por defecto o manejar este caso de otra manera
                channel.ImageUrl = "https://static.thenounproject.com/png/4595376-200.png"; // Cambia esto por una ruta válida
            }

            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var image = new Image
            {
                Width = 150, // Ancho del cuadrado
                Height = 150, // Alto del cuadrado
                Margin = new Thickness(5),
                Source = new BitmapImage(new Uri(channel.ImageUrl, UriKind.Absolute)),
                Cursor = Cursors.Hand, // Cambia el cursor a una mano al pasar por encima
                Stretch = Stretch.Fill // Estira la imagen para llenar el área asignada
            };

            // Establecer el recorte de la imagen para que tenga esquinas redondeadas
            var geometry = new RectangleGeometry(new Rect(0, 0, image.Width, image.Height), 20, 20); // Ajusta el radio para redondear las esquinas
            image.Clip = geometry;

            image.MouseDown += (s, e) => PlayInVLC(channel.AceId);

            var textBlock = new TextBlock
            {
                Text = channel.Name,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(5),
                FontSize = 14, // Establece un tamaño de fuente fijo
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap, // Permite que el texto se ajuste en varias líneas
                MaxWidth = 150 // Establece un ancho máximo para controlar el ajuste
            };

            stackPanel.Children.Add(image); // Añadir la imagen directamente al panel
            stackPanel.Children.Add(textBlock);
            channelsPanel.Children.Add(stackPanel);
        }





        private void CheckAceStream()
        {
            try
            {
                if (!IsAceStreamRunning())
                {
                    var result = MessageBox.Show(
                        "Acestream no está ejecutándose. ¿Desea iniciarlo?",
                        "Acestream no detectado",
                        MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        StartAceStream();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar Acestream: {ex.Message}");
            }
        }

        private bool IsAceStreamRunning()
        {
            return Process.GetProcessesByName("ace_engine").Length > 0;
        }

        private void StartAceStream()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = @"C:\Program Files (x86)\Ace Stream\ace_engine.exe",
                    UseShellExecute = true
                });
                Task.Delay(5000);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar Acestream: {ex.Message}");
            }
        }

        private void PlayInVLC(string aceId)
        {
            try
            {
                if (!IsAceStreamRunning())
                {
                    MessageBox.Show("Por favor, inicie Acestream primero.");
                    return;
                }

                string fullUrl = $"{ACE_BASE_URL}{aceId}";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = vlcPath, // Utilizar la ruta de VLC actualizada
                    Arguments = fullUrl,
                    UseShellExecute = false
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reproducir: {ex.Message}");
            }
        }

        private void AddChannel_Click(object sender, RoutedEventArgs e)
        {
            var inputWindow = new AddChannelWindow();
            if (inputWindow.ShowDialog() == true)
            {
                var newChannel = new Channel
                {
                    Name = inputWindow.ChannelName,
                    AceId = inputWindow.AceId
                };
                channels.Add(newChannel);
                AddChannelToPanel(newChannel);
            }
        }
    }

    public class Channel
    {
        public string Name { get; set; }
        public string AceId { get; set; }
        public string ImageUrl { get; set; } // Nueva propiedad para la URL de la imagen
    }


    public class AddChannelWindow : Window
    {
        private TextBox nameInput;
        private TextBox idInput;

        public string ChannelName => nameInput.Text;
        public string AceId => idInput.Text;

        public AddChannelWindow()
        {
            Title = "Añadir Canal";
            Width = 300;
            Height = 200;

            var grid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            var nameLabel = new Label { Content = "Nombre del Canal:" };
            nameInput = new TextBox { Margin = new Thickness(5) };

            var idLabel = new Label { Content = "ID de Acestream:" };
            idInput = new TextBox { Margin = new Thickness(5) };

            var addButton = new Button
            {
                Content = "Añadir",
                Margin = new Thickness(5),
                Padding = new Thickness(10)
            };

            addButton.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(ChannelName) && !string.IsNullOrWhiteSpace(AceId))
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                }
            };

            Grid.SetRow(nameLabel, 0);
            Grid.SetRow(nameInput, 1);
            Grid.SetRow(idLabel, 2);
            Grid.SetRow(idInput, 3);
            Grid.SetRow(addButton, 4);

            grid.Children.Add(nameLabel);
            grid.Children.Add(nameInput);
            grid.Children.Add(idLabel);
            grid.Children.Add(idInput);
            grid.Children.Add(addButton);

            Content = grid;
        }
    }
}