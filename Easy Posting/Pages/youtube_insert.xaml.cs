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
    /// youtube_insert.xaml에 대한 상호 작용 논리
    /// </summary>


    public partial class youtube_insert : UserControl
    {
        App thisApp = App.Current as App;
        //Thread search_thread;
        //myUploads myvideo;
        List<YouTubeInfo> infos;
        MessageBoxButton btn = MessageBoxButton.OK;
        int idx;

        public youtube_insert()
        {
            InitializeComponent();
            InitViewer();
        }

        // 검색 이미지 생성
        private void PopulateCanvas(List<YouTubeInfo> infos)
        {
            this.YouList.Items.Clear();

            for (int i = 0; i < infos.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = infos[i].EmbedUrl;
                item.Margin = new Thickness(10F);

                StackPanel outerPanel = new StackPanel();
                outerPanel.Orientation = Orientation.Horizontal;
                outerPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                StackPanel imgCanvas = new StackPanel();
                imgCanvas.Width = 130F;

                Image img = new Image();
                BitmapImage imgSource = new BitmapImage(new Uri(infos[i].ThumbNailUrl, UriKind.RelativeOrAbsolute));
                img.Source = imgSource;

                imgCanvas.Children.Add(img);
                outerPanel.Children.Add(imgCanvas);

                /* panel을 추가하는 방법 */

                //StackPanel innerPanel = new StackPanel();
                //innerPanel.Orientation = Orientation.Vertical;

                //TextBlock text1 = new TextBlock();
                //text1.Text = control.Info.title;
                //text1.TextWrapping = TextWrapping.Wrap;
                //innerPanel.Children.Add(text1);

                //text1 = new TextBlock();
                //text1.Text = control.Info.description;
                //innerPanel.Children.Add(text1);

                //outerPanel.Children.Add(innerPanel);

                item.Content = outerPanel;

                this.YouList.Items.Add(item);

            }
        }


        // 초기 목록 생성
        public void InitViewer()
        {
            // ImageSource imageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + "//Youlogo.png"));
            //image1.Source = imageSource;
            infos = YouTubeProvider.LoadVideosKey("에이핑크");
            PopulateCanvas(infos);
            idx = 0;

            //myvideo = new myUploads();
            //search_thread = new Thread(new ThreadStart(myvideo.search));
            //search_thread.Start();
        }


        // 찾기 버튼
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtKeyWord.Text == "")
                ModernDialog.ShowMessage("검색어를 입력하세요", "알림", btn);
            else
            {
                infos = YouTubeProvider.LoadVideosKey(txtKeyWord.Text);
                PopulateCanvas(infos);
            }
        }

        //엔터키로 찾기
        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_1(sender, e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            if (thisApp.you_url != "")
            {
                thisApp.check_video_insert = true;
                parentWindow.Close();
            }
            else
                ModernDialog.ShowMessage("선택된 동영상이 없습니다.", "알림", MessageBoxButton.OK);
        }

        private void YouList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object o = YouList.SelectedItem;

                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListViewItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                idx = YouList.ItemContainerGenerator.IndexFromContainer(dep);

                YouTitle.Text = infos[idx].title;
                player.Source = new Uri(infos[idx].EmbedUrl, UriKind.Absolute);
                thisApp.thum_url = infos[idx].ThumbNailUrl;
                thisApp.you_url = infos[idx].ViewerUrl;
                //thisApp.you_url = thisApp.you_url.Replace("http", "https");
                YouDescription.Text = infos[idx].description;
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            player.Dispose();
        }
    }
}
