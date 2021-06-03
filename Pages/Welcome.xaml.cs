using System;
using System.Windows;
using System.Windows.Controls;
using WindowChrome.Demo.Pages;

namespace WindowChrome.Demo
{
    /// <summary>
    /// Логика взаимодействия для Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {

            InitializeComponent();
        }

        private void SingUp_Click(object sender, RoutedEventArgs e)
        {
            if (App.RegistrationRef == null)
            {
                App.RegistrationRef = new Registration();
            }
            App.ParentWindowRef.ParentFrame.Navigate(App.RegistrationRef);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = ((TextBox)FindName("usernameTextBox")).Text;
            string password = ((TextBox)FindName("passwordTextBox")).Text;
            if (username.Equals("") || password.Equals(""))
            {
                ((Label)FindName("errorLoginLabel")).Visibility = Visibility.Visible;
                return;
            }
            try
            {
                App.ParentWindowRef.ParentFrame.Navigate(new BooksAccount(username, password));
            }
            catch (UserNotFoundException)
            {
                ((Label)FindName("errorLoginLabel")).Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                ((Label)FindName("errorLoginLabel")).Content = "Произошли технические неполадки. Повторите вход позднее.";
                ((Label)FindName("errorLoginLabel")).Visibility = Visibility.Visible;
            }
        }
    }
}
