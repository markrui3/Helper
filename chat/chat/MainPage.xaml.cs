using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Xml;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.IO;
using Windows.UI.Popups;
using Windows.Web.Syndication;
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using System.Threading;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace chat
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<MusicControl> MediaList = new List<MusicControl>();

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TextBox tba = new TextBox();
            tba.IsReadOnly = true;
            tba.Width = 1340;
            //tba.FontSize = 20;
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
"回复关键词获取查询信息 或者 直接回复“帮助”或“h”获取相关信息。\n" +
"回复任意内容进行聊天~";
            lb.Items.Add(tba);
            ScrollToEnd();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {

                String question = input.Text;
                input.Text = "";

                TextBox tb = new TextBox();
                tb.IsReadOnly = true;
                tb.Width = 1340;

                tb.TextWrapping = TextWrapping.Wrap;
                tb.Text += "主人 \n" + question;
                tb.TextAlignment = TextAlignment.Right;
                lb.Items.Add(tb);
                ScrollToEnd();

                if (question != "")
                {
                    models.MsgText msg = new models.MsgText(question);
                    await msg.getText();
                    utils.JsonObj obj = new utils.JsonObj(msg.retObj);
                    if (obj.MsgType == "text")
                    {
                        TextBox tb1 = new TextBox();
                        tb1.IsReadOnly = true;
                        tb1.TextWrapping = TextWrapping.Wrap;
                        tb1.Width = 1340;
                        if (obj.Content == "")
                        {
                            tb1.Text = "helper \n您在说什么啊，大点声~";
                        }
                        else
                        {
                            tb1.Text = "helper \n" + obj.Content;

                        }

                        lb.Items.Add(tb1);

                    }
                    else if (obj.MsgType == "news")
                    {
                        news news = new news();
                        news.initial(obj.ArticleCount, obj.Title, obj.Url);
                        lb.Items.Add(news);
                    }
                    else if (obj.MsgType == "music")
                    {
                        MusicControl mc = new MusicControl();

                        //mc.Height = 120;
                        //mc.Width = 250;
                        mc.SetPoster("ms-appx:///Assets/stopped.jpg");
                        mc.SetSource(obj.MusicUrl);
                        mc.SetText(obj.MusicTitle, obj.MusicDes);

                        mc.PointerPressed += MediaElement_PointerPressed;
                        lb.Items.Add(mc);
                        //lb.PointerPressed += MediaElement_PointerPressed;
                        MediaList.Add(mc);
                    }
                }
                else
                {
                    TextBox tb2 = new TextBox();
                    tb2.IsReadOnly = true;
                    tb2.TextWrapping = TextWrapping.Wrap;
                    tb2.Width = 1340;
                    tb2.Text = "helper \n您在说什么啊，大点声~\n\n";
                    lb.Items.Add(tb2);
                }
                ScrollToEnd();
                input.Focus(FocusState.Programmatic);
            }
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            NetworkChange.NetworkAddressChanged += OnNetworkAddressChanged;

             UpdateNetWorkState();
            //new Timer(UpdateNetWorkState, null, 0, 2000);
        }

        async Task UpdateNetWorkState()
        {
            try
            {
                string result = await utils.webutils.file_get_contents("http://www.hao123.com/");
                if (result != "")
                    network.Text = "您当前的网络状态良好";
                else
                    network.Text = "您当前木有网络连接，请尝试重新连接";
            }
            catch (Exception)
            {
                network.Text = "您当前木有网络连接，请尝试重新连接";
            }
        }

        void OnNetworkAddressChanged(object sender, EventArgs e)
        {
            UpdateNetWorkState();
        }
        private void text_changed(object sender, TextChangedEventArgs e)
        {
            /*var grid = (Grid)VisualTreeHelper.GetChild(output, 0);

            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(grid) - 1; i++)
            {
                object obj = VisualTreeHelper.GetChild(grid, i);
                if (!(obj is ScrollViewer)) continue;
               // ((ScrollViewer)obj).ScrollToVerticalOffset(((ScrollViewer)obj).ExtentHeight);
                ((ScrollViewer)obj).ChangeView(0, ((ScrollViewer)obj).ExtentHeight, 1, false);
                break;
          }*/
        }

        private void input_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (input.Text == "")
            {

            }
            else if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Button_Click_1(sender, e);
                return;
            }
        }

        private void ScrollToEnd()
        {
            lb.UpdateLayout();
            lb.ScrollIntoView(lb.Items.LastOrDefault());
            //sv.ScrollToVerticalOffset(1340);
        }

        private void MediaElement_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                MediaElement media = (MediaElement)e.OriginalSource;
                if (media.CurrentState == MediaElementState.Stopped || media.CurrentState == MediaElementState.Buffering || media.CurrentState == MediaElementState.Opening || media.CurrentState == MediaElementState.Paused)
                {
                    foreach (MusicControl m in MediaList)
                    {
                        m.SetPoster("ms-appx:///Assets/stopped.jpg");
                        m.Stop();
                    }
                    media.Play();
                    if (media.CurrentState == MediaElementState.Stopped)
                        media.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/playing.jpg", UriKind.RelativeOrAbsolute));
                }
                else if (media.CurrentState == MediaElementState.Playing || media.CurrentState == MediaElementState.Closed)
                {
                    media.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/stopped.jpg", UriKind.RelativeOrAbsolute));
                    media.Stop();
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }

}
