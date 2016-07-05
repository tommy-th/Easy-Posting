using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;
using Google.API.Search;
using FirstFloor.ModernUI.Windows.Controls;

namespace Easy_Posting.Content
{
    /// <summary>
    /// image_search.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class image_search : UserControl
    {
        App thisApp = App.Current as App;
        private int idx;
        private IList<IImageResult> results;

        public image_search()
        {
            InitializeComponent();
        }

        private void serch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GimageSearchClient client = new GimageSearchClient("http://www.google.com");
                ImageListbox.Items.Clear();

                IAsyncResult result = client.BeginSearch(
                    serch_text.Text.Trim(),  //param1
                    int.Parse("80"), //param2
                    ((arResult) => //param3
                    {
                        results = client.EndSearch(arResult);
                        Dispatcher.Invoke(DispatcherPriority.Send,
                            (Action<IList<IImageResult>>)(async (data) =>
                            {
                                for (int i = 0; i < results.Count; i++)
                                {
                                    Image img = new Image
                                    {
                                        Source = await DownloadImage(results[i].TbImage.Url),
                                        Stretch = Stretch.UniformToFill,
                                        StretchDirection = StretchDirection.DownOnly,
                                    };

                                    ListViewItem item = new ListViewItem();
                                    item.Margin = new Thickness(10F);

                                    StackPanel outerPanel = new StackPanel();
                                    outerPanel.Orientation = Orientation.Horizontal;
                                    outerPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

                                    StackPanel imgCanvas = new StackPanel();
                                    imgCanvas.Width = 130F;
                                    imgCanvas.Children.Add(img);

                                    outerPanel.Children.Add(imgCanvas);
                                    item.Content = outerPanel;

                                    this.ImageListbox.Items.Add(item);                                    
                                }
                            }), null);
                    }),
                    null //param4
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<BitmapImage> DownloadImage(string url)
        {
            return byteArrayToImage(await new WebClient().DownloadDataTaskAsync(url));
        }
        public BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            BitmapImage myBitmapImage;
            using (MemoryStream stream = new MemoryStream(byteArrayIn))
            {
                // frame= BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.StreamSource = stream;
                myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                myBitmapImage.EndInit();
            }
            return myBitmapImage;
        }

        private void ImageListbox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object o = ImageListbox.SelectedItem;

                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is ListViewItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                    return;

                idx = ImageListbox.ItemContainerGenerator.IndexFromContainer(dep);                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void serch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                serch_Click(sender, e);
            }
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            if (results != null)
            {
                thisApp.image_url = results[idx].Url;
                parentWindow.Close();
            }
            else
                ModernDialog.ShowMessage("선택된 이미지가 없습니다.", "알림", MessageBoxButton.OK);
        }        
    }
}
