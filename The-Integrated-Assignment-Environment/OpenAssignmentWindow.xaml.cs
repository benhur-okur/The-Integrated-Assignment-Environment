using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Data; // Eðer ProjectDbHandler burada ise
// veya Service klasöründeyse ona göre namespace'i ayarla

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
            cmbProjects.ItemsSource = _projects;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var selectedProject = cmbProjects.SelectedItem as Project;

            if (selectedProject == null)
            {
                System.Windows.MessageBox.Show("Please select a project.");

                return;
            }

            // Projeyi aç – örnek olarak rapor ekranýna git
            var reportWindow = new AssignmentReportWindow(selectedProject);
            reportWindow.Show();
            this.Close();
        }
       

    }
}
