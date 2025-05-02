using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services
{
    public static class ConfigurationService
    {
        private static string dbPath = "iae.db";

        public static ObservableCollection<Configuration> LoadAll()
        {
            var list = new ObservableCollection<Configuration>();
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            // Tablonun var olup olmadığını kontrol et ve oluştur (yalnızca 3 alan içeriyor)
            var createTableCmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Configurations (
                    Language TEXT PRIMARY KEY,
                    CompilerPath TEXT,
                    Arguments TEXT
                )", conn);
            createTableCmd.ExecuteNonQuery();

            // Verileri çek
            var selectCmd = new SQLiteCommand("SELECT * FROM Configurations", conn);
            using var reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Configuration
                {
                    LanguageName = reader["Language"].ToString(),
                    CompilerPath = reader["CompilerPath"].ToString(),
                    CompileArguments = reader["Arguments"].ToString()
                });
            }

            return list;
        }

        public static void Add(Configuration config)
        {
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            try
            {
                var cmd = new SQLiteCommand(
                    "INSERT INTO Configurations (Language, CompilerPath, Arguments) VALUES (@lang, @comp, @args)", conn);
                cmd.Parameters.AddWithValue("@lang", config.LanguageName);
                cmd.Parameters.AddWithValue("@comp", config.CompilerPath);
                cmd.Parameters.AddWithValue("@args", config.CompileArguments);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Failed to add configuration:\n{ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void Update(Configuration config)
        {
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var cmd = new SQLiteCommand(
                "UPDATE Configurations SET CompilerPath=@comp, Arguments=@args WHERE Language=@lang", conn);
            cmd.Parameters.AddWithValue("@lang", config.LanguageName);
            cmd.Parameters.AddWithValue("@comp", config.CompilerPath);
            cmd.Parameters.AddWithValue("@args", config.CompileArguments);
            cmd.ExecuteNonQuery();
        }

        public static void Delete(Configuration config)
        {
            using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            var cmd = new SQLiteCommand("DELETE FROM Configurations WHERE Language=@lang", conn);
            cmd.Parameters.AddWithValue("@lang", config.LanguageName);
            cmd.ExecuteNonQuery();
        }
    }
}