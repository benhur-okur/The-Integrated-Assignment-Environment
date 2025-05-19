using System.Collections.Generic;
using System.Data.SQLite;
using System.Text.Json;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services
{
    public class ProjectDbHandler
    {
        private const string dbPath = "iae.db";

        public ProjectDbHandler() => InitializeDatabase();

        
        
        private void InitializeDatabase()
        {
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var createProjects = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Projects (
                    ProjectName TEXT PRIMARY KEY,
                    Json TEXT NOT NULL
                )", conn);
            createProjects.ExecuteNonQuery();

            var createResults = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Results (
                    ProjectName TEXT,
                    StudentId TEXT,
                    CompilationSuccess INTEGER,
                    ExecutionSuccess INTEGER,
                    OutputMatch INTEGER,
                    Output TEXT,
                    ErrorMessage TEXT,
                    PRIMARY KEY (ProjectName, StudentId)
                )", conn);
            createResults.ExecuteNonQuery();
        }

        public void InsertProject(Project project)
        {
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var json = JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true });

            var cmd = new SQLiteCommand("INSERT OR REPLACE INTO Projects (ProjectName, Json) VALUES (@name, @json)", conn);
            cmd.Parameters.AddWithValue("@name", project.ProjectName);
            cmd.Parameters.AddWithValue("@json", json);
            cmd.ExecuteNonQuery();
        }

        public List<Project> LoadAll()
        {
            var list = new List<Project>();

            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var cmd = new SQLiteCommand("SELECT * FROM Projects", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var json = reader["Json"].ToString();
                var project = JsonSerializer.Deserialize<Project>(json);
                project.Results = LoadResults(project.ProjectName);
                list.Add(project);
            }

            return list;
        }

        public List<Result> LoadResults(string projectName)
        {
            var results = new List<Result>();

            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var cmd = new SQLiteCommand("SELECT * FROM Results WHERE ProjectName = @p", conn);
            cmd.Parameters.AddWithValue("@p", projectName);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                results.Add(new Result
                {
                    StudentId = reader["StudentId"].ToString(),
                    CompilationSuccess = (long)reader["CompilationSuccess"] == 1,
                    ExecutionSuccess = (long)reader["ExecutionSuccess"] == 1,
                    OutputMatch = (long)reader["OutputMatch"] == 1,
                    Output = reader["Output"].ToString(),
                    ErrorMessage = reader["ErrorMessage"].ToString()
                });
            }

            return results;
        }

        public void UpdateProjectResults(Project project)
        {
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var deleteCmd = new SQLiteCommand("DELETE FROM Results WHERE ProjectName = @p", conn);
            deleteCmd.Parameters.AddWithValue("@p", project.ProjectName);
            deleteCmd.ExecuteNonQuery();

            foreach (var r in project.Results)
            {
                var insertCmd = new SQLiteCommand(@"
                    INSERT INTO Results (
                        ProjectName, StudentId,
                        CompilationSuccess, ExecutionSuccess, OutputMatch,
                        Output, ErrorMessage
                    ) VALUES (
                        @project, @id, @compile, @exec, @match, @output, @error
                    )", conn);

                insertCmd.Parameters.AddWithValue("@project", project.ProjectName);
                insertCmd.Parameters.AddWithValue("@id", r.StudentId);
                insertCmd.Parameters.AddWithValue("@compile", r.CompilationSuccess ? 1 : 0);
                insertCmd.Parameters.AddWithValue("@exec", r.ExecutionSuccess ? 1 : 0);
                insertCmd.Parameters.AddWithValue("@match", r.OutputMatch ? 1 : 0);
                insertCmd.Parameters.AddWithValue("@output", r.Output ?? "");
                insertCmd.Parameters.AddWithValue("@error", r.ErrorMessage ?? "");
                insertCmd.ExecuteNonQuery();
            }
        }

        public List<Result> LoadResultsForProject(string projectName)
        {
            return LoadResults(projectName);
        }
    }
}
