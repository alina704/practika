using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WindowChrome.Demo.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookRead.xaml
    /// </summary>
    public partial class BookRead : Page
    {
        private string pathfile;
        public BookRead(string pathfile)
        {
            this.pathfile = pathfile;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var docFolder = Environment.GetFolderPath(
        Environment.SpecialFolder.MyDocuments,
        Environment.SpecialFolderOption.Create);

            var path = Path.Combine(docFolder, pathfile);
            string[] textArray = File.ReadAllLines(path);
            string text = "";

            TextBlock textBlock = (TextBlock)FindName("textBlock");

            foreach (string s in textArray)
            {
                text += "\n" + s;
            }

            textBlock.Text = text;
        }
    }
}
