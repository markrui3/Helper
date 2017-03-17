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
using System.Windows.Media.Imaging;

namespace chat_wp
{
    public partial class MusicControl : UserControl
    {
        public MusicControl()
        {
            InitializeComponent();
        }

        public void SetSource(string url)
        {
            this.me.Source = new Uri(url);
        }

        public void SetPoster(string url)
        {
            this.img.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        public void SetText(string title, string des)
        {
            this.tb.Text = title + "\n" + des;
        }

        public void Play()
        {
            this.me.Play();
            this.img.Source = new BitmapImage(new Uri("ms-appx:///Assets/playing.png", UriKind.RelativeOrAbsolute));
        }

        public void Stop()
        {
            this.me.Stop();
            this.img.Source = new BitmapImage(new Uri("ms-appx:///Assets/stopped.png", UriKind.RelativeOrAbsolute));
        }

        public MediaElementState GetState()
        {
            return this.me.CurrentState;
        }

        private void me_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.img.Source = new BitmapImage(new Uri("ms-appx:///Assets/stopped.png", UriKind.RelativeOrAbsolute));
            this.me.Stop();
        }

        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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
