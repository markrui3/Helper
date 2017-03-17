using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using chat_wp.Resources;

namespace chat_wp
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<MusicControl> MediaList = new List<MusicControl>();
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            TextBox tba = new TextBox();
            tba.IsReadOnly = true;
            tba.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            tba.TextWrapping = TextWrapping.Wrap;
            tba.Text += "helper \n目前可以查到的功能有：\n" +
"-------------------------  \n" +
"查快递   翻译   天气查询  \n " +
"算人品   ip       身份证  \n" +
"查百科   笑话   周公解梦  \n" +
"查公交   菜谱   成语词典  \n" +
"手机            空气质量  \n" +
"历史上的今天    在线听歌\n" +
"-------------------------  \n" +
"回复关键词获取查询信息\n" + "或者直接回复“帮助”\n或“h”获取相关信息。\n" +
"回复任意内容进行聊天~";
            panel.Children.Add(tba);


            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {

                String question = input.Text;
                input.Text = "";

                TextBox tb = new TextBox();
                tb.IsReadOnly = true;
                //tb.Width = 1340;

                tb.TextWrapping = TextWrapping.Wrap;
                tb.Text += "主人 \n" + question;
                tb.TextAlignment = TextAlignment.Right;
                panel.Children.Add(tb);

                if (question != "")
                {
                    models.MsgText msg = new models.MsgText(question);
                    msg.getText();
                    utils.JsonObj obj = new utils.JsonObj(msg.retObj);
                    if (obj.MsgType == "text")
                    {
                        TextBox tb1 = new TextBox();
                        tb1.IsReadOnly = true;
                        tb1.TextWrapping = TextWrapping.Wrap;
                        //tb1.Width = 1366;
                        if (obj.Content == "")
                        {
                            tb1.Text = "helper \n您在说什么啊，大点声~";
                        }
                        else
                        {
                            tb1.Text = "helper \n" + obj.Content;

                        }

                        panel.Children.Add(tb1);

                    }
                    else if (obj.MsgType == "news")
                    {
                        news news = new news();
                        news.initial(obj.ArticleCount, obj.Title, obj.Url);
                        panel.Children.Add(news);
                    }
                    else if (obj.MsgType == "music")
                    {
                        MusicControl mc = new MusicControl();
                        mc.SetPoster("ms-appx:///Assets/stopped.jpg");
                        mc.SetSource(obj.MusicUrl);
                        mc.SetText(obj.MusicTitle, obj.MusicDes);

                        mc.MouseLeftButtonDown += MediaElement_PointerPressed;
                        panel.Children.Add(mc);
                        //lb.PointerPressed += MediaElement_PointerPressed;
                        MediaList.Add(mc);
                    }
                }
                else
                {
                    TextBox tb2 = new TextBox();
                    tb2.IsReadOnly = true;
                    tb2.TextWrapping = TextWrapping.Wrap;
                    tb2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    tb2.Text = "helper \n您在说什么啊，大点声~\n\n";
                    panel.Children.Add(tb2);
                }
               // input.Focus();
            }
            sv_SizeChanged();
       

        }

        private void sv_SizeChanged()
        {
            sv.UpdateLayout();
            sv.ScrollToVerticalOffset(panel.ActualHeight);
        }

        private void input_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (input.Text == "")
            {

            }
            else if (e.Key == System.Windows.Input.Key.Enter)
            {
                Button_Click_1(sender, e);
                return;
            }
        }

        private void MediaElement_PointerPressed(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                MediaElement media = (MediaElement)e.OriginalSource;
                if (media.CurrentState == MediaElementState.Stopped || media.CurrentState == MediaElementState.Buffering || media.CurrentState == MediaElementState.Opening || media.CurrentState == MediaElementState.Paused)
                {
                    foreach (MusicControl m in MediaList)
                    {
                        m.SetPoster("ms-appx:///Assets/stopped.png");
                        m.Stop();
                    }
                    media.Play();
                    if (media.CurrentState == MediaElementState.Stopped)
                    {
                        Image img = (Image)e.OriginalSource;
                        img.Source = new BitmapImage(new Uri("ms-appx:///Assets/playing.png", UriKind.RelativeOrAbsolute));
                    }
                        
                }
                else if (media.CurrentState == MediaElementState.Playing || media.CurrentState == MediaElementState.Closed)
                {
                    Image img = (Image)e.OriginalSource;
                    img.Source = new BitmapImage(new Uri("ms-appx:///Assets/stopped.png", UriKind.RelativeOrAbsolute));
                    media.Stop();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}