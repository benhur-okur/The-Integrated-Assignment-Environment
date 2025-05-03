using System.Diagnostics;
using System.IO;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services;

public class CompilerService
{
    public Result Compile(StudentSubmission submission, Configuration config)
    {
        Result result = new() { StudentId = submission.StudentId };

        string fileNameWithExtension = Path.GetFileName(submission.SourceFilePath);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(submission.SourceFilePath);

        string fullCommand;

        switch (config.LanguageName)
        {
            case "C":
                fullCommand = $"gcc {fileNameWithExtension} -o {fileNameWithoutExtension}.exe";
                break;
            case "C++":
                fullCommand = $"g++ {fileNameWithExtension} -o {fileNameWithoutExtension}.exe";
                break;
            case "Java":
                fullCommand = $"javac {fileNameWithExtension}";
                break;
            case "Python":
                fullCommand = $"python -m py_compile {fileNameWithExtension}";
                break;
            case "Haskell":
                fullCommand = $"ghc {fileNameWithExtension} -o {fileNameWithoutExtension}.exe";
                break;
            default:
                throw new NotSupportedException("Unsupported language");
        }

        Console.WriteLine($"[Compiler] Working Directory: {submission.ExtractedFolderPath}");
        Console.WriteLine($"[Compiler] CompileCommand: {fullCommand}");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo("cmd.exe", $"/C \"{fullCommand}\"")
            {
                WorkingDirectory = submission.ExtractedFolderPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        try
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            result.CompilationSuccess = process.ExitCode == 0;
            result.Output = output;

            if (!result.CompilationSuccess)
            {
                result.ErrorMessage = error;
            }
        }
        catch (Exception ex)
        {
            result.CompilationSuccess = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}
