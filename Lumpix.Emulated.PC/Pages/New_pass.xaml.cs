using System;
using System.Collections.Generic;
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
using System.IO;


namespace Lumpix.Emulated.PC.Pages
{
    /// <summary>
    /// Interaktionslogik für New_pass.xaml
    /// </summary>
    public partial class New_pass : Page
    {
        public New_pass()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewPasswordTextBox.Password == RepeatNewPasswordTextBox.Password)
            {
                string new_pass = getHashSha256(NewPasswordTextBox.Password);
                File.WriteAllText("password.txt", new_pass);
            }
            else
            {
                Error.Visibility = Visibility.Visible;
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
