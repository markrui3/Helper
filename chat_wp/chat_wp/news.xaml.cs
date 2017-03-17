using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace chat_wp
{
    public partial class news : UserControl
    {
        public news()
        {
            InitializeComponent();
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
                hy.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                hy.Content = title[i];

                hy.Background = new SolidColorBrush(Colors.Blue);
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
