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
        var window = new OpenAssignmentWindow();
        window.Show();
        this.Close();
    }


    private void MenuHelp_Click(object sender, RoutedEventArgs e)
    {
        var helpWindow = new HelpWindow();
        helpWindow.Owner = this;
        helpWindow.ShowDialog();
    }



    private void MenuAbout_Click(object sender, RoutedEventArgs e)
    {
        var aboutWindow = new AboutWindow();
        aboutWindow.Owner = this;
        aboutWindow.ShowDialog();
    }


    
}