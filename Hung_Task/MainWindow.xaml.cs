using Microsoft.Win32;
using System.Text;
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

namespace Hung_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(200);
            _timer.Tick += Timer_Tick;
        }

        void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {
            myMediaElement.Play();
            _timer.Start();

            InitializePropertyValues();
        }

        void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {
            myMediaElement.Pause();
            _timer.Stop();
        }

        void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {
            myMediaElement.Stop();
            _timer.Stop();
        }

        private void Timer_Tick(object? sender = null, EventArgs? e = null)
        {
            if (!timelineSlider.IsMouseCaptureWithin)
            {
                timelineSlider.Value = myMediaElement.Position.TotalMilliseconds;
            }
        }
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            if (timelineSlider.IsMouseCaptureWithin)
            {
                myMediaElement.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
            }
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            //myMediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMediaElement.Stop();
            _timer.Stop();
        }

        void InitializePropertyValues()
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
            myMediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void OnOpenMediaFile(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                myMediaElement.Source = new Uri(dialog.FileName);
            }
        }

        private void OnSkipBackward(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Position = myMediaElement.Position.Subtract(TimeSpan.FromSeconds(5));
        }

        private void OnSkipForward(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Position = myMediaElement.Position.Add(TimeSpan.FromSeconds(5));
        }
    }
}