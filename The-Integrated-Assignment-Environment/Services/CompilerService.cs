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

        string fullCommand = config.CompileCommand
            .Replace("{filename}", fileNameWithExtension)
            .Replace("{file}", fileNameWithoutExtension);

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

            Console.WriteLine($"[Compiler] ExitCode: {process.ExitCode}");
            Console.WriteLine($"[Compiler] STDOUT:\n{output}");
            Console.WriteLine($"[Compiler] STDERR:\n{error}");

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
