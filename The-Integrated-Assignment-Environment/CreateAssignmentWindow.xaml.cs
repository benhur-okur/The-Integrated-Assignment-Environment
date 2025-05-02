using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;

namespace The_Integrated_Assignment_Environment;

public partial class CreateAssignmentWindow : Window
{
    private ObservableCollection<Configuration> configurations;

    public CreateAssignmentWindow()
    {
        InitializeComponent();
        LoadConfigurations();
    }

    private void LoadConfigurations()
    {
        configurations = ConfigurationService.LoadAll();
        cmbConfiguration.ItemsSource = configurations;
        cmbConfiguration.DisplayMemberPath = "LanguageName";
    }

    private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
    {
        // TODO: FolderBrowserDialog kullanarak klasör seçimi
    }

    private void btnSelectExpectedOutput_Click(object sender, RoutedEventArgs e)
    {
        // TODO: OpenFileDialog ile beklenen output dosyası seçimi
    }

    private void btnSaveAssignment_Click(object sender, RoutedEventArgs e)
    {
        AssignmentReportWindow reportWindow = new AssignmentReportWindow();
        reportWindow.Show();
        this.Close();
    }

    private void btnNewConfiguration_Click(object sender, RoutedEventArgs e)
    {
        var configWindow = new ConfigurationWindow();
        configWindow.ShowDialog();

        // Config penceresi kapandıktan sonra yeniden yükle
        LoadConfigurations();
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        WelcomeWindow welcome = new WelcomeWindow();
        welcome.Show();
        this.Close();
    }
}