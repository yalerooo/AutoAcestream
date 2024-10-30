using LibVLCSharp.Shared;
using System.Windows;
using Microsoft.Win32; // Para OpenFileDialog

namespace TuNamespace // Cambia esto por el espacio de nombres de tu proyecto
{
    public partial class SettingsWindow : Window
    {
        public string VlcPath => vlcPathInput.Text;

        public SettingsWindow(string currentPath)
        {
            InitializeComponent(); // Inicializa los componentes de la interfaz
            vlcPathInput.Text = currentPath; // Establece la ruta actual en el TextBox

            // Evento click para abrir el diálogo de selección de archivos
            browseButton.Click += (s, e) =>
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Seleccionar VLC",
                    Filter = "Ejecutables (*.exe)|*.exe", // Filtrar solo archivos ejecutables
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) // Abrir en la carpeta de Program Files por defecto
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    vlcPathInput.Text = openFileDialog.FileName; // Establecer la ruta en el TextBox
                }
            };

            // Evento click para el botón de guardar
            saveButton.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(VlcPath))
                {
                    DialogResult = true; // Cerrar la ventana con resultado exitoso
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa una ruta válida."); // Mostrar mensaje de error
                }
            };
        }
    }
}
