using System;
using System.Collections.Generic;
using System.IO;
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
using System.Security.Cryptography;

namespace Lumpix.Emulated.PC.Pages
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            PasswordCheck(sender, e);
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordCheck(sender, e);
            }
        }
        private void PasswordCheck(object sender, RoutedEventArgs e)
        {
            bool new_pass = false;
            string cor_password = null;
            if (File.Exists("password.txt"))
            {
                cor_password = File.ReadAllText("password.txt");
            }
            else
            {
                cor_password = getHashSha256("1234");
                new_pass = true;
                File.WriteAllText("password.txt", cor_password);
            }
            cor_password = File.ReadAllText("password.txt");
            string password = PasswordTextBox.Password;
            if (getHashSha256(password) == cor_password)
            {
                if (new_pass)
                {
                    NavigationService.Navigate(new New_pass());
                }
            }
            else
            {
                NavigationService.Navigate(new Wrong_Pass());
            }
        }
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
