using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using The_Integrated_Assignment_Environment.Data;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;
using ProjectDbHandler = The_Integrated_Assignment_Environment.Services.ProjectDbHandler;

namespace The_Integrated_Assignment_Environment
{
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

        public void LoadProject(Project project)
        {
            txtAssignmentName.Text = project.ProjectName;
            cmbConfiguration.SelectedItem = configurations.FirstOrDefault(c =>
                c.LanguageName == project.Configuration.LanguageName &&
                c.CompileCommand == project.Configuration.CompileCommand &&
                c.RunCommand == project.Configuration.RunCommand);

            selectedFolderPath = project.SubmissionsFolderPath;
            lblSelectedFolderPath.Content = selectedFolderPath;

            expectedOutputFilePath = project.ExpectedOutputFilePath;
            lblExpectedOutputPath.Content = expectedOutputFilePath;

            txtArguments.Text = project.RunArguments;
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedFolderPath = dialog.SelectedPath;
                lblSelectedFolderPath.Content = selectedFolderPath;
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
                expectedOutputFilePath = dialog.FileName;
                lblExpectedOutputPath.Content = expectedOutputFilePath;
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

            var project = new Project
            {
                ProjectName = projectName,
                Configuration = new Configuration
                {
                    LanguageName = selectedConfig.LanguageName,
                    CompileCommand = selectedConfig.CompileCommand,
                    RunCommand = selectedConfig.RunCommand
                },
                SubmissionsFolderPath = selectedFolderPath,
                RunArguments = arguments,
                ExpectedOutputFilePath = expectedOutputFilePath,
                Submissions = new(),
                Results = new()
            };

            new ProjectDbHandler().InsertProject(project);
            System.Windows.MessageBox.Show("Assignment saved!");

            var reportWindow = new AssignmentReportWindow(project, openedFromCreate: true);
            reportWindow.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new WelcomeWindow().Show();
            this.Close();
        }

        private void btnNewConfiguration_Click(object sender, RoutedEventArgs e)
        {
            new ConfigurationWindow().ShowDialog();
            LoadConfigurations();
        }

        private void btnOpenAssignment_Click(object sender, RoutedEventArgs e)
        {
            new OpenAssignmentWindow().Show();
            this.Close();
        }
    }
}
