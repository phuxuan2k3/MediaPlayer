using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Threading;
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
        private DispatcherTimer _previewTimer;
        private DateTime _lastUpdate = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(200);
            _timer.Tick += Timer_Tick;

            _previewTimer = new DispatcherTimer();
            _previewTimer.Interval = TimeSpan.FromMilliseconds(1);
            _previewTimer.Tick += PreviewTimer_Tick;

            timelineSlider.MouseEnter += TimelineSlider_MouseEnter;
            timelineSlider.MouseMove += TimelineSlider_MouseMove;
            timelineSlider.MouseLeave += TimelineSlider_MouseLeave;
        }

        // Event nút bấm

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

        void OnMouseStartOverMedia(object sender, MouseButtonEventArgs args) // Tua video lại từ đầu, chưa có nút
        {
            myMediaElement.Stop();
            _timer.Stop();

            myMediaElement.Play();
            _timer.Start();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        private void ChangeMediaSpeedRatio(object sender, SelectionChangedEventArgs e)
        {
            string selectedSpeedRatio = ((ComboBoxItem)speedRatioComboBox.SelectedItem).Content.ToString();
            double speedRatio = double.Parse(selectedSpeedRatio.Trim('x'));
            SetMediaSpeed(speedRatio);
        }

        private void SetMediaSpeed(double speedRatio)
        {
            myMediaElement.SpeedRatio = speedRatio;
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
        }

        // Cập nhật thanh slider 200ms 1 lần

        private void Timer_Tick(object? sender = null, EventArgs? e = null)
        {
            if (!timelineSlider.IsMouseCaptureWithin)
            {
                timelineSlider.Value = myMediaElement.Position.TotalMilliseconds;
            }
            if (myMediaElement.NaturalDuration.HasTimeSpan)
            {
                progressTextBox.Text = $"{myMediaElement.Position.ToString(@"hh\:mm\:ss")}/{myMediaElement.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss")}";
            }
        }

        // Tua MediaElement, 100ms cập nhật 1 lần, cập nhật liên tục video siêu lag

        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            if (timelineSlider.IsMouseCaptureWithin)
            {
                if ((DateTime.Now - _lastUpdate).TotalMilliseconds >= 100)
                {
                    myMediaElement.Position = TimeSpan.FromMilliseconds(timelineSlider.Value);
                    _lastUpdate = DateTime.Now;
                }
            }
        }

        // Mở file

        private void OnOpenMediaFile(object sender, MouseButtonEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                myMediaElement.Source = new Uri(dialog.FileName);
                previewMediaElement.Source = myMediaElement.Source;
                myMediaElement.Play();
                previewMediaElement.Volume = 0;
                _timer.Start();
            }
        }

        // Nút tua + tua ngược 5s

        private void OnSkipBackward(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Position = myMediaElement.Position.Subtract(TimeSpan.FromSeconds(5));
        }

        private void OnSkipForward(object sender, MouseButtonEventArgs e)
        {
            myMediaElement.Position = myMediaElement.Position.Add(TimeSpan.FromSeconds(5));
        }

        // Preview

        private void TimelineSlider_MouseEnter(object sender, MouseEventArgs e)
        {
            previewMediaElement.Visibility = Visibility.Visible;
            previewMediaElement.Position = TimeSpan.FromMilliseconds(0);
        }

        private void TimelineSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (timelineSlider.IsMouseOver && !timelineSlider.IsMouseCaptureWithin)
            {
                previewMediaElement.Visibility = Visibility.Visible;
                double mousePos = e.GetPosition(timelineSlider).X;
                double sliderValue = mousePos / timelineSlider.ActualWidth * timelineSlider.Maximum;
                previewMediaElement.Position = TimeSpan.FromMilliseconds(sliderValue);
                previewMediaElement.Play();

                Point position = e.GetPosition(this);
                Canvas.SetLeft(previewMediaElement, position.X - 60); //60 là width / 2 của cái previewMediaElement

                _previewTimer.Start();
            }
            else
            {
                previewMediaElement.Visibility = Visibility.Hidden;
            }
        }
        private void TimelineSlider_MouseLeave(object sender, MouseEventArgs e)
        {
            previewMediaElement.Visibility = Visibility.Hidden;
            previewMediaElement.Position = TimeSpan.FromMilliseconds(0);
        }

        private void PreviewTimer_Tick(object? sender = null, EventArgs? e = null)
        {
            previewMediaElement.Pause();
            _previewTimer.Stop();
        }

        private void TimelineSlider_SeekClick(object sender, MouseButtonEventArgs e)
        {
            double mousePos = e.GetPosition(timelineSlider).X;
            double sliderValue = mousePos / timelineSlider.ActualWidth * timelineSlider.Maximum;
            timelineSlider.Value = sliderValue;
            myMediaElement.Position = TimeSpan.FromMilliseconds(sliderValue);
        }

        // Hàm viết/đọc guid
        public void WriteGuidToTextFile(Guid guid, string filePath) // filePath là đường dẫn tới folder, lấy bằng FolderBrowserDialog
        {
            string fileName = System.IO.Path.Combine(filePath, "ExportedGuidList.txt");
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(guid.ToString());
            }
        }

        public List<Guid> ReadGuidFromTextFile(string fileName) // fileName là đường dẫn tới folder, lấy bằng OpenFileDialog
        {
            string[]? lines = File.ReadAllLines(fileName);
            List<Guid> guidList = new List<Guid>();

            for (int i = lines.Length - 1; i >= 0; i--)
            {
                guidList.Add(Guid.Parse(lines[i]));
            }

            return guidList;
        }
    }
}