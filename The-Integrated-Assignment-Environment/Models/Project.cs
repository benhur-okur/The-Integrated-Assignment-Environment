using The_Integrated_Assignment_Environment.Models;
using Configuration = System.Configuration.Configuration;

namespace The_Integrated_Assignment_Environment.Models
{
    public class Project
    {
        public string ProjectName { get; set; }
        public Configuration Configuration { get; set; }
        public string SubmissionsFolderPath { get; set; }
        public string RunArguments { get; set; } // yeni
        public string ExpectedOutputFilePath { get; set; } // yeni

        public List<StudentSubmission> Submissions { get; set; } = new();
        public List<Result> Results { get; set; } = new();
    }

}