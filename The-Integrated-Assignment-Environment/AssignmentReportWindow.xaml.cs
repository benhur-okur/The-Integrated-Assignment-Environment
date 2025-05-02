using System.Collections.ObjectModel;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;

namespace The_Integrated_Assignment_Environment;

public partial class AssignmentReportWindow : Window
{
    private ObservableCollection<Configuration> configurations;
    public AssignmentReportWindow()
    {
        InitializeComponent();
    }
    
    private void LoadConfigurations()
    {
        configurations = ConfigurationService.LoadAll();
        //cmbConfiguration.ItemsSource = configurations;
        //cmbConfiguration.DisplayMemberPath = "LanguageName";
    }
    

    private void btnSelectZipDirectory_Click(object sender, RoutedEventArgs e)
    {
        // TODO: ZIP klasör seçimi yapılacak
    }

    private void btnProcessAssignments_Click(object sender, RoutedEventArgs e)
    {
        // TODO: ZIP dosyalarını işleyip sonuçları gösterecek
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        var createAssignmentWindow = new CreateAssignmentWindow();
        createAssignmentWindow.Show();
        this.Close();
    }
    private void btnNewConfiguration_Click(object sender, RoutedEventArgs e)
    {
        var configWindow = new ConfigurationWindow();
        configWindow.ShowDialog();

        // Config penceresi kapandıktan sonra yeniden yükle
        LoadConfigurations();
    }
    public class AssignmentResult
    {
        public string StudentId { get; set; }
        public string CompileStatus { get; set; }
        public string RunStatus { get; set; }
        public string OutputMatch { get; set; }
    }
}