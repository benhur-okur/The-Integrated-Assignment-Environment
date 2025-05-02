namespace The_Integrated_Assignment_Environment.Models;


    public class Configuration
    {
        public string LanguageName { get; set; }
        public string CompilerPath { get; set; }
        public string CompileArguments { get; set; }
        public string RunCommandTemplate { get; set; }
        public string ExpectedOutputFilePath { get; set; }
        public string RunArguments { get; set; }


    public Configuration() { }
    }
