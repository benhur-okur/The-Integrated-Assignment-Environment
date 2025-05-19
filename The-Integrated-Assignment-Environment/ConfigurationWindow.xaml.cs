using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
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
            form.OnSave += config =>
            {
                ConfigurationService.Add(config);
                LoadConfigurations();
                ToggleFormView(false);
            };
            form.OnCancel += () => ToggleFormView(false);
            configFormContainer.Content = form;
            ToggleFormView(true);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgConfigurations.SelectedItem is Configuration selected)
            {
                var form = new ConfigurationForm(selected);
                form.OnSave += config =>
                {
                    ConfigurationService.Update(config);
                    LoadConfigurations();
                    ToggleFormView(false);
                };
                form.OnCancel += () => ToggleFormView(false);
                configFormContainer.Content = form;
                ToggleFormView(true);
            }
            else
            {
                System.Windows.MessageBox.Show("Please select exactly one configuration to edit.", "Edit Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgConfigurations.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select at least one configuration to delete.", "Delete Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedConfigs = dgConfigurations.SelectedItems.Cast<Configuration>().ToList();
            string configNames = string.Join("\n", selectedConfigs.Select(c => $"- {c.LanguageName}"));

            var confirm = System.Windows.MessageBox.Show(
                $"Are you sure you want to delete the following configurations?\n\n{configNames}",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                foreach (var config in selectedConfigs)
                    ConfigurationService.Delete(config);

                LoadConfigurations();
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
            buttonPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowDataGrid()
        {
            configFormContainer.Visibility = Visibility.Collapsed;
            dgConfigurations.Visibility = Visibility.Visible;
            buttonPanel.Visibility = Visibility.Visible;
        }

        private void ToggleFormView(bool showForm)
        {
            configFormContainer.Visibility = showForm ? Visibility.Visible : Visibility.Collapsed;
            dgConfigurations.Visibility = showForm ? Visibility.Collapsed : Visibility.Visible;
            buttonPanel.Visibility = showForm ? Visibility.Collapsed : Visibility.Visible;

            // Import & Export butonlarını gizle/göster
            btnImport.Visibility = showForm ? Visibility.Collapsed : Visibility.Visible;
            btnExport.Visibility = showForm ? Visibility.Collapsed : Visibility.Visible;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                Title = "Import Configurations"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    configurations = ConfigurationService.ImportFromFile(openFileDialog.FileName);
                    dgConfigurations.ItemsSource = configurations;
                    System.Windows.MessageBox.Show("Configurations imported successfully!", "Import",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Failed to import configurations:\n{ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (dgConfigurations.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select at least one configuration to export.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedConfigs = dgConfigurations.SelectedItems.Cast<Configuration>().ToList();

            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                FileName = "ConfigurationsExport.json"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var json = JsonSerializer.Serialize(selectedConfigs, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(dialog.FileName, json);
                    System.Windows.MessageBox.Show("Selected configurations exported successfully.", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Failed to export configurations:\n{ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
