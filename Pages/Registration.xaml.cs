using BooksReader.App;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WindowChrome.Demo.Pages;

namespace WindowChrome.Demo
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            //App.ParentWindowRef.ParentFrame.Navigate(App.WelcomeRef);
            NavigationService.GoBack();
        }



        private void SingupButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                firstname = ((TextBox)FindName("firstnameTextBox")).Text,
                lastname = ((TextBox)FindName("lastnameTextBox")).Text,
                patronymic = ((TextBox)FindName("patronymicTextBox")).Text,
                username = ((TextBox)FindName("usernameTextBox")).Text,
                password = ((TextBox)FindName("passwordTextBox")).Text
            };

            if (user.firstname.Equals("") ||
                user.lastname.Equals("") ||
                user.patronymic.Equals("") ||
                user.username.Equals("") ||
                user.password.Equals(""))
            {
                ((Label)FindName("errorLabel")).Content = "Заполните все поля для регистрации.";
                ((Label)FindName("errorLabel")).Visibility = Visibility.Visible;
                return;
            }
            try
            {
                SaveNewUser(user);
                App.ParentWindowRef.ParentFrame.Navigate(new BooksAccount(user.username, user.password));
                App.RegistrationRef = null;
            }
            catch (NameAlreadyExistsException)
            {
                ((Label)FindName("errorLabel")).Content = "Такой же логин уже существует.";
                ((Label)FindName("errorLabel")).Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                ((Label)FindName("errorLabel")).Content = "Произошли технические неполадки. Повторите регистрацию позднее.";
                ((Label)FindName("errorLabel")).Visibility = Visibility.Visible;
            }
        }

        private void SaveNewUser(User user)
        {
            using (EntityContext db = new EntityContext())
            {
                if (UserExists(db, user.username))
                    throw new NameAlreadyExistsException("This username already exists.");

                db.Users.AddRange(user);
                db.SaveChanges();
            }
        }

        private bool UserExists(EntityContext entityContext, string username)
        {
            int id = 0;
            id = entityContext.Users.
                Where(u => u.username == username).
                Select(u => u.Id).
                SingleOrDefault();
            if (id != 0)
            {
                return true;
            }
            else return false;
        }
    }
}
