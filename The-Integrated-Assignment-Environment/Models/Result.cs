namespace The_Integrated_Assignment_Environment.Models;

public class Result
{
    public string StudentId { get; set; }
    public bool CompilationSuccess { get; set; }
    public bool ExecutionSuccess { get; set; }
    public bool OutputMatch { get; set; }
    public string Output { get; set; }
    public string ErrorMessage { get; set; }

    // Bu alanlar UI gösterimi için eklendi:
    public string CompileStatus => CompilationSuccess ? "Success" : "Failed";
    public string RunStatus => ExecutionSuccess ? "Success" : "Failed";
    public string OutputMatchStatus => OutputMatch ? "Matched" : "Different";

    // XAML'deki OutputMatch binding'i doðrudan bool yerine bu property'yi göstermek için kullanýlabilir
    public string OutputMatchDisplay => OutputMatchStatus;

    public Result() { }
}
