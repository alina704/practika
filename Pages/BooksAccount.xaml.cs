using BooksReader.App;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowChrome.Demo.Pages
{
    /// <summary>
    /// Логика взаимодействия для BooksAccount.xaml
    /// </summary>
    public partial class BooksAccount : Page
    {
        private ListView booksList;
        private ListView tempBooksList;
        public BooksAccount(string username, string password)
        {
            using (EntityContext db = new EntityContext())
            {
                int id = 0;
                id = db.Users.
                    Where(u => u.username == username && u.password == password).
                    Select(u => u.Id).
                    SingleOrDefault();
                if (id == 0)
                {
                    throw new UserNotFoundException("User not found in data base.");
                }
            }
            InitializeComponent();
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new Welcome());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            booksList = (ListView)FindName("booksListListView");
            tempBooksList = new ListView();
            using (EntityContext db = new EntityContext())
            {
                var dataset = db.Books.
                    Select(u => new BookView { author = u.author, name = u.name, genre = u.genre }).
                    ToList();

                for (int i = 0; i < dataset.Count; i++)
                {
                    booksList.Items.Add(dataset[i]);
                    tempBooksList.Items.Add(dataset[i]);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            string keyWord = ((TextBox)FindName("keyWordTextBox")).Text;
            if (keyWord.Equals(""))
            {
                booksList.Items.Clear();
                booksList.Items.Add(tempBooksList);
                return;
            }

            for (int i = 0; i < booksList.Items.Count; i++)
            {
                BookView item = (BookView)booksList.Items[i];
                if (!item.name.Equals(keyWord))
                {
                    booksList.Items.RemoveAt(i);
                }
            }
        }

        private void booksListListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                var content = item.Content as BookView;
                string path = "";
                using (EntityContext db = new EntityContext())
                {

                    path = db.Books.
                        Where(u => u.author == content.author && u.name == content.name && u.genre == content.genre).
                        Select(u => u.path).
                        SingleOrDefault();
                }
                try
                {
                    App.ParentWindowRef.ParentFrame.Navigate(new BookRead(path));
                }
                catch (DirectoryNotFoundException)
                {

                }
                
            }
        }

        private void Page_Initialized(object sender, System.EventArgs e)
        {
            booksList = (ListView)FindName("booksListListView");
            tempBooksList = new ListView();
            using (EntityContext db = new EntityContext())
            {
                var dataset = db.Books.
                    Select(u => new BookView { author = u.author, name = u.name, genre = u.genre }).
                    ToList();

                for (int i = 0; i < dataset.Count; i++)
                {
                    booksList.Items.Add(dataset[i]);
                    tempBooksList.Items.Add(dataset[i]);
                }
            }
        }
    }
}
