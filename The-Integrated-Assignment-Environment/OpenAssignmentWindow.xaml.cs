using System;
using System.Collections.Generic;
using System.Windows;
using The_Integrated_Assignment_Environment.Data;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Services;
using ProjectDbHandler = The_Integrated_Assignment_Environment.Services.ProjectDbHandler;


namespace The_Integrated_Assignment_Environment
{
    public partial class OpenAssignmentWindow : Window
    {
        private ProjectDbHandler _dbHandler = new();
        private List<Project> _projects;

        public OpenAssignmentWindow()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            _projects = _dbHandler.LoadAll();
            lstProjects.ItemsSource = _projects;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (lstProjects.SelectedItem is not Project selectedProject)
            {
                System.Windows.MessageBox.Show("Please select a project to open.");
                return;
            }

            var db = new ProjectDbHandler();
            selectedProject.Results = db.LoadResultsForProject(selectedProject.ProjectName); 

            var reportWindow = new AssignmentReportWindow(selectedProject, openedFromCreate: false, autoProcess: false, suppressSuccessDialog: true);
            reportWindow.Show();
            Close();
        }


        private void lstProjects_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnOpen_Click(sender, e);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            this.Close();
        }
    }
}