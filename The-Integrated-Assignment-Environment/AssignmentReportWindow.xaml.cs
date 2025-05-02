using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;



namespace The_Integrated_Assignment_Environment;

public partial class AssignmentReportWindow : Window
{
    private Project currentProject;
    public ObservableCollection<Result> ResultList { get; set; } = new();

    public AssignmentReportWindow(Project project)
    {
        InitializeComponent();
        currentProject = project;
        DataContext = this;
        ResultsDataGrid.ItemsSource = ResultList;

        txtAssignmentName.Text = currentProject.ProjectName;
        txtConfigurationName.Text = currentProject.Configuration.LanguageName;
        txtSubmissionFolder.Text = currentProject.SubmissionsFolderPath;
    }

    private void btnSelectZipDirectory_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            currentProject.SubmissionsFolderPath = dialog.SelectedPath;
            txtSubmissionFolder.Text = currentProject.SubmissionsFolderPath;
        }
    }

    private void btnProcessAssignments_Click(object sender, RoutedEventArgs e)
    {
        var compiler = new CompilerService();
        var runner = new RunService();

        string[] zipFiles = Directory.GetFiles(currentProject.SubmissionsFolderPath, "*.zip");

        foreach (var zipFile in zipFiles)
        {
            string studentId = Path.GetFileNameWithoutExtension(zipFile);
            string extractPath = Path.Combine(currentProject.SubmissionsFolderPath, studentId);

            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);

            ZipFile.ExtractToDirectory(zipFile, extractPath);

            var submission = new StudentSubmission
            {
                StudentId = studentId,
                ExtractedFolderPath = extractPath,
                SourceFilePath = Path.Combine(extractPath, "main.c") // sabit, ileride dinamik yapılabilir
            };

            currentProject.Submissions.Add(submission);

            var result = compiler.Compile(submission, currentProject.Configuration);

            if (result.CompilationSuccess)
            {
                result = runner.Run(submission, currentProject.Configuration);
            }

            currentProject.Results.Add(result);
            ResultList.Add(result);
        }

        System.Windows.MessageBox.Show("Tüm ödevler işlendi!", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void btnSaveProject_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.SaveFileDialog { Filter = "Project Files (*.json)|*.json" };
        if (dialog.ShowDialog() == true)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(currentProject, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dialog.FileName, json);
            System.Windows.MessageBox.Show("Proje kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void btnLoadProject_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog { Filter = "Project Files (*.json)|*.json" };
        if (dialog.ShowDialog() == true)
        {
            string json = File.ReadAllText(dialog.FileName);
            currentProject = System.Text.Json.JsonSerializer.Deserialize<Project>(json);
            ResultList.Clear();
            foreach (var r in currentProject.Results)
                ResultList.Add(r);
            System.Windows.MessageBox.Show("Proje yüklendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
