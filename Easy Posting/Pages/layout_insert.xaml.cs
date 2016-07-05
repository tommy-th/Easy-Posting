using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using FirstFloor.ModernUI.Windows.Controls;

namespace Easy_Posting.Content
{
    /// <summary>
    /// y_insert.xaml에 대한 상호 작용 논리
    /// </summary>


    public partial class layout_insert : UserControl
    {        
        App thisApp = App.Current as App;
        MessageBoxButton btn = MessageBoxButton.OK;
        string html_changed = null;
        string[] strimage = new string[100];

        public layout_insert()
        {
            InitializeComponent();
            ImageSource image1 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//layout1.png"));
            layout1.Source = image1;
            ImageSource image2 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//layout2.png"));
            layout2.Source = image2;
            ImageSource image3 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//layout3.png"));
            layout3.Source = image3;
            ImageSource image4 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//art.png"));
            art.Source = image4;
            ImageSource image5 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//book.png"));
            book.Source = image5;
            ImageSource image6 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//cook.png"));
            cook.Source = image6;
            ImageSource image7 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//drama.png"));
            drama.Source = image7;
            ImageSource image8 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//movie.png"));
            movie.Source = image8;
            ImageSource image9 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//music.png"));
            music.Source = image9;
            ImageSource image10 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//restaurant.png"));
            restaurant.Source = image10;
            ImageSource image11 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//review.png"));
            review.Source = image11;
            ImageSource image12 = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Assets//layouts//tour.png"));
            tour.Source = image12;
        }


        private void layout1_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("layout1");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//layout1.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void layout2_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("layout2");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//layout2.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void layout3_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("layout3");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//layout3.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void art_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("art");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            imageselect.Items.Add("5번이미지");
            imageselect.Items.Add("6번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//art.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void book_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("book");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//book.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void cook_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("cook");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//cook.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void music_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("music");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//music.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void review_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("review");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//review.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void restaurant_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("restaurant");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//restaurant.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void tour_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("tour");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//tour.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void drama_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("drama");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//drama.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }
        private void movie_Selected(object sender, RoutedEventArgs e)
        {
            layoutselect.Items.Clear();
            layoutselect.SelectedIndex = layoutselect.Items.Add("movie");
            imageselect.Items.Clear();
            imageselect.Items.Add("1번이미지");
            imageselect.Items.Add("2번이미지");
            imageselect.Items.Add("3번이미지");
            imageselect.Items.Add("4번이미지");
            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//movie.html");
            // string html2 = html.Replace("img1src", "http://cfs8.tistory.com/image/24/tistory/2008/09/08/21/43/48c51ddecb27f");
            LayoutBrowse.DocumentText = html;
        }


        private void insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MediaObjectUrl url;                
                string[] html_image = new string[100];
                

                Microsoft.Win32.OpenFileDialog Dialog = new Microsoft.Win32.OpenFileDialog();
                Dialog.Title = "이미지 파일 추가";
                Dialog.ShowReadOnly = true;
                Dialog.Multiselect = false;
                Dialog.Filter = "Image Files (*.jpeg, *.png, *.jpg, *.gif)| *.jpeg; *.png; *.jpg; *.gif";

                if (Dialog.ShowDialog() == true)
                {
                    strimage = Dialog.FileNames;
                }

                if (strimage[0] != null)
                {

                        string[] cut = new string[100];
                        int index = 0;
                        MetaWeblog api;

                        if (thisApp.now_blog_service == "Tistory")
                        {
                            api = new MetaWeblog(thisApp.now_blog_ad + "/api");
                            url = api.newMediaObject(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key, api.GetMediaObject(strimage[0]));

                            index = url.url.ToString().IndexOf("?tt");
                            string test = url.url.ToString();
                            char[] cuttest = new char[index];
                            for (int j = 0; j < index; j++)
                                cuttest[j] = test[j];

                            cut[0] = new string(cuttest);
                            string sel = layoutselect.SelectedValue.ToString();
                            string sel_img = imageselect.SelectedValue.ToString();
                            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//" + sel + ".html");


                            if (html_changed == null)
                                html_changed = html.Replace(sel_img, cut[0]);
                            else
                                html_changed = html_changed.Replace(sel_img, cut[0]);
                            LayoutBrowse.DocumentText = html_changed;
                        }
                        else if (thisApp.now_blog_service == "Naver")
                        {
                            string str = "";
                            api = new MetaWeblog("https://api.blog.naver.com/xmlrpc");
                            url = api.newMediaObject(thisApp.now_api_id, thisApp.now_id, thisApp.now_api_key, api.GetMediaObject(strimage[0]));
                            str = url.url.ToString();
                            string sel = layoutselect.SelectedValue.ToString();
                            string sel_img = imageselect.SelectedValue.ToString();
                            string html = File.ReadAllText(Environment.CurrentDirectory + "//Assets//layouts//" + sel + ".html");


                            if (html_changed == null)
                                html_changed = html.Replace(sel_img, str);
                            else
                                html_changed = html_changed.Replace(sel_img, str);
                            LayoutBrowse.DocumentText = html_changed;
                        }
                        //else if (thisApp.now_blog_service == "WordPress")
                        //{
                        //    string str = "";
                        //    api = new MetaWeblog(thisApp.now_blog_ad + "/xmlrpc.php");
                        //    url = api.newMediaObject(thisApp.now_api_id, thisApp.now_id, thisApp.now_password, api.GetMediaObject(strimage[0]));
                        //    str = url.url.ToString();
                        //    string sel = layoutselect.SelectedValue.ToString();
                        //    string sel_img = imageselect.SelectedValue.ToString();
                        //    string html = File.ReadAllText("../assets/layouts/" + sel + ".html");


                        //    if (html_changed == null)
                        //        html_changed = html.Replace(sel_img, str);
                        //    else
                        //        html_changed = html_changed.Replace(sel_img, str);
                        //    LayoutBrowse.DocumentText = html_changed;
                        //}


                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }



        }

        private void layout_insert_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;

            if (Layouts.SelectedValue != null)
            {
                if (strimage[0] == null)
                {
                    ModernDialog.ShowMessage("삽입된 이미지가 없습니다.", "알림", MessageBoxButton.OK);
                }
                else
                {
                    thisApp.layout = html_changed;
                    thisApp.check_layout_insert = true;
                    parentWindow.Close();
                }
            }
            else
                ModernDialog.ShowMessage("선택된 레이아웃이 없습니다.", "알림", MessageBoxButton.OK);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            LayoutBrowse.Dispose();
        }

    }
}

