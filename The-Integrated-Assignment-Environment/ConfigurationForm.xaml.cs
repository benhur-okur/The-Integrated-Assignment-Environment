
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment
{
    public partial class ConfigurationForm : UserControl
    {
        public Configuration Config { get; set; } = new Configuration();

        public event Action<Configuration>? OnSave;
        public event Action? OnCancel;

        public ConfigurationForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ConfigurationForm(Configuration config)
        {
            InitializeComponent();
            Config = config;
            this.DataContext = this;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Config.LanguageName) || string.IsNullOrWhiteSpace(Config.CompileArguments))
            {
                MessageBox.Show("Language and Compile Command cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OnSave?.Invoke(Config);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OnCancel?.Invoke();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select Compiler Executable",
                Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                txtCompilerPath.Text = dialog.FileName;
                Config.CompilerPath = dialog.FileName;
            }
        }
    }
}