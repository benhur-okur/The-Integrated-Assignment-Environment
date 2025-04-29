using System.Windows;

namespace The_Integrated_Assignment_Environment;

public partial class CreateAssignmentWindow : Window
{
    public CreateAssignmentWindow()
    {
        InitializeComponent();
    }

    private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
    {
        // TODO: FolderBrowserDialog kullanarak klasör seçimi
    }

    private void btnSelectExpectedOutput_Click(object sender, RoutedEventArgs e)
    {
        // TODO: OpenFileDialog ile beklenen output dosyası seçimi
    }

    private void btnSaveAssignment_Click(object sender, RoutedEventArgs e)
    {
        AssignmentReportWindow reportWindow = new AssignmentReportWindow();
        reportWindow.Show();
        this.Close();
    }
        
}