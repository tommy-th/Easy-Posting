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
using FirstFloor.ModernUI.Windows.Controls;


namespace Easy_Posting.Content
{
    /// <summary>
    /// map.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class map : UserControl
    {
        MessageBoxButton btn = MessageBoxButton.OK;
        App thisApp = App.Current as App;

        public map()
        {
            InitializeComponent();
            string url = Environment.CurrentDirectory + "\\map.html";
            this.webBrowser.Navigate(url);
            
        }

        private void map_search_bt(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textStreet.Text == "")
                {
                    ModernDialog.ShowMessage("주소를 입력 해주세요", "알림", btn);
                }
                else
                {
                    webBrowser.InvokeScript("codeAddress", new object[] { textStreet.Text });
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message.ToString(), "알림", btn);                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            if (textStreet.Text != "")
            {
                thisApp.textStreet = textStreet.Text;
                thisApp.check_mapuse = true;
                parentWindow.Close();
            }
            else
                ModernDialog.ShowMessage("주소를 입력 해주세요", "알림", MessageBoxButton.OK);
        }

        private void textStreet_KeyDown(object sender, KeyEventArgs e)
        {                     
            if (e.Key == Key.Enter)
            {
                map_search_bt(sender, e);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.webBrowser.Dispose();
        }
    }
}
