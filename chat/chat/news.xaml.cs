using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace chat
{
    public sealed partial class news : UserControl
    {
        public news()
        {
            this.InitializeComponent();
        }

        public void initial(double num, string[] title, string[] url)
        {
            TextBlock tb = new TextBlock();
            tb.Text = "helper";
            lv.Children.Add(tb);
            for (int i = 0; i < num; i++)
            {
                HyperlinkButton hy = new HyperlinkButton();
                hy.Height = 50;
                hy.Width = 400;
                hy.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                hy.Content = title[i];

                hy.Background = new SolidColorBrush(Colors.AliceBlue);
                hy.Foreground = new SolidColorBrush(Colors.Black);
                try
                {
                    hy.NavigateUri = new Uri(url[i]);
                }
                catch (Exception)
                {

                }
                lv.Children.Add(hy);
            }
        }

    }
}
