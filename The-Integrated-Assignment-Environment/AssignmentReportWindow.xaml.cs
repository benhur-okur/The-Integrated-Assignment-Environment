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
    private Project _project;
    public ObservableCollection<Result> ResultList { get; set; } = new();

    public AssignmentReportWindow(Project project)
    {
        InitializeComponent();
        _project = project;
        DataContext = this;
        ResultsDataGrid.ItemsSource = ResultList;
    }

    private void btnSelectZipDirectory_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new System.Windows.Forms.FolderBrowserDialog();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            _project.SubmissionsFolderPath = dialog.SelectedPath;
        }
    }

    private void btnProcessAssignments_Click(object sender, RoutedEventArgs e)
    {
        var compiler = new CompilerService();
        var runner = new RunService();

        string[] zipFiles = Directory.GetFiles(_project.SubmissionsFolderPath, "*.zip");

        foreach (var zipFile in zipFiles)
        {
            string studentId = Path.GetFileNameWithoutExtension(zipFile);
            string extractPath = Path.Combine(_project.SubmissionsFolderPath, studentId);

            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);

            ZipFile.ExtractToDirectory(zipFile, extractPath);

            var submission = new StudentSubmission
            {
                StudentId = studentId,
                ExtractedFolderPath = extractPath,
                SourceFilePath = Path.Combine(extractPath, "main.c") // sabit, ileride dinamik yapılabilir
            };

            _project.Submissions.Add(submission);

            var result = compiler.Compile(submission, _project.Configuration);

            if (result.CompilationSuccess)
            {
                result = runner.Run(submission, _project.Configuration);
            }

            _project.Results.Add(result);
            ResultList.Add(result);
        }

        System.Windows.MessageBox.Show("Tüm ödevler işlendi!", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void btnSaveProject_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.SaveFileDialog { Filter = "Project Files (*.json)|*.json" };
        if (dialog.ShowDialog() == true)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(_project, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
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
            _project = System.Text.Json.JsonSerializer.Deserialize<Project>(json);
            ResultList.Clear();
            foreach (var r in _project.Results)
                ResultList.Add(r);
            System.Windows.MessageBox.Show("Proje yüklendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
