using System.Diagnostics;
using System.IO;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services;

public class RunService
{
    public Result Run(StudentSubmission submission, Configuration config)
    {
        Result result = new() { StudentId = submission.StudentId };

        string runCommand = config.RunCommandTemplate.Replace("{args}", config.RunArguments);

        var process = new Process
        {
            StartInfo = new ProcessStartInfo("cmd.exe", $"/C {runCommand}")
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

            result.ExecutionSuccess = process.ExitCode == 0;
            result.Output = output;
            result.OutputMatch = CompareOutput(output, config.ExpectedOutputFilePath);

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
        string expected = File.ReadAllText(expectedFilePath);
        return actualOutput.Trim() == expected.Trim();
    }
}
