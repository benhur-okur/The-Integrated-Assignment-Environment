using System.Diagnostics;
using System.IO;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services;

public class RunService
{
    public Result Run(StudentSubmission submission, Project project, Result result)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(submission.SourceFilePath);
        string fullCommand;

        switch (project.Configuration.LanguageName)
        {
            case "C":
            case "C++":
                fullCommand = $"{fileNameWithoutExtension}.exe {project.RunArguments}";
                break;
            case "Java":
                fullCommand = $"java {fileNameWithoutExtension} {project.RunArguments}";
                break;
            case "Python":
                fullCommand = $"python {fileNameWithoutExtension}.py {project.RunArguments}";
                break;
            case "Haskell":
                fullCommand = $"{fileNameWithoutExtension}.exe {project.RunArguments}";
                break;
            default:
                throw new NotSupportedException("Unsupported language");
        }

        Console.WriteLine($"[Runner] Working Directory: {submission.ExtractedFolderPath}");
        Console.WriteLine($"[Runner] RunCommand: {fullCommand}");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C \"{fullCommand}\"",
                WorkingDirectory = submission.ExtractedFolderPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8
            }
        };

        try
        {
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            result.ExecutionSuccess = process.ExitCode == 0;
            result.Output = output;
            result.OutputMatch = CompareOutput(output, project.ExpectedOutputFilePath);

            if (!result.ExecutionSuccess)
                result.ErrorMessage = error;
        }
        catch (Exception ex)
        {
            result.ExecutionSuccess = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    private bool CompareOutput(string actualOutput, string expectedFilePath)
    {
        if (!File.Exists(expectedFilePath))
            return false;

        string expected = File.ReadAllText(expectedFilePath);

        string Normalize(string input) =>
            string.Join("\n", input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim()));

        return Normalize(actualOutput) == Normalize(expected);
    }
}
