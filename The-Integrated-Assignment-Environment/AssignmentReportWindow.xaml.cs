using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;

namespace The_Integrated_Assignment_Environment
{
    public partial class AssignmentReportWindow : Window
    {
        private Project currentProject;
        public ObservableCollection<Result> ResultList { get; set; } = new();
        private ObservableCollection<Configuration> configurations;

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

        private void LoadConfigurations()
        {
            configurations = ConfigurationService.LoadAll();
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
            if (string.IsNullOrWhiteSpace(currentProject.SubmissionsFolderPath))
            {
                System.Windows.MessageBox.Show("Please select a submissions folder.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ResultList.Clear();                     // 👈 UI listesini temizle
            currentProject.Results.Clear();         // 👈 Proje içindeki eski sonuçları temizle
            currentProject.Submissions.Clear();     // 👈 Aynı şekilde submissions da temizlenmeli

            var processor = new AssignmentProcessor();
            var results = processor.ProcessAll(currentProject); // tüm zipleri işler

            foreach (var r in results)
                ResultList.Add(r); // UI'da görünür hale getir

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

                txtAssignmentName.Text = currentProject.ProjectName;
                txtConfigurationName.Text = currentProject.Configuration.LanguageName;
                txtSubmissionFolder.Text = currentProject.SubmissionsFolderPath;

                System.Windows.MessageBox.Show("Proje yüklendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            LoadConfigurations(); // config listesi güncellenirse hazır olsun
        }
    }
}
