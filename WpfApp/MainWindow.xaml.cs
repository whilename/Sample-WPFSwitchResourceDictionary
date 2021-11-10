using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WPFMultiLanguage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            //string message = TryFindResource("Message") as string;
            //MessageBox.Show(message);

            // Reset the CultureInfo of the current application; If it is en-US, switch to zh-CN, otherwise switch to zh-CN.
            System.Globalization.CultureInfo.CurrentUICulture = CurrentLanguageIsEnUS() ?
                new System.Globalization.CultureInfo("zh-CN") : new System.Globalization.CultureInfo("en-US");
            SetLanguage();
        }

        public void SetLanguage()
        {
            // TODO: Switch resource dictionary
            string url = CurrentLanguageIsEnUS() ? "Themes/Theme.xaml" : "pack://application:,,,/WpfLibrary;component/Themes/Theme.xaml";
            Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
            // Clean up the resource list of the current application.
            Application.Current.Resources.MergedDictionaries.Clear();
            // Load new resource dictionary
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }

        private bool CurrentLanguageIsEnUS()
        {
            // Get the current system language.
            string lang = System.Globalization.CultureInfo.CurrentUICulture.Name;
            return lang.Equals("en-US", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
