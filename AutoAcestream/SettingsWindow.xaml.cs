using System;
using System.Windows;
using Microsoft.Win32;

namespace TuNamespace
{
    public partial class SettingsWindow : Window
    {
        // Propiedad para obtener la ruta de VLC desde el TextBox
        public string VlcPath => vlcPathInput.Text;

        // Propiedades para manejar la selección de URL
        public bool UseFirstUrl { get; private set; }
        public string CustomUrl { get; private set; }

        public SettingsWindow(string currentPath, bool currentUrlChoice, string currentCustomUrl = "")
        {
            InitializeComponent();

            // Cargar configuraciones existentes
            vlcPathInput.Text = currentPath;
            radioUrl1.IsChecked = currentUrlChoice;
            radioUrl2.IsChecked = !currentUrlChoice;
            customUrlInput.Text = currentCustomUrl;
        }

        // Método para el botón Examinar
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Seleccionar VLC",
                Filter = "Ejecutables (*.exe)|*.exe",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                vlcPathInput.Text = openFileDialog.FileName;
            }
        }

        // Método para el botón Guardar
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(VlcPath))
            {
                // Guardar la configuración de la ruta de VLC
                AutoAcestream.Properties.Settings.Default.VlcPath = VlcPath;

                // Guardar la selección de URL
                UseFirstUrl = radioUrl1.IsChecked == true;

                // Solo asignar CustomUrl si el segundo radio button está seleccionado
                if (!UseFirstUrl)
                {
                    CustomUrl = customUrlInput.Text.Trim();
                    AutoAcestream.Properties.Settings.Default.CustomUrl = CustomUrl;
                }
                else
                {
                    AutoAcestream.Properties.Settings.Default.CustomUrl = ""; // Limpiar si se usa el primer URL
                }

                // Guardar cambios en la configuración
                AutoAcestream.Properties.Settings.Default.Save();

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa una ruta válida.");
            }
        }

    }
}
