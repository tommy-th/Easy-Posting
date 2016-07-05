using FirstFloor.ModernUI.Windows.Controls;
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
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;

namespace Easy_Posting.Content
{
    /// <summary>
    /// Interaction logic for account.xaml
    /// </summary>
    public partial class account : UserControl
    {
        App thisApp = App.Current as App;
        MessageBoxButton btn = MessageBoxButton.OK;
        string blog_ad = "";
        string user_id = "";
        string user_password = "";
        string blogapi_id = "";
        string api_key = "";
        string blog_service = "";

        public account()
        {
            InitializeComponent();
        }

        static void EncryptFile(string sInputFilename,
         string sOutputFilename,
         string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        static void DecryptFile(string sInputFilename,
         string sOutputFilename,
         string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
            cryptostreamDecr.Close();
            fsread.Close();
        } 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ModernDialog
            {
                Title = "계정 추가",
                Content = new account_insert(),
            };
            wnd.CloseButton.Visibility = Visibility.Hidden;
            wnd.ShowDialog();

            try
            {
                string[] load_ad = new string[100];
                int i = 0;
                StreamReader file = new StreamReader("id_list.txt");

                account_list.Items.Clear();
                while (file.Peek() > -1)
                {
                    load_ad[i] = file.ReadLine();
                    account_list.Items.Add(load_ad[i]);
                    i++;

                    if (i < 2)
                        account_del.IsEnabled = false;
                    else
                        account_del.IsEnabled = true;
                }
                file.Close();
                thisApp.list_refresh = true;
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //계정 삭제
            try
            {
                string del_blog_ad = account_list.SelectedValue.ToString();
                if (del_blog_ad.Contains("/") == true)
                    del_blog_ad = del_blog_ad.Replace("/", "_");

                account_list.Items.RemoveAt(account_list.SelectedIndex);
                
                string[] load_ad = new string[100];
                int i = 0;

                StreamReader check_del = new StreamReader("id_list.txt");
                while (check_del.Peek() > -1)
                {
                    load_ad[i] = check_del.ReadLine();
                    if (load_ad[i] == del_blog_ad)
                    {
                        FileInfo del = new FileInfo(del_blog_ad + ".txt");
                        del.Delete();
                        load_ad[i] = null;
                        i = i - 1;
                    }
                    i++;                   
                }
                check_del.Close();

                StreamWriter rewrite_del = new StreamWriter("id_list.txt");
                for (int j = 0; j < i; j++)
                {
                    rewrite_del.WriteLine(load_ad[j]);
                }
                rewrite_del.Close();


                StreamReader file = new StreamReader("id_list.txt");
                string[] reload_ad = new string[100];
                int k = 0;
                account_list.Items.Clear();
                while (file.Peek() > -1)
                {
                    reload_ad[k] = file.ReadLine();
                    account_list.Items.Add(reload_ad[k]);
                    k++;

                    if (i < 2)
                        account_del.IsEnabled = false;
                    else
                        account_del.IsEnabled = true;
                }
                file.Close();
                thisApp.list_refresh = true;
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //블로그 보기
            try
            {
                string tmp_blog_ad;
                string go_blog_ad="";
                tmp_blog_ad = account_list.SelectedItem.ToString();
                if (tmp_blog_ad.Contains("/") == true)
                    tmp_blog_ad = tmp_blog_ad.Replace("/", "_");

                DecryptFile(tmp_blog_ad + ".txt", tmp_blog_ad + "_de.txt", thisApp.sSecretKey);
                StreamReader file = new StreamReader(tmp_blog_ad + "_de.txt");

                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        go_blog_ad = file.ReadLine();
                        thisApp.now_blog_ad = blog_ad;
                    }             
                }
                file.Close();

                EncryptFile(tmp_blog_ad + "_de.txt", tmp_blog_ad + ".txt", thisApp.sSecretKey);

                FileInfo del = new FileInfo(tmp_blog_ad + "_de.txt");
                del.Delete();
                
                System.Diagnostics.Process.Start(go_blog_ad);

            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {                
                //계정 선택
                if (account_list.SelectedItem != null)
                {
                    string tmp_blog_ad;
                    tmp_blog_ad = account_list.SelectedItem.ToString();
                    thisApp.now_select_id_item = tmp_blog_ad;
                    if (tmp_blog_ad.Contains("/") == true)
                        tmp_blog_ad = tmp_blog_ad.Replace("/", "_");

                    DecryptFile(tmp_blog_ad + ".txt", tmp_blog_ad + "_de.txt", thisApp.sSecretKey);
                    StreamReader file = new StreamReader(tmp_blog_ad + "_de.txt");

                    for (int i = 0; i < 6; i++)
                    {
                        if (i == 0)
                        {
                            blog_ad = file.ReadLine();
                            thisApp.now_blog_ad = blog_ad;
                        }
                        else if (i == 1)
                        {
                            user_id = file.ReadLine();
                            thisApp.now_id = user_id;
                        }
                        else if (i == 2)
                        {
                            user_password = file.ReadLine();
                            thisApp.now_password = user_password;
                        }
                        else if (i == 3)
                        {
                            blogapi_id = file.ReadLine();
                            thisApp.now_api_id = blogapi_id;
                        }
                        else if (i == 4)
                        {
                            api_key = file.ReadLine();
                            thisApp.now_api_key = api_key;
                        }
                        else if (i == 5)
                        {
                            blog_service = file.ReadLine();
                            thisApp.now_blog_service = blog_service;
                        }
                    }
                    file.Close();

                    EncryptFile(tmp_blog_ad + "_de.txt", tmp_blog_ad + ".txt", thisApp.sSecretKey);

                    FileInfo del = new FileInfo(tmp_blog_ad + "_de.txt");
                    del.Delete();
                    thisApp.check_account_select = true;
                    thisApp.list_refresh = true;
                    ModernDialog.ShowMessage("계정이 선택되었습니다.", "알림", btn);
                }
                else
                    ModernDialog.ShowMessage("계정을 선택해 주세요", "알림", btn);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] load_ad = new string[100];
                int i = 0;
                StreamReader file = new StreamReader("id_list.txt");

                account_list.Items.Clear();
                while (file.Peek() > -1)
                {
                    load_ad[i] = file.ReadLine();
                    if (load_ad[i].Contains("_") == false)
                        account_list.Items.Add(load_ad[i]);
                    else
                    {
                        load_ad[i] = load_ad[i].Replace("_", "/");
                        account_list.Items.Add(load_ad[i]);
                    }
                    i++;

                    if (i < 2)
                        account_del.IsEnabled = false;
                    else
                        account_del.IsEnabled = true;
                }
                file.Close();

            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }       
        }
    }
}
