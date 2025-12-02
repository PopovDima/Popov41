using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Popov41размер
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = TBoxLogin.Text;
            string password = TBoxPassword.Text;
            if (login == "" ||  password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }

            User user = Popov41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if (user != null)
            {
                Manager.MainFrame.Navigate(new ProductPage());
                TBoxLogin.Text = "";
                TBoxPassword.Text = "";
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
                BtnLogin.IsEnabled = false;
                BtnLogin.IsEnabled = true;
            }
        }
    }
}
