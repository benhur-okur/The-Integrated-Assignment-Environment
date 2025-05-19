using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using The_Integrated_Assignment_Environment.Data;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;
using ProjectDbHandler = The_Integrated_Assignment_Environment.Services.ProjectDbHandler;

namespace The_Integrated_Assignment_Environment
{
    public partial class AssignmentReportWindow : Window
    {
        private Project currentProject;
        public ObservableCollection<Result> ResultList { get; set; } = new();
        private ObservableCollection<Configuration> configurations;

        private Project originalProject;
        private bool openedFromCreate;
        private bool suppressSuccessDialog;

        public AssignmentReportWindow(Project project, bool openedFromCreate = false, bool autoProcess = false, bool suppressSuccessDialog = false)
        {
            InitializeComponent();
            currentProject = project;
            this.openedFromCreate = openedFromCreate;
            this.suppressSuccessDialog = suppressSuccessDialog;
            originalProject = project;

            DataContext = this;
            ResultsDataGrid.ItemsSource = ResultList;

            txtAssignmentName.Text = currentProject.ProjectName;
            txtConfigurationName.Text = currentProject.Configuration.LanguageName;
            txtSubmissionFolder.Text = currentProject.SubmissionsFolderPath;
            txtExpectedOutputPath.Text = currentProject.ExpectedOutputFilePath;
            
            foreach (var result in currentProject.Results)
                ResultList.Add(result);
            
            if (autoProcess)
            {
                btnProcessAssignments.Visibility = Visibility.Collapsed;
                AutoProcessAssignmentsSafely();
            }
            
            btnSaveResults.Visibility = Visibility.Collapsed;
        }

        private void AutoProcessAssignmentsSafely()
        {
            if (!Directory.Exists(currentProject.SubmissionsFolderPath))
            {
                System.Windows.MessageBox.Show("Submissions folder not found. Please select a valid folder.", "Folder Mismatch", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(currentProject.ExpectedOutputFilePath))
            {
                System.Windows.MessageBox.Show("Expected output file not found. Please select a valid .txt file.", "File Mismatch", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ProcessAssignmentsAndDisplayResults();
        }

        private void ProcessAssignmentsAndDisplayResults()
        {
            ResultList.Clear();
            currentProject.Results.Clear();
            currentProject.Submissions.Clear();

            var processor = new AssignmentProcessor();
            var results = processor.ProcessAll(currentProject);

            foreach (var r in results)
                ResultList.Add(r);

            if (!suppressSuccessDialog)
            {
                System.Windows.MessageBox.Show("All submissions has scanned.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            btnSaveResults.Visibility = Visibility.Visible;
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
                btnProcessAssignments.Visibility = Visibility.Visible;
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

            ProcessAssignmentsAndDisplayResults();
        }

        private void btnSaveResults_Click(object sender, RoutedEventArgs e)
        {
            var db = new ProjectDbHandler();
            db.UpdateProjectResults(currentProject);
            System.Windows.MessageBox.Show("Results successfully saved.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (openedFromCreate)
            {
                var createWindow = new CreateAssignmentWindow();
                createWindow.Show();
                createWindow.LoadProject(originalProject);
                this.Close();
            }
            else
            {
                var welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();
                this.Close();
            }
        }
        
        private void btnOpenAssignment_Click(object sender, RoutedEventArgs e)
        {
            new OpenAssignmentWindow().Show();
            this.Close();
        }

        private void btnNewConfiguration_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new ConfigurationWindow();
            configWindow.ShowDialog();
            LoadConfigurations();
        }
        
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Owner = this; 
            helpWindow.ShowDialog(); 
        }
        
    }
}
