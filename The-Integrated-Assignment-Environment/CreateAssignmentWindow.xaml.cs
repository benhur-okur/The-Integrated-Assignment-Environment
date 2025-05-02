using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;


namespace The_Integrated_Assignment_Environment;

public partial class CreateAssignmentWindow : Window

{
    private string selectedFolderPath = "";
    private string expectedOutputFilePath = "";
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
        using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
        {
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                selectedFolderPath = dialog.SelectedPath;
                lblSelectedFolderPath.Content = selectedFolderPath;
            }
        }
    }


    private void btnSelectExpectedOutput_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Title = "Select Expected Output File",
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        };

        if (dialog.ShowDialog() == true)
        {
            lblExpectedOutputPath.Content = dialog.FileName;
            expectedOutputFilePath = dialog.FileName; // expectedOutputFilePath diye bir alan tanımlamalısın
        }
    }


    private void btnSaveAssignment_Click(object sender, RoutedEventArgs e)
    {
        var projectName = txtAssignmentName.Text;
        var selectedConfig = cmbConfiguration.SelectedItem as Configuration;
        var arguments = txtArguments.Text;

        if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(selectedFolderPath))
        {
            System.Windows.MessageBox.Show("Assignment name and submission folder are required.");
            return;
        }

        if (selectedConfig == null)
        {
            System.Windows.MessageBox.Show("Please select a configuration.");
            return;
        }

        // Yeni bir Configuration nesnesi oluşturmak yerine mevcut olanı doğrudan kullanıyoruz
        var config = new Configuration
        {
            LanguageName = selectedConfig.LanguageName,
            CompilerPath = selectedConfig.CompilerPath,
            CompileArguments = selectedConfig.CompileArguments
        };

        var project = new Project
        {
            ProjectName = projectName,
            Configuration = config,
            SubmissionsFolderPath = selectedFolderPath,
            RunArguments = arguments,
            ExpectedOutputFilePath = expectedOutputFilePath,
            Submissions = new List<StudentSubmission>(),
            Results = new List<Result>()
        };

        Console.WriteLine($"Assignment: {project.ProjectName}, Folder: {project.SubmissionsFolderPath}, Language: {project.Configuration.LanguageName}");

        System.Windows.MessageBox.Show("Assignment saved!");

        var reportWindow = new AssignmentReportWindow(project);
        reportWindow.Show();
        this.Close();
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        WelcomeWindow welcome = new WelcomeWindow();
        welcome.Show();
        this.Close();
    }
    private void btnNewConfiguration_Click(object sender, RoutedEventArgs e)
    {
        var configWindow = new ConfigurationWindow();
        configWindow.ShowDialog();

        // Config penceresi kapandıktan sonra yeniden yükle
        LoadConfigurations();
    }

}