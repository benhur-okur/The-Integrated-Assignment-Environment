using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Data;

namespace The_Integrated_Assignment_Environment
{
    public partial class OpenAssignmentWindow : Window
    {
        private ProjectDbHandler _dbHandler = new ProjectDbHandler();
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
            var selectedProject = lstProjects.SelectedItem as Project;

            if (selectedProject == null)
            {
                System.Windows.MessageBox.Show("Please select a project to open.");
                return;
            }

            var reportWindow = new AssignmentReportWindow(selectedProject);
            reportWindow.Show();
            this.Close();
        }

        private void lstProjects_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnOpen_Click(sender, e); // çift tıklamada da aç
        }
        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
            this.Close();
        }

    }
    
    
}