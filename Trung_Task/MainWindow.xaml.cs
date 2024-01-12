using System.ComponentModel;
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

namespace Trung_Task
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Init
        public Uri mediaSource { get; set; }

        public int currentIndex { get; set; }
        public bool shufleMode { get; set; } = false;
        public List<Uri> listMediaSource { get; set; }

        public MainWindow()
        {
            listMediaSource = new List<Uri>() {
                new Uri("C:\\Users\\USER\\Downloads\\SampleVideo_720x480_30mb.mp4"),
                new Uri("C:\\Users\\USER\\Downloads\\pexels-thirdman-5538137 (1080p).mp4"),
                new Uri("C:\\Users\\USER\\Downloads\\video (2160p).mp4"),
                new Uri("C:\\Users\\USER\\Downloads\\pexels-thirdman-5538262 (1080p).mp4"),
            };
            mediaSource = listMediaSource[0];
            currentIndex = 0;
            InitializeComponent();
            this.DataContext = this;
        }

        // Function


        private void SetRandomSource() {
            int sourceIndex = -1;
            do {
                Random rnd = new Random();
                sourceIndex = rnd.Next(listMediaSource.Count);
            } while (mediaSource == listMediaSource[sourceIndex]);
            mediaSource = listMediaSource[sourceIndex];
            currentIndex = sourceIndex;
        }


        // Event

        private void Element_MediaEnded(object sender, RoutedEventArgs e)
        {
            SetRandomSource();
        }

        private void Element_MediaOpened(object sender, RoutedEventArgs e)
        {

        }

        private void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Play();
        }

        private void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Pause();
        }

        private void OnMouseDownStopMedia(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Stop();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void OnMouseDownPreMedia(object sender, MouseButtonEventArgs e)
        {
            if (shufleMode) { 
                SetRandomSource();
            } else
            {
                var newIndex = listMediaSource.IndexOf(mediaSource) - 1 < 0 ? listMediaSource.Count - 1 : listMediaSource.IndexOf(mediaSource) - 1;
                mediaSource = listMediaSource[newIndex];
                currentIndex = newIndex;
            }
        }

        private void OnMouseDownNextMedia(object sender, MouseButtonEventArgs e)
        {
            if (shufleMode)
            {
                SetRandomSource();
            } else
            {
                var newIndex = listMediaSource.IndexOf(mediaSource) + 1 >= listMediaSource.Count ? 0 : listMediaSource.IndexOf(mediaSource) + 1;
                mediaSource = listMediaSource[newIndex];
                currentIndex = newIndex;    
            }
        }
    }
}