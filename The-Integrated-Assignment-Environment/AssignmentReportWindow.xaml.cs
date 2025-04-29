using System.Windows;

namespace The_Integrated_Assignment_Environment;

public partial class AssignmentReportWindow : Window
{
    public AssignmentReportWindow()
    {
        InitializeComponent();

    }

    private void btnSelectZipDirectory_Click(object sender, RoutedEventArgs e)
    {
        // TODO: ZIP klasör seçimi yapılacak (şu anda boş)
    }

    private void btnProcessAssignments_Click(object sender, RoutedEventArgs e)
    {
        // TODO: ZIP dosyalarını işleyip sonuçları gösterecek (şu anda boş)
    }




    public class AssignmentResult
    {
        public string StudentId { get; set; }
        public string CompileStatus { get; set; }
        public string RunStatus { get; set; }
        public string OutputMatch { get; set; }
    }
}