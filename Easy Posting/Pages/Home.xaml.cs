using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Configuration;
using CookComputing.XmlRpc;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.Windows.Threading;

namespace Easy_Posting.Content
{    
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        string[] check_idnull = new string[100];        
        string blog_ad = "";
        string user_id = "";
        string user_password = "";
        string blogapi_id = "";
        string api_key = "";
        string blog_service = "";
        string test = "";
        List<string> youtube_url = null;
        List<string> youtube_thum_url = null;
        List<string> str_map = null;
        List<string> ch_str_map = null;
        bool check_private = true;
        bool check_test = false;
        bool check_test1 = false;
        DispatcherTimer styleTimer;
        MessageBoxButton btn = MessageBoxButton.OK;
        App thisApp = App.Current as App;

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

        public Home()
        {
            InitializeComponent();
            InitContainer();
            InitTimer();
            youtube_url = new List<string>();
            youtube_thum_url = new List<string>();
            str_map = new List<string>();
            ch_str_map = new List<string>();

            thisApp.first_skin = false;
            if (thisApp.first_skin == false)
            {
                thisApp.first_skin = true;
                thisApp.first_list = true;
                SettingsAppearanceViewModel skin = new SettingsAppearanceViewModel();
            }
            //document_map.IsEnabled = false;
            //VisualEditor2.Document.Title = "right";
            title.Text = "제목을 입력하세요.";

            Tags.Text = "Tag 입력( ,로 구별)";
          
        }

        void InitContainer()
        {
            VisualEditor2.Navigated += this.VisualEditor2_Navigated;
            VisualEditor2.DocumentText = @"<html><body style=""zoom:30%; overflow-x:hidden""></body></html>";            
        }

        void VisualEditor2_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            VisualEditor2.Document.ContextMenuShowing += this.DocumentContextMenuShowing;
            VisualEditor2.Document.Focus();
        }

        void DocumentContextMenuShowing(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {
            e.ReturnValue = false;
        }

        static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }             

        private void posting_bt(object sender, RoutedEventArgs e)
        {
            newPost();                        
        }

        public string GetFileName(string toSplit)
        {
            string[] splitted = toSplit.Split('\\');
            string fin = splitted[splitted.Length - 1];
            return fin;
        }

        private void image_insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MediaObjectUrl url;
                string[] strimage = new string[100];
                string[] html_image = new string[100];

                Microsoft.Win32.OpenFileDialog Dialog = new Microsoft.Win32.OpenFileDialog();
                Dialog.Title = "이미지 파일 추가";
                Dialog.ShowReadOnly = true;
                Dialog.Multiselect = true;
                Dialog.Filter = "Image Files (*.jpeg, *.png, *.jpg, *.gif)| *.jpeg; *.png; *.jpg; *.gif";

                if (Dialog.ShowDialog() == true)
                {
                    strimage = Dialog.FileNames;
                }

                if (strimage[0] != null)
                {
                    for (int i = 0; i < strimage.Length; i++)
                    {
                        string[] cut = new string[100];
                        int index = 0;
                        MetaWeblog api;

                        if (blog_service == "Tistory")
                        {
                            api = new MetaWeblog(blog_ad + "/api");
                            url = api.newMediaObject(blogapi_id, user_id, api_key, api.GetMediaObject(strimage[i]));

                            index = url.url.ToString().IndexOf("?tt");
                            string test = url.url.ToString();
                            char[] cuttest = new char[index];
                            for (int j = 0; j < index; j++)
                                cuttest[j] = test[j];

                            cut[i] = new string(cuttest);
                            html_image[i] = "<img src=" + "\"" + cut[i] + "\"" + "style=\"max-width:100%; height: auto;\"" + ">";
                        }
                        else if (blog_service == "Naver")
                        {
                            string str = "";
                            api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                            url = api.newMediaObject(blogapi_id, user_id, api_key, api.GetMediaObject(strimage[i]));
                            str = url.url.ToString();
                            html_image[i] = "<img src=" + "\"" + str + "\"" + "style=\"max-width:100%; height: auto;\"" + ">";
                        }
                        else if (blog_service == "WordPress")
                        {
                            string str = "";
                            api = new MetaWeblog(blog_ad + "/xmlrpc.php");
                            url = api.newMediaObject("1", user_id, user_password, api.GetMediaObject(strimage[i]));
                            str = url.url.ToString();
                            html_image[i] = "<img src=" + "\"" + str + "\"" + "style=\"max-width:100%; height: auto;\"" + ">";
                        }                                           

                        Editor.inputVisualEditor(html_image[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void file_insert_Click(object sender, RoutedEventArgs e)
        {
            string[] strfile = { "" };
            MediaObjectUrl url;
            string html_file = "";
            MetaWeblog api;
                    
            Microsoft.Win32.OpenFileDialog Dialog = new Microsoft.Win32.OpenFileDialog();
            Dialog.Title = "파일 첨부";
            Dialog.ShowReadOnly = true;
            Dialog.Multiselect = true;
            Dialog.Filter = "All Files (*.*)| *.*";

            try
            {       
                if (Dialog.ShowDialog() == true)
                {
                    strfile = Dialog.FileNames;
                }

                if (strfile[0] != "")
                {
                    for (int i = 0; i < strfile.Length; i++)
                    {
                        if (blog_service == "Tistory")
                        {
                            api = new MetaWeblog(blog_ad + "/api");
                            url = api.newMediaObject(blogapi_id, user_id, api_key, api.GetMediaObject(strfile[i]));
                            html_file += "<a href=" + "\"" + url.url.ToString() + "\"" + " style=\"max-width:100%; height: auto;\"" + ">" + GetFileName(strfile[i]) + "</a>\n";
                        }
                        else if (blog_service == "Naver")
                        {
                            api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                            url = api.newMediaObject(blogapi_id, user_id, api_key, api.GetMediaObject(strfile[i]));
                            html_file += "<a href=" + "\"" + url.url.ToString() + "\"" + " style=\"max-width:100%; height: auto;\"" + ">" + GetFileName(strfile[i]) + "</a>\n";
                        }
                        else if (blog_service == "WordPress")
                        {
                            api = new MetaWeblog(blog_ad + "/xmlrpc.php");
                            url = api.newMediaObject("1", user_id, user_password, api.GetMediaObject(strfile[i]));
                            html_file += "<a href=" + "\"" + url.url.ToString() + "\"" + " style=\"max-width:100%; height: auto;\"" + ">" + GetFileName(strfile[i]) + "</a>\n";
                        }                       
                    }
                    Editor.inputVisualEditor(html_file);
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            bool bImage = response.ContentType.StartsWith("image",
                StringComparison.OrdinalIgnoreCase);
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                bImage)
            {
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        } 

        private void map_insert_Click(object sender, RoutedEventArgs e)
        {
            String fileName = "a.png";

            var wnd = new ModernWindow
            {
                Style = (Style)App.Current.Resources["EmptyWindow"],
                Content = new map(),
            };
            wnd.ShowDialog();

            if (thisApp.check_mapuse == true)
            {
                thisApp.check_mapuse = false;

                thisApp.textStreet = System.Web.HttpUtility.UrlEncode(thisApp.textStreet);

                if (blog_service == "Tistory")
                {
                    string[] cut = new string[100];
                    int index = 0;
                    MetaWeblog api;
                    MediaObjectUrl url;
                    String mapurl = @"http://maps.googleapis.com/maps/api/staticmap?size=400x400&maptype=hybrid&zoom=16&markers=size:mid%7Ccolor:red%7C"
                        + thisApp.textStreet + @"&sensor=false";

                    if (!DownloadRemoteImageFile(mapurl, fileName))
                    {
                        ModernDialog.ShowMessage("Download Failed: " + mapurl, "알림", btn);
                    }

                    api = new MetaWeblog(thisApp.now_blog_ad + "/api");
                    url = api.newMediaObject(blogapi_id, user_id, api_key, api.GetMediaObject(fileName));
                    
                    index = url.url.ToString().IndexOf("?tt");
                    string test = url.url.ToString();
                    char[] cuttest = new char[index];
                    for (int j = 0; j < index; j++)
                        cuttest[j] = test[j];

                    cut[0] = new string(cuttest);


                    str_map.Add(@"<A href=" + @"""http://maps.google.com/maps?q=" + thisApp.textStreet + @""" target=_BLANK" +
                   @"><IMG style=""MAX-WIDTH: 100%; HEIGHT: auto"" src=""" + cut[0] + @"""> </A>");
                    ch_str_map.Add(@"<img src=""" + cut[0] + @"""style=""max-width:100%; height: auto;""" + "/></a>");
                    check_test1 = true;
                    FileInfo del = new FileInfo(fileName);
                    del.Delete();
                }                
                else if (blog_service == "Naver")
                {
                    MetaWeblog api;
                    MediaObjectUrl url;
                    String mapurl = @"http://maps.googleapis.com/maps/api/staticmap?size=400x400&maptype=hybrid&zoom=16&markers=size:mid%7Ccolor:red%7C"
                        + thisApp.textStreet + @"&sensor=false";
                                        
                    if (!DownloadRemoteImageFile(mapurl, fileName))
                    {
                        ModernDialog.ShowMessage("Download Failed: " + mapurl, "알림", btn);
                    }

                    string str = "";
                    api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                    url = api.newMediaObject(blogapi_id, user_id, api_key, api.GetMediaObject(fileName));
                    str = url.url.ToString();
                    str_map.Add(@"<A href=" + @"""http://maps.google.com/maps?q=" + thisApp.textStreet + @""" target=_BLANK" +
                   @"><IMG style=""MAX-WIDTH: 100%; HEIGHT: auto"" src="""+ str + @"""> </A>");
                    ch_str_map.Add(@"<img src=""" + str + @"""style=""max-width:100%; height: auto;""" + "/></a>");
                    check_test1 = true;
                    FileInfo del = new FileInfo(fileName);
                    del.Delete();
                }
                else if (blog_service == "WordPress")
                {
                    MetaWeblog api;
                    MediaObjectUrl url;
                    String mapurl = @"http://maps.googleapis.com/maps/api/staticmap?size=400x400&maptype=hybrid&zoom=16&markers=size:mid%7Ccolor:red%7C"
                        + thisApp.textStreet + @"&sensor=false";

                    if (!DownloadRemoteImageFile(mapurl, fileName))
                    {
                        ModernDialog.ShowMessage("Download Failed: " + mapurl, "알림", btn);
                    }

                    string str = "";
                    api = new MetaWeblog(thisApp.now_blog_ad + "/xmlrpc.php");
                    url = api.newMediaObject("1", user_id, user_password, api.GetMediaObject(fileName));
                    str = url.url.ToString();
                    str_map.Add(@"<A href=" + @"""http://maps.google.com/maps?q=" + thisApp.textStreet + @""" target=_BLANK" +
                   @"><IMG style=""MAX-WIDTH: 100%; HEIGHT: auto"" src=""" + str + @"""> </A>");
                    ch_str_map.Add(@"<img src=""" + str + @"""style=""max-width:100%; height: auto;""" + "/></a>");
                    check_test1 = true;
                    FileInfo del = new FileInfo(fileName);
                    del.Delete();                   
                }

                Editor.inputVisualEditor(str_map[str_map.Count-1]);              
            } 
        }

        public void newPost()
        {
            Post post = new Post();
            string upload_source = "";
            try
            {
                if (title.Text != "" && Editor.Document.Body.InnerHtml != null && Tags.Text != "")
                {
                    post.title = title.Text;
                    post.dateCreated = DateTime.UtcNow;

                    if (posting_date.SelectedDate.HasValue == true)
                    {
                        if (posting_date.SelectedDate == DateTime.Today)
                        {
                            post.dateCreated = DateTime.UtcNow;
                        }
                        else
                            post.dateCreated = (DateTime)posting_date.SelectedDate;
                    }
                    else
                        posting_date.DisplayDate = DateTime.UtcNow;

                    post.categories = new string[] { combo_category.SelectedValue.ToString() };
                    MetaWeblog api;

                    if (blog_service == "Tistory")
                    {
                        api = new MetaWeblog(blog_ad + "/api");
                        post.mt_keywords = Tags.Text;
                        upload_source = Editor.ContentHtml;

                        upload_source = upload_source.Replace("/v/", "/embed/");
                        post.description = upload_source;

                        api.newPost(blogapi_id, user_id, api_key, post, check_private);
                    }
                    else if (blog_service == "Naver")
                    {
                        api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                        post.tags = Tags.Text;
                        upload_source = Editor.ContentHtml;

                        upload_source = upload_source.Replace("/v/", "/embed/");
                        post.description = upload_source;

                        api.newPost(blogapi_id, user_id, api_key, post, check_private);
                    }
                    else if (blog_service == "WordPress")
                    {
                        api = new MetaWeblog(blog_ad + "/xmlrpc.php");
                        upload_source = Editor.ContentHtml;
                        post.mt_keywords = Tags.Text;
                        upload_source = upload_source.Replace("/v/", "/embed/");
                        post.description = upload_source;

                        api.newPost("1", user_id, user_password, post, check_private);
                    }

                    ModernDialog.ShowMessage("포스팅이 완료되었습니다.", "알림", btn);
                }
                else
                    ModernDialog.ShowMessage("포스팅 내용을 모두 입력해 주세요.", "알림", btn);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            check_private = false;            
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            check_private = true;
        }

        private void check_main_account(string id)
        {
            try
            {
                Category[] category;                
                string tmp_blog_ad;
                tmp_blog_ad = id;
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

                MetaWeblog api;

                if (blog_service == "Tistory")
                {
                    api = new MetaWeblog("http://" + tmp_blog_ad + "/api");
                    category = api.getCategories(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key);

                    combo_category.Items.Clear();
                    for (int j = 0; j < category.Length; j++)
                    {
                        combo_category.Items.Add(category[j].title);
                    }
                }
                else if (blog_service == "Naver")
                {
                    api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");                    
                    category = api.getCategories(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key);

                    combo_category.Items.Clear();
                    for (int j = 0; j < category.Length; j++)
                    {
                        combo_category.Items.Add(category[j].title);
                    }
                }
                else if (blog_service == "WordPress")
                {
                    api = new MetaWeblog(thisApp.now_blog_ad + "/xmlrpc.php");
                    category = api.getCategories("1", thisApp.now_id, thisApp.now_password);

                    combo_category.Items.Clear();
                    for (int j = 0; j < category.Length; j++)
                    {
                        combo_category.Items.Add(category[j].description);
                    }
                }

                Editor.Document.Body.InnerHtml = "";
                Editor.Document.Body.InnerText = "";
                VisualEditor2.Document.Body.InnerHtml = "";
                youtube_url = new List<string>();
                youtube_thum_url = new List<string>();
                str_map = new List<string>();
                ch_str_map = new List<string>();
                check_test = false;
                check_test1 = false;
                //ModernDialog.ShowMessage("계정이 선택되었습니다.", "알림", btn);
                combo_category.SelectedIndex = 0;
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
                styleTimer.Start();
                VisualEditor2.Document.Window.Scroll += Window_Scroll;
                VisualEditor2.Document.Title = "right";
                Category[] category;
                StreamReader id_list = new StreamReader("id_list.txt");                            

                int k = 0;

                if (thisApp.first_list == false && thisApp.list_refresh == true)
                {
                    combo_id_list.Items.Clear();
                    while (id_list.Peek() > -1)
                    {
                        check_idnull[k] = id_list.ReadLine();
                        if (check_idnull[k].Contains("_") == false)
                            combo_id_list.Items.Add(check_idnull[k]);
                        else
                        {
                            check_idnull[k] = check_idnull[k].Replace("_", "/");
                            combo_id_list.Items.Add(check_idnull[k]);
                        }
                        k++;
                    }
                    id_list.Close();
                    thisApp.list_refresh = false;
                    combo_id_list.SelectedIndex = 0;
                    k = 0;
                }

                if (thisApp.first_list == true)
                {                    
                    combo_id_list.Items.Clear();
                    while (id_list.Peek() > -1)
                    {
                        check_idnull[k] = id_list.ReadLine();
                        if (check_idnull[k].Contains("_") == false)
                            combo_id_list.Items.Add(check_idnull[k]);
                        else
                        {
                            check_idnull[k] = check_idnull[k].Replace("_", "/");
                            combo_id_list.Items.Add(check_idnull[k]);
                        }
                        k++;
                    }
                    id_list.Close();
                    k = 0;
                }

                if (check_idnull[0] == null)
                {
                    thisApp.check_first = true;
                    var wnd2 = new ModernDialog
                    {
                        Title = "계정 추가",
                        Content = new account_insert(),
                    };
                    wnd2.CloseButton.Visibility = Visibility.Hidden;
                    wnd2.ShowDialog();

                    StreamReader first_id_list = new StreamReader("id_list.txt");
                    string[] first_check_idnull = new string[100];
                    int o = 0;
                    combo_id_list.Items.Clear();
                    while (first_id_list.Peek() > -1)
                    {
                        first_check_idnull[o] = first_id_list.ReadLine();
                        combo_id_list.Items.Add(first_check_idnull[o]);
                        o++;
                    }
                    id_list.Close();                   

                    string tmp_blog_ad;
                    tmp_blog_ad = combo_id_list.Items.GetItemAt(0).ToString();

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

                    MetaWeblog api;

                    if (thisApp.now_blog_service == "Tistory")
                    {
                        api = new MetaWeblog(thisApp.now_blog_ad + "/api");
                        category = api.getCategories(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key);

                        combo_category.Items.Clear();
                        for (int j = 0; j < category.Length; j++)
                        {
                            combo_category.Items.Add(category[j].title);
                        }
                    }
                    else if (thisApp.now_blog_service == "Naver")
                    {
                        api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                        category = api.getCategories(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key);

                        combo_category.Items.Clear();
                        for (int j = 0; j < category.Length; j++)
                        {
                            combo_category.Items.Add(category[j].title);
                        }
                    }
                    else if (thisApp.now_blog_service == "WordPress")
                    {
                        api = new MetaWeblog(thisApp.now_blog_ad + "/xmlrpc.php");
                        category = api.getCategories("1", thisApp.now_id, thisApp.now_password);

                        combo_category.Items.Clear();
                        for (int j = 0; j < category.Length; j++)
                        {
                            combo_category.Items.Add(category[j].description);
                        }
                    }
                    combo_id_list.SelectedIndex = 0;
                    combo_category.SelectedIndex = 0;
                    thisApp.first_list = false;
                    thisApp.list_refresh = true;
                }
                else if (check_idnull[0] != null)
                {
                    StreamReader key = new StreamReader("key.txt");
                    thisApp.sSecretKey = key.ReadLine();                    
                    key.Close();

                    if (thisApp.check_account_select != true && thisApp.first_list == true)
                    {
                        combo_id_list.SelectedIndex = 0;
                        check_main_account(combo_id_list.SelectedValue.ToString());
                        thisApp.first_list = false;
                    }
                }

                if (thisApp.check_account_select == true)
                {
                    thisApp.check_account_select = false;
                    Editor.Document.Body.InnerHtml = "";
                    Editor.Document.Body.InnerText = "";

                    string now_id_item = thisApp.now_select_id_item;
                    blog_ad = thisApp.now_blog_ad;
                    user_id = thisApp.now_id;
                    user_password = thisApp.now_password;
                    blogapi_id = thisApp.now_api_id;
                    api_key = thisApp.now_api_key;
                    blog_service = thisApp.now_blog_service;

                    MetaWeblog api;

                    if (thisApp.now_blog_service == "Tistory")
                    {
                        api = new MetaWeblog(thisApp.now_blog_ad + "/api");
                        category = api.getCategories(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key);

                        combo_category.Items.Clear();
                        for (int j = 0; j < category.Length; j++)
                        {
                            combo_category.Items.Add(category[j].title);
                        }
                    }
                    else if (thisApp.now_blog_service == "Naver")
                    {
                        api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                        category = api.getCategories(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key);

                        combo_category.Items.Clear();
                        for (int j = 0; j < category.Length; j++)
                        {
                            combo_category.Items.Add(category[j].title);
                        }
                    }
                    else if (thisApp.now_blog_service == "WordPress")
                    {
                        api = new MetaWeblog(thisApp.now_blog_ad+"/xmlrpc.php");
                        category = api.getCategories("1", thisApp.now_id, thisApp.now_password);

                        combo_category.Items.Clear();
                        for (int j = 0; j < category.Length; j++)
                        {
                            combo_category.Items.Add(category[j].description);
                        }
                    }

                    int i=0;
                    int inde=0;
                    while (true)
                    {
                        if (now_id_item == combo_id_list.Items.GetItemAt(i).ToString())
                        {
                            inde = i;
                            break;
                        }
                        i++;
                    }
                    combo_id_list.SelectedIndex = inde;
                    combo_category.SelectedIndex = 0;
                }                
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }           
        }

        private void hiliter_insert_Click(object sender, RoutedEventArgs e)
        {            
            var wnd = new ModernWindow
            {
                Style = (Style)App.Current.Resources["EmptyWindow"],
                Content = new code_hiliter(),
            };
            wnd.ShowDialog();

            if (thisApp.coderesult != "")
            {
                Editor.inputVisualEditor(thisApp.coderesult);
                thisApp.coderesult = "";
            }
        }

        private void video_insert_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ModernWindow
            {
                Style = (Style)App.Current.Resources["EmptyWindow"],
                Content = new youtube_insert(),
                Height = 750,
            };
            wnd.ShowDialog();

            youtube_url.Add(@"<BR><IFRAME height=315 src=""" + thisApp.you_url + @""" frameBorder=0 width=450 allowfullscreen=""true""> </IFRAME>");
            youtube_thum_url.Add("<img src=" + "\"" + thisApp.thum_url + "\"" + "style=\"max-width:100%; height: auto;\"" + ">");
            if (thisApp.you_url != "" && thisApp.check_video_insert == true)
            {
                Editor.inputVisualEditor(youtube_url[youtube_url.Count-1]);
                thisApp.you_url = "";
                check_test = true;
            }
            thisApp.check_video_insert = false;
        }

        private void combo_id_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Editor.Document.Body.InnerHtml = "";
            //Editor.Document.Body.InnerText = "";
            //VisualEditor2.Document.Body.InnerHtml = "";
            //youtube_url.Clear();
            //youtube_thum_url.Clear();
            //str_map.Clear();
            //ch_str_map.Clear();
            //check_test = false;
            //check_test1 = false;
            if (thisApp.list_refresh != true)
                check_main_account(combo_id_list.SelectedValue.ToString());
        }

        private void layout_insert_Click(object sender, RoutedEventArgs e)
        {
            if (thisApp.now_blog_service != "WordPress")
            {

                var wnd = new ModernWindow
                {
                    Style = (Style)App.Current.Resources["EmptyWindow"],
                    Content = new layout_insert(),
                    Height = 800,
                };
                wnd.ShowDialog();

                if (thisApp.layout != "" && thisApp.check_layout_insert == true)
                {
                    Editor.inputVisualEditor(thisApp.layout);
                }
                thisApp.check_layout_insert = false;
                thisApp.layout = "";
            }
            else
                ModernDialog.ShowMessage("워드프레스는 레이아웃이 제공되지 않습니다", "알림", btn);
        }

        void Window_Scroll(object sender, System.Windows.Forms.HtmlElementEventArgs e)
        {            
            var scrolledBrowser = sender as System.Windows.Forms.HtmlWindow;
            if (scrolledBrowser == null) return;

            int y = scrolledBrowser.Document.Body.ScrollRectangle.Top;

            Editor.Document.Body.ScrollTop = y * 3;
        }

        private void title_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            title.Text = "";
        }

        private void Tags_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Tags.Text = "";
        }

        private void image_search_Click(object sender, RoutedEventArgs e)
        {
            var wnd3 = new ModernWindow
            {
                Style = (Style)App.Current.Resources["EmptyWindow"],
                Content = new image_search(),
                Height = 600,
                Width = 900,
            };
            wnd3.ShowDialog();

            if (thisApp.image_url != "")
            {
                string image = "<img src=" + "\"" + thisApp.image_url + "\"" + "style=\"max-width:100%; height: auto;\"" + ">";
                Editor.inputVisualEditor(image);
                VisualEditor2.Document.Body.InnerHtml = Editor.ContentHtml;
                thisApp.image_url = "";
            }

        }

        void InitTimer()
        {
            styleTimer = new DispatcherTimer();
            styleTimer.Interval = TimeSpan.FromMilliseconds(800);
            styleTimer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            test = Editor.ContentHtml;
            if (check_test == true)
            {               
                for (int i = 0; i < youtube_url.Count; i++)
                {
                    test = test.Replace(youtube_url[i], youtube_thum_url[i]);
                }                                
            }
            if (check_test1 == true)
            {
                for (int i = 0; i < str_map.Count; i++)
                {
                    test = test.Replace(str_map[i], ch_str_map[i]);
                }                
            }
            VisualEditor2.Document.Body.InnerHtml = test;
        }

        private void go_blog_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(thisApp.now_blog_ad);
        }
    }
}
