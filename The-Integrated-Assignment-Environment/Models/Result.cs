namespace The_Integrated_Assignment_Environment.Models;

public class Result
{
    public string StudentId { get; set; }
    public bool CompilationSuccess { get; set; }
    public bool ExecutionSuccess { get; set; }
    public bool OutputMatch { get; set; }
    public string ErrorMessage { get; set; }

    public Result() { }
}