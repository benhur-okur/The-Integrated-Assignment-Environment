using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace The_Integrated_Assignment_Environment
{
    public partial class HelpWindow : Window
    {
        private class HelpPage
        {
            public string ImagePath { get; set; }
            public string Description { get; set; }
        }

        private List<HelpPage> helpPages;
        private int currentPageIndex = 0;

        public HelpWindow()
        {
            InitializeComponent();
            InitializeHelpPages();
            DisplayPage();
        }

       private void InitializeHelpPages()
{
    helpPages = new List<HelpPage>
    {
        new HelpPage
        {
            ImagePath = "Resources/help1.png",
            Description = "'Create Assignment' stands for creating a new blank assignment project based on configuration templates, while 'Open Assignment' allows you to open a previously saved project along with its submission results."
        },
        new HelpPage
        {
            ImagePath = "Resources/help2.png",
            Description = "In the assignment creation screen, you can enter a name for your assignment, choose from predefined configurations, and select the folder containing student submissions in .ZIP format. You also need to select the expected output file (e.g., sample output.txt). Arguments to pass to the program during runtime can be written into the 'Arguments to Main' box. You can manage your configurations from the sidebar menu."
        },
        new HelpPage
        {
            ImagePath = "Resources/help3.png",
            Description = "The Configuration Manager shows all available language configurations. Each configuration includes a language name, a compile command, and a run command. You can import/export these configurations as JSON, or add/edit/delete entries individually."
        },
        new HelpPage
        {
            ImagePath = "Resources/help4.png",
            Description = "When adding or editing a configuration, placeholders like {filename} and {file} are critical:\n• {filename} represents the full filename (e.g., main.java).\n• {file} represents the base name without extension (e.g., main).\nFor example, the compile command for Java might be: javac {filename}, and the run command: java {file}."
        },
        new HelpPage
        {
            ImagePath = "Resources/help5.png",
            Description = "Once process assignment button clicked, each student's submission is compiled and executed. The report screen shows detailed status for each student: whether compilation succeeded, whether execution completed without error, and whether the output matched the expected output file. You can optionally re-select the submission folder or output file before re-processing."
        },
        new HelpPage
        {
            ImagePath = "Resources/help6.png",
            Description = "From the 'Open Assignment' screen, previously saved projects are listed. Each card shows the project name, programming language, and total number of processed submissions. Double-clicking or selecting and pressing 'Open' will load the assignment report and saved results instantly, without needing to reprocess."
        }
    };
}



        private void DisplayPage()
        {
            var page = helpPages[currentPageIndex];
            imgHelp.Source = new BitmapImage(new System.Uri(page.ImagePath, System.UriKind.Relative));
            txtDescription.Text = page.Description;
            lblPageInfo.Text = $" Page {currentPageIndex + 1}/{helpPages.Count} ";

            btnPrev.Visibility = currentPageIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            btnNext.Visibility = currentPageIndex == helpPages.Count - 1 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                DisplayPage();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex < helpPages.Count - 1)
            {
                currentPageIndex++;
                DisplayPage();
            }
        }
    }
}
