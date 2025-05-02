using System.Windows;

namespace The_Integrated_Assignment_Environment;

public partial class CreateAssignmentWindow : Window
{
    private string selectedFolderPath = "";

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
        // TODO: OpenFileDialog ile beklenen output dosyası seçimi
    }

    private void btnSaveAssignment_Click(object sender, RoutedEventArgs e)
    {
        var projectName = txtAssignmentName.Text;
        var selectedConfig = ((ComboBoxItem)cmbConfiguration.SelectedItem)?.Content?.ToString();
        var arguments = txtArguments.Text;

        if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(selectedFolderPath))
        {
            System.Windows.MessageBox.Show("Assignment name and submission folder are required.");
            return;
        }

        var config = new Configuration
        {
            LanguageName = selectedConfig ?? "Unknown",
            CompilerPath = "path/to/compiler", // Daha sonra doldurulacak
            CompileArguments = arguments,
            RunCommandTemplate = "run {0}",
            ExpectedOutputFilePath = "expected/output/path" // Bu da seçilen dosyadan alınmalı
        };

        var project = new Project
        {
            ProjectName = projectName,
            Configuration = config,
            SubmissionsFolderPath = selectedFolderPath,
            Submissions = new List<StudentSubmission>(),
            Results = new List<Result>()
        };

        // DEBUG: Verileri konsola yaz (veya sonra dosyaya kaydedersin)
        Console.WriteLine($"Assignment: {project.ProjectName}, Folder: {project.SubmissionsFolderPath}, Language: {project.Configuration.LanguageName}");

        System.Windows.MessageBox.Show("Assignment saved!");

        var reportWindow = new AssignmentReportWindow(project);
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