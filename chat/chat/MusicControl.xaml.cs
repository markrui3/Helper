using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234236 上提供

namespace chat
{
 
    public sealed partial class MusicControl : UserControl
    {

        public MusicControl()
        {
            this.InitializeComponent();
        }

        public void SetSource(string url)
        {
            this.me.Source = new Uri(url); 
        }

        public void SetPoster(string url)
        {
            this.me.PosterSource = new BitmapImage(new Uri(url,UriKind.RelativeOrAbsolute));
        }

        public void SetText(string title, string des)
        {
            this.tb.Text = title + "\n" + des;
        }

        public void Play()
        {
            this.me.Play();
            this.me.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/playing.jpg", UriKind.RelativeOrAbsolute));
        }

        public void Stop()
        {
            this.me.Stop();
            this.me.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/stopped.jpg", UriKind.RelativeOrAbsolute));
        }

        public MediaElementState GetState()
        {
            return this.me.CurrentState;
        }

        private void me_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.me.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/stopped.jpg", UriKind.RelativeOrAbsolute));
            this.me.Stop();
        }

        private void timelineSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            int SliderValue = (int)timelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            this.me.Position = ts;
        }

        private void me_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = this.me.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void me_Loaded(object sender, RoutedEventArgs e)
        {
            this.me.Stop();
        }

    }
}
