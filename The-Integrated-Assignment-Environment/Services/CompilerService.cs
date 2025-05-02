using System.Diagnostics;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services;

public class CompilerService
{
    public Result Compile(StudentSubmission submission, Configuration config)
    {
        Result result = new() { StudentId = submission.StudentId };

        string compileCommand = $"{config.CompilerPath} {config.CompileArguments}";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo("cmd.exe", $"/C {compileCommand}")
            {
                WorkingDirectory = submission.ExtractedFolderPath,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        try
        {
            process.Start();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            result.CompilationSuccess = process.ExitCode == 0;
            if (!result.CompilationSuccess)
                result.ErrorMessage = error;
        }
        catch (Exception ex)
        {
            result.CompilationSuccess = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}
