using System.Collections.ObjectModel;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;

namespace The_Integrated_Assignment_Environment
{
    public partial class ConfigurationWindow : Window
    {
        private ObservableCollection<Configuration> configurations = new();

        public ConfigurationWindow()
        {
            InitializeComponent();
            LoadConfigurations();
        }

        private void LoadConfigurations()
        {
            configurations = ConfigurationService.LoadAll();
            dgConfigurations.ItemsSource = configurations;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = new ConfigurationForm();
            form.OnSave += (config) =>
            {
                ConfigurationService.Add(config);
                LoadConfigurations();
                ShowDataGrid();
            };
            form.OnCancel += ShowDataGrid;

            configFormContainer.Content = form;
            ShowForm();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgConfigurations.SelectedItem is Configuration selected)
            {
                var form = new ConfigurationForm(selected);
                form.OnSave += (config) =>
                {
                    ConfigurationService.Update(config);
                    LoadConfigurations();
                    ShowDataGrid();
                };
                form.OnCancel += ShowDataGrid;

                configFormContainer.Content = form;
                ShowForm();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgConfigurations.SelectedItem is Configuration selected)
            {
                var result = MessageBox.Show("Delete selected configuration?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ConfigurationService.Delete(selected);
                    LoadConfigurations();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowForm()
        {
            configFormContainer.Visibility = Visibility.Visible;
            dgConfigurations.Visibility = Visibility.Collapsed;
            buttonPanel.Visibility = Visibility.Collapsed; // <- yeni
        }

        private void ShowDataGrid()
        {
            configFormContainer.Visibility = Visibility.Collapsed;
            dgConfigurations.Visibility = Visibility.Visible;
            buttonPanel.Visibility = Visibility.Visible; // <- yeni
        }

    }
}