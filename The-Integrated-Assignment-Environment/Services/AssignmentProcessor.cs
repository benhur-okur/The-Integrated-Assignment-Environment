using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using The_Integrated_Assignment_Environment.Models;

namespace The_Integrated_Assignment_Environment.Services;

public class AssignmentProcessor
{
    private readonly CompilerService compiler = new();
    private readonly RunService runner = new();

    public List<Result> ProcessAll(Project project)
    {
        List<Result> results = new();
        string[] zipFiles = Directory.GetFiles(project.SubmissionsFolderPath, "*.zip");

        foreach (var zipFile in zipFiles)
        {
            Console.WriteLine($"[AssignmentProcessor] Processing zip: {zipFile}");
            string studentId = Path.GetFileNameWithoutExtension(zipFile);
            string extractPath = Path.Combine(project.SubmissionsFolderPath, studentId);

            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);

            ZipFile.ExtractToDirectory(zipFile, extractPath);

            var sourceFile = Directory.GetFiles(extractPath, "*.*", SearchOption.AllDirectories)
                .FirstOrDefault();

            if (sourceFile == null)
            {
                Console.WriteLine($"[AssignmentProcessor] No source file found in: {extractPath}");
                results.Add(new Result
                {
                    StudentId = studentId,
                    CompilationSuccess = false,
                    ExecutionSuccess = false,
                    OutputMatch = false,
                    ErrorMessage = "No source file found"
                });
                continue;
            }

            Console.WriteLine($"[AssignmentProcessor] Found source file: {sourceFile}");

            var submission = new StudentSubmission
            {
                StudentId = studentId,
                ExtractedFolderPath = extractPath,
                SourceFilePath = sourceFile
            };

            project.Submissions.Add(submission);

            var result = compiler.Compile(submission, project.Configuration);

            if (result.CompilationSuccess)
            {
                result = runner.Run(submission, project, result); 
            }

            project.Results.Add(result);
            results.Add(result);
        }

        return results;
    }
}
