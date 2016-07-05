using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace Easy_Posting.Content
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class SettingsAppearanceViewModel
        : NotifyPropertyChanged
    {
        App thisApp = App.Current as App;
        // 9 accent colors from metro design principles
        /*private Color[] accentColors = new Color[]{
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x33, 0x99, 0x33),   // green
            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };*/

        // 20 accent colors from Windows Phone 8
        private Color[] accentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };
        
        private Color selectedAccentColor;
        private LinkCollection themes = new LinkCollection();
        private Link selectedTheme;

        public SettingsAppearanceViewModel()
        {           
            // add additional themes     
            this.themes.Add(new Link { DisplayName = "제이레빗", Source = new Uri("/Assets/ModernUI.jay.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "제이레빗1", Source = new Uri("/Assets/ModernUI.jay1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "제이레빗2", Source = new Uri("/Assets/ModernUI.jay2.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "제이레빗3", Source = new Uri("/Assets/ModernUI.jay3.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "한효주", Source = new Uri("/Assets/ModernUI.hyoju.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "한효주1", Source = new Uri("/Assets/ModernUI.hyoju1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "한효주2", Source = new Uri("/Assets/ModernUI.hyoju2.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "한효주3", Source = new Uri("/Assets/ModernUI.hyoju3.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "한효주4", Source = new Uri("/Assets/ModernUI.hyoju4.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "한효주5", Source = new Uri("/Assets/ModernUI.hyoju6.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "아이유", Source = new Uri("/Assets/ModernUI.iu.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "아이유1", Source = new Uri("/Assets/ModernUI.iu1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "김연아", Source = new Uri("/Assets/ModernUI.yuna.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "김연아1", Source = new Uri("/Assets/ModernUI.yuna1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "김연아2", Source = new Uri("/Assets/ModernUI.yuna2.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "김연아3", Source = new Uri("/Assets/ModernUI.yuna3.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "김연아4", Source = new Uri("/Assets/ModernUI.yuna4.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "김연아5", Source = new Uri("/Assets/ModernUI.yuna5.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "강민경", Source = new Uri("/Assets/ModernUI.kang.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "강민경1", Source = new Uri("/Assets/ModernUI.kang1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "강민경2", Source = new Uri("/Assets/ModernUI.kang2.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "강민경3", Source = new Uri("/Assets/ModernUI.kang3.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "태연", Source = new Uri("/Assets/ModernUI.yeon.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "태연1", Source = new Uri("/Assets/ModernUI.yeon1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "태연2", Source = new Uri("/Assets/ModernUI.yeon2.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "수지", Source = new Uri("/Assets/ModernUI.suzi.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "수지1", Source = new Uri("/Assets/ModernUI.suzi1.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "수지2", Source = new Uri("/Assets/ModernUI.suzi2.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "수지3", Source = new Uri("/Assets/ModernUI.suzi3.xaml", UriKind.Relative) });           
            this.themes.Add(new Link { DisplayName = "hello kitty", Source = new Uri("/Assets/ModernUI.HelloKitty.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "love", Source = new Uri("/Assets/ModernUI.Love.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "snowflakes", Source = new Uri("/Assets/ModernUI.Snowflakes.xaml", UriKind.Relative) });

            // add the default themes            
            this.themes.Add(new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource });
            this.themes.Add(new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource });

            SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
        }

        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.

            string skin_data = "";
            string skin_color = "";
            StreamReader file = new StreamReader("skin.txt");
            StreamReader file1 = new StreamReader("color.txt");
            skin_data = file.ReadLine();
            skin_color = file1.ReadLine();
            file.Close();
            file1.Close();
            var color = (Color)ColorConverter.ConvertFromString(skin_color);
                       
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(skin_data));
            this.SelectedAccentColor = color;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }

        public LinkCollection Themes
        {
            get { return this.themes; }
        }

        public Color[] AccentColors
        {
            get { return this.accentColors; }
        }

        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");

                    StreamWriter file = new StreamWriter("skin.txt");
                    file.WriteLine(value.Source);
                    file.Close();

                    // and update the actual theme                    
                    AppearanceManager.Current.ThemeSource = value.Source;
                    
                }
            }
        }

        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;
                    OnPropertyChanged("SelectedAccentColor");

                    StreamWriter file = new StreamWriter("color.txt");
                    file.WriteLine(value.ToString());
                    file.Close();

                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }
    }
}
