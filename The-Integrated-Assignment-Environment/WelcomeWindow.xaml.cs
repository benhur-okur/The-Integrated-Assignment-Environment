using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace The_Integrated_Assignment_Environment;

public partial class WelcomeWindow : Window
{
    public WelcomeWindow()
    {
        InitializeComponent();
    }

    private void btnCreateAssignment_Click(object sender, RoutedEventArgs e)
    {
        CreateAssignmentWindow createWindow = new CreateAssignmentWindow();
        createWindow.Show();
        this.Close(); // İstersen sadece Hide() da yapabilirsin
    }

    private void btnOpenAssignment_Click(object sender, RoutedEventArgs e)
    {
        // TODO: "Open Assignment" ekranını açma logic’i
    }
        
    private void MenuHelp_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Yardım içeriğini göster
    }

    private void MenuAbout_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Hakkında penceresini aç
    }

    
}
