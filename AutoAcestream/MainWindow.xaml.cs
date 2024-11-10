using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
namespace AceStreamPlayer
{
    public partial class MainWindow : Window
    {
        private List<Channel> channels = new List<Channel>();
        private ObservableCollection<Channel> filteredChannels = new ObservableCollection<Channel>(); // Nueva lista para los canales filtrados
        private const string ACE_BASE_URL = "http://127.0.0.1:6878/ace/getstream?id=";
        private bool useFirstUrl = true;
        private string customUrl = "";
        private const string DEFAULT_CHANNELS_URL = "https://actualsebastian.vercel.app/base.txt";
        private const string DEFAULT_CHANNELS_URL2 = "https://raw.githubusercontent.com/yalerooo/listaparatv/refs/heads/main/lista2";
        private static readonly HttpClient client = new HttpClient();
        private LibVLC _libVLC;
        private System.Windows.Media.MediaPlayer _mediaPlayer;

        private string vlcPath = @"C:\Program Files\VideoLAN\VLC\vlc.exe";
        public MainWindow()
        {
            InitializeComponent();

            CheckAceStream();
            LoadSettings();
            LoadChannels();

        }

        private void LoadSettings()
        {
            vlcPath = AutoAcestream.Properties.Settings.Default.VlcPath;

            // Verificar si vlcPath es null o una cadena vacía
            if (string.IsNullOrEmpty(vlcPath))
            {
                // Si vlcPath es nulo o vacío, usar la ruta por defecto
                vlcPath = @"C:\Program Files\VideoLAN\VLC\vlc.exe"; // Ruta por defecto
            }
        }




        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(vlcPath, useFirstUrl, customUrl);
            if (settingsWindow.ShowDialog() == true)
            {
                vlcPath = settingsWindow.VlcPath;
                useFirstUrl = settingsWindow.UseFirstUrl;
                customUrl = settingsWindow.CustomUrl;

                // Recargar canales con la nueva configuración
                LoadChannels();
            }
        }

        private async void LoadChannels()
        {
            channels.Clear();
            await LoadChannelsFromUrl(GetSelectedUrl());
        }


        private async Task LoadChannelsFromUrl(string url)
        {
            try
            {
                var response = await client.GetStringAsync(url);
                ParseChannels(response, url == DEFAULT_CHANNELS_URL);
                filteredChannels = new ObservableCollection<Channel>(channels);
                RefreshChannelList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar canales: {ex.Message}");
            }
        }



        private string GetSelectedUrl()
        {
            // Si hay una URL personalizada y está habilitada, úsala
            if (!string.IsNullOrWhiteSpace(customUrl))
            {
                return customUrl;
            }

            // De lo contrario, usa la URL seleccionada
            return useFirstUrl ? DEFAULT_CHANNELS_URL : DEFAULT_CHANNELS_URL2;
        }

        private void ReloadChannelsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadChannels(); // Simplificar recarga de canales
        }



        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            filteredChannels = new ObservableCollection<Channel>(
    channels.Where(channel => channel.Name.ToLower().Contains(searchText)));
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
                filteredChannels = new ObservableCollection<Channel>(
    channels.Where(channel => channel.Name.ToLower().Contains(searchText.ToLower())));
                RefreshChannelList();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un texto para buscar.");
            }
        }

        private void ParseChannels(string m3uContent, bool isFirstSource)
        {
            channels.Clear();
            var lines = m3uContent.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string name = null, aceId = null, imageUrl = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    // Extraer nombre y logo para ambas fuentes
                    var nameMatch = Regex.Match(line, isFirstSource ? @",(.*?)$" : @"tvg-id=""[^""]*"",\s*(.*)");
                    if (nameMatch.Success) name = nameMatch.Groups[1].Value.Trim();

                    var logoMatch = Regex.Match(line, @"tvg-logo=""([^""]*)""");
                    if (logoMatch.Success) imageUrl = logoMatch.Groups[1].Value.Trim();
                }
                else if (line.StartsWith("acestream://") || line.StartsWith("http"))
                {
                    var aceIdMatch = Regex.Match(line, @"id=([a-fA-F0-9]+)");
                    aceId = line.StartsWith("acestream://") ? line.Replace("acestream://", "").Trim() :
                            (aceIdMatch.Success ? aceIdMatch.Groups[1].Value : null);

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(aceId))
                    {
                        channels.Add(new Channel { Name = name, AceId = aceId, ImageUrl = imageUrl });
                        name = aceId = imageUrl = null;
                    }
                }
            }
        }



        private void RefreshChannelList()
        {
            channelsPanel.Children.Clear();
            foreach (var channel in filteredChannels)
                channelsPanel.Children.Add(CreateChannelUI(channel));
        }


        private StackPanel CreateChannelUI(Channel channel)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var image = new Image
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(5),
                Source = new BitmapImage(new Uri(channel.ImageUrl ?? "https://static.thenounproject.com/png/4595376-200.png")),
                Cursor = Cursors.Hand,
                Stretch = Stretch.Fill,
                Clip = new RectangleGeometry(new Rect(0, 0, 150, 150), 20, 20)
            };

            image.MouseDown += (s, e) => PlayInVLC(channel.AceId);

            var textBlock = new TextBlock
            {
                Text = channel.Name,
                TextAlignment = TextAlignment.Center,
                FontSize = 14,
                Foreground = Brushes.White,
                MaxWidth = 150
            };

            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock);
            return stackPanel;
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
            if (!IsAceStreamRunning())
            {
                MessageBox.Show("Inicie Acestream.");
                return;
            }

            Process.Start(new ProcessStartInfo(vlcPath, $"{ACE_BASE_URL}{aceId}"));
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

                // Usa el nuevo método para crear la UI del canal y añadirlo al panel
                var channelUI = CreateChannelUI(newChannel);
                channelsPanel.Children.Add(channelUI);
            }
        }


        public class Channel
        {
            public string Name { get; set; }
            public string AceId { get; set; }
            public string ImageUrl { get; set; }
            public string GroupTitle { get; set; } // Nueva propiedad para el grupo
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
}