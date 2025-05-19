using Microsoft.Data.Sqlite;
using System.Text.Json;
using The_Integrated_Assignment_Environment.Models;
using The_Integrated_Assignment_Environment.Data;

namespace The_Integrated_Assignment_Environment.Data
{
    public partial class ProjectDbHandler
    {
        private const string ConnectionString = "Data Source=assignment.db";

        public ProjectDbHandler()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Projects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProjectName TEXT,
                    ConfigurationJson TEXT,
                    SubmissionsFolderPath TEXT,
                    RunArguments TEXT,
                    ExpectedOutputPath TEXT
                );";
            cmd.ExecuteNonQuery();
        }

        public void InsertProject(Project project)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Projects 
                (ProjectName, ConfigurationJson, SubmissionsFolderPath, RunArguments, ExpectedOutputPath)
                VALUES ($name, $config, $folder, $args, $expected);";

            cmd.Parameters.AddWithValue("$name", project.ProjectName);
            cmd.Parameters.AddWithValue("$config", JsonSerializer.Serialize(project.Configuration));
            cmd.Parameters.AddWithValue("$folder", project.SubmissionsFolderPath);
            cmd.Parameters.AddWithValue("$args", project.RunArguments);
            cmd.Parameters.AddWithValue("$expected", project.ExpectedOutputFilePath);

            cmd.ExecuteNonQuery();
        }
        public List<Project> LoadAll()
        {
            var projects = new List<Project>();

            using var conn = new SqliteConnection(ConnectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Projects";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var project = new Project
                {
                    ProjectName = reader["ProjectName"].ToString(),
                    Configuration = JsonSerializer.Deserialize<Configuration>(reader["ConfigurationJson"].ToString()),
                    SubmissionsFolderPath = reader["SubmissionsFolderPath"].ToString(),
                    RunArguments = reader["RunArguments"].ToString(),
                    ExpectedOutputFilePath = reader["ExpectedOutputPath"].ToString(),
                    Submissions = new List<StudentSubmission>(),
                    Results = new List<Result>()
                };
                projects.Add(project);
            }

            return projects;
        }

    }
}
