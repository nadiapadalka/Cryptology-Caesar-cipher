using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

using System.Security.Cryptography;
namespace Cryptology_1_lab
{
    
    public partial class MainWindow : Window
    {
        
        string filename = "";
        string alfabet = "";
        int key = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void PrintButton_Click(object sender, EventArgs e)
        {
           //this should be implemented 
        }
        
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                textbox.Text = File.ReadAllText(openFileDialog.FileName);
            textbox.Visibility = System.Windows.Visibility.Visible;

            getfilename(openFileDialog.FileName);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void getfilename(string path)
        {
            filename = path;
        }
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, textbox.Text);
            getfilename( saveFileDialog.FileName);
            textbox.Text = filename;
        }
        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            textbox.Visibility = System.Windows.Visibility.Visible;
        }


        

        private void About_btn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developer:Nadiia Padalka.Year:2020");
        }

        private void Close_btn(object sender, RoutedEventArgs e)
        {

        }
        public class CaesarCipher
        {
            //символи української абетки
            //const string alfabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";

            private string CodeEncode(string text, int k,string alfabet)
            {
                //додаємо до алфавіту малі літери
                var fullAlfabet = alfabet + alfabet.ToLower();
                var letterQty = fullAlfabet.Length;
                var retVal = "";
                for (int i = 0; i < text.Length; i++)
                {
                    var c = text[i];
                    var index = fullAlfabet.IndexOf(c);
                    if (index < 0)
                    {
                        //якщо літеру не знайдено, додаємо її незмінною
                        retVal += c.ToString();
                    }
                    else
                    {
                        var codeIndex = (letterQty + index + k) % letterQty;
                        retVal += fullAlfabet[codeIndex];
                    }
                }

                return retVal;
            }

            //шифрування тексту
            public string Encrypt(string plainMessage, int key,string alfabet)
                => CodeEncode(plainMessage, key,alfabet);

             public string Decrypt(string encryptedMessage, int key, string alfabet)
                => CodeEncode(encryptedMessage, -key,alfabet);

        }
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {

            var cipher = new CaesarCipher();

            textbox.Text = cipher.Decrypt(Encrypted_text.Text, key,alfabet);
            TextLabel.Content = "Decrypted text";
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(alfabet + " " + textbox.Text);
            var cipher = new CaesarCipher();
            Encrypted_text.Text =  cipher.Encrypt(textbox.Text, key,alfabet);
        }
        private void eng_btn_Click(object sender, RoutedEventArgs e)
        {
            Language_Box.Text = "English";
            To_TextBox.Text = "26";
            alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            From_TextBox.Text = "1";



        }
        private void ukr_btn_Click(object sender, RoutedEventArgs e)
        {
            alfabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯ";
           
            Language_Box.Text = "Ukrainian";
            To_TextBox.Text = "33";
            From_TextBox.Text = "1";
        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

       

        private void TextBox_TextChanged_2(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
            key = Int32.Parse(Key_Box.Text);
            var valid = new Uri(@"images/valid.png", UriKind.Relative);
            var not_valid = new Uri(@"images/notvalid.png", UriKind.Relative);
            //if ((key  >= 1) && (key < Int32.Parse(To_TextBox.Text)))
            //{ validation_img.Source = new BitmapImage(valid); }
            //else
            //{ validation_img.Source = new BitmapImage(not_valid); }
        }
    }
}  
    

