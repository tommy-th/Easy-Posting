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
    /// Interaction logic for account_insert.xaml
    /// </summary>
    public partial class account_insert : UserControl
    {
        App thisApp = App.Current as App;
        MessageBoxButton btn = MessageBoxButton.OK;

        public account_insert()
        {
            InitializeComponent();
            service.Items.Add("Tistory");
            service.Items.Add("Naver");
            service.Items.Add("WordPress");
            service.SelectedIndex = 0;
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

        static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;

            if (service.SelectedValue.ToString() == "Tistory" && blog_ad.Text != "" && id.Text != "" && password.Password != "")
            {
                Post[] check_login= new Post[1];
                //계정 정보 파일 암호화
                if (thisApp.sSecretKey == "")
                {
                    StreamReader key = new StreamReader("key.txt");
                    thisApp.sSecretKey = key.ReadLine();
                    key.Close();
                    if (thisApp.sSecretKey == null)
                    {
                        thisApp.sSecretKey = GenerateKey();
                    }
                    StreamWriter wr_key = new StreamWriter("key.txt");
                    wr_key.WriteLine(thisApp.sSecretKey);
                    wr_key.Close();

                }

                string[] strarray = blog_ad.Text.Split('/');
                if (thisApp.check_first == true)
                {
                    thisApp.check_first = false;
                    thisApp.first_id = strarray[2];
                }

                StreamReader check_id = new StreamReader("id_list.txt");
                string[] load_ad = new string[100];
                bool check_id_flag = false;
                int i = 0;
                while (check_id.Peek() > -1)
                {
                    load_ad[i] = check_id.ReadLine();
                    if (load_ad[i] == strarray[2])
                    {
                        ModernDialog.ShowMessage("이미 존재하는 계정입니다.", "알림", btn);
                        check_id_flag = true;
                        break;
                    }
                    i++;
                }
                i = 0;
                check_id.Close();

                if (check_id_flag == false)
                {
                    MetaWeblog api = new MetaWeblog("http://" + strarray[2] + "/api");
                    try
                    {
                        check_login = api.getRecentPosts(Api_id.Text, id.Text, password.Password, 1);
                        StreamWriter file = new StreamWriter(strarray[2] + "_un.txt");
                        StreamWriter id_list = new StreamWriter("id_list.txt", true);

                        file.WriteLine(blog_ad.Text);
                        file.WriteLine(id.Text);

                        id_list.WriteLine(strarray[2]);

                        file.WriteLine(password.Password);
                        file.WriteLine(Api_id.Text);
                        file.WriteLine(Api.Text);
                        file.WriteLine(service.SelectedItem.ToString());

                        thisApp.blog_ad = strarray[2];
                        file.Close();
                        id_list.Close();
                        parentWindow.Close();
                    }
                    catch
                    {
                        ModernDialog.ShowMessage("잘못된 계정 정보입니다.", "알림", btn);
                    }

                    EncryptFile(strarray[2] + "_un.txt", strarray[2] + ".txt", thisApp.sSecretKey);

                    FileInfo del = new FileInfo(strarray[2] + "_un.txt");
                    del.Delete();
                }
            }
            else if (service.SelectedValue.ToString() == "Naver" && blog_ad.Text != "" && id.Text != "" && password.Password != "")
            {
                Post[] check_login= new Post[1];
                //계정 정보 파일 암호화
                if (thisApp.sSecretKey == "")
                {
                    StreamReader key = new StreamReader("key.txt");
                    thisApp.sSecretKey = key.ReadLine();
                    key.Close();
                    if (thisApp.sSecretKey == null)
                    {
                        thisApp.sSecretKey = GenerateKey();
                    }
                    StreamWriter wr_key = new StreamWriter("key.txt");
                    wr_key.WriteLine(thisApp.sSecretKey);
                    wr_key.Close();

                }

                string[] strarray = blog_ad.Text.Split('/');
                if (thisApp.check_first == true)
                {
                    thisApp.check_first = false;
                    thisApp.first_id = strarray[2]+"_"+strarray[3];
                }

                StreamReader check_id = new StreamReader("id_list.txt");
                string[] load_ad = new string[100];
                bool check_id_flag = false;
                int i = 0;
                while (check_id.Peek() > -1)
                {
                    load_ad[i] = check_id.ReadLine();
                    if (load_ad[i] == strarray[2] + "_" + strarray[3])
                    {
                        ModernDialog.ShowMessage("이미 존재하는 계정입니다.", "알림", btn);
                        check_id_flag = true;
                        break;
                    }
                    i++;
                }
                i = 0;
                check_id.Close();

                if (check_id_flag == false)
                {                   
                    MetaWeblog api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                    try
                    {
                        check_login = api.getRecentPosts(Api_id.Text, id.Text, Api.Text, 1);

                        StreamWriter file = new StreamWriter(strarray[2] + "_" + strarray[3] + "_un.txt");
                        StreamWriter id_list = new StreamWriter("id_list.txt", true);

                        file.WriteLine(blog_ad.Text);
                        file.WriteLine(id.Text);

                        id_list.WriteLine(strarray[2] + "_" + strarray[3]);

                        file.WriteLine(password.Password);
                        file.WriteLine(Api_id.Text);
                        file.WriteLine(Api.Text);
                        file.WriteLine(service.SelectedItem.ToString());

                        thisApp.blog_ad = strarray[2] + "_" + strarray[3];
                        file.Close();
                        id_list.Close();
                        parentWindow.Close();
                    }
                    catch
                    {
                        ModernDialog.ShowMessage("잘못된 계정 정보입니다.", "알림", btn);
                    }

                    EncryptFile(strarray[2] + "_" + strarray[3] + "_un.txt", strarray[2] + "_" + strarray[3] + ".txt", thisApp.sSecretKey);

                    FileInfo del = new FileInfo(strarray[2] + "_" + strarray[3] + "_un.txt");
                    del.Delete();
                }
            }
            else if (service.SelectedValue.ToString() == "WordPress" && blog_ad.Text != "" && id.Text != "" && password.Password != "")
            {
                Post[] check_login = new Post[1];
                //계정 정보 파일 암호화
                if (thisApp.sSecretKey == "")
                {
                    StreamReader key = new StreamReader("key.txt");
                    thisApp.sSecretKey = key.ReadLine();
                    key.Close();
                    if (thisApp.sSecretKey == null)
                    {
                        thisApp.sSecretKey = GenerateKey();
                    }
                    StreamWriter wr_key = new StreamWriter("key.txt");
                    wr_key.WriteLine(thisApp.sSecretKey);
                    wr_key.Close();

                }

                string[] strarray = blog_ad.Text.Split('/');
                if (thisApp.check_first == true)
                {
                    thisApp.check_first = false;
                    thisApp.first_id = strarray[2];
                }

                StreamReader check_id = new StreamReader("id_list.txt");
                string[] load_ad = new string[100];
                bool check_id_flag = false;
                int i = 0;
                while (check_id.Peek() > -1)
                {
                    load_ad[i] = check_id.ReadLine();
                    if (load_ad[i] == strarray[2])
                    {
                        ModernDialog.ShowMessage("이미 존재하는 계정입니다.", "알림", btn);
                        check_id_flag = true;
                        break;
                    }
                    i++;
                }
                i = 0;
                check_id.Close();

                if (check_id_flag == false)
                {                  
                    MetaWeblog api = new MetaWeblog(blog_ad.Text+"/xmlrpc.php");
                    try
                    {
                        check_login = api.getRecentPosts("", id.Text, password.Password, 1);

                        StreamWriter file = new StreamWriter(strarray[2] + "_un.txt");
                        StreamWriter id_list = new StreamWriter("id_list.txt", true);

                        file.WriteLine(blog_ad.Text);
                        file.WriteLine(id.Text);

                        id_list.WriteLine(strarray[2]);

                        file.WriteLine(password.Password);
                        file.WriteLine(Api_id.Text);
                        file.WriteLine(Api.Text);
                        file.WriteLine(service.SelectedItem.ToString());

                        thisApp.blog_ad = strarray[2];
                        file.Close();
                        id_list.Close();
                        parentWindow.Close();
                    }
                    catch
                    {
                        ModernDialog.ShowMessage("잘못된 계정 정보입니다.", "알림", btn);
                    }

                    EncryptFile(strarray[2] + "_un.txt", strarray[2] + ".txt", thisApp.sSecretKey);

                    FileInfo del = new FileInfo(strarray[2] + "_un.txt");
                    del.Delete();
                }
            }
            else
                ModernDialog.ShowMessage("계정 정보를 모두 입력해 주세요.", "알림", btn);
        }

        private void service_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (service.SelectedValue.ToString() == "WordPress")
            {
                Api_id.IsEnabled = false;
                Api_id.Text = "입력 불필요";
                Api.IsEnabled = false;
                Api.Text = "입력 불필요";
            }
            else
            {
                Api_id.IsEnabled = true;
                Api.IsEnabled = true;
                Api_id.Text = "";
                Api.Text = "";
            }
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            parentWindow.Close();
        }
    }
}
