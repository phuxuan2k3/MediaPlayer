using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace MediaPlayerProject.Views
{
    /// <summary>
    /// Interaction logic for PlayFileView.xaml
    /// </summary>
    public partial class PlayFileView : UserControl
    {
        private DispatcherTimer _timer;
        private DispatcherTimer _previewTimer;
        private DateTime _lastUpdate = DateTime.Now;


        public int currentIndex { get; set; }
        public bool shufleMode { get; set; } = false;
        public List<Uri> listMediaSource { get; set; }
        private PlayFileViewModel vm { get; set; }
        public PlayFileView()
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
        private Uri fileToUri(MediaFile m)
        {
            return new Uri(m.Path + "\\" + m.FileName);
        }
        private void displayPlayingFile()
        {
            FilesListView.SelectedItem = FilesListView.Items[currentIndex];
            FilesListView.Focus();
        }

        private void SetRandomSource()
        {
            int sourceIndex = -1;
            do
            {
                Random rnd = new Random();
                sourceIndex = rnd.Next(listMediaSource.Count);
            } while (vm.mediaSource == listMediaSource[sourceIndex]);
            vm.mediaSource = listMediaSource[sourceIndex];
            currentIndex = sourceIndex;
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

            if (shufleMode)
            {
                SetRandomSource();
            }
            else
            {
                var newIndex = listMediaSource.IndexOf(vm.mediaSource) + 1 >= listMediaSource.Count ? 0 : listMediaSource.IndexOf(vm.mediaSource) + 1;
                vm.mediaSource = listMediaSource[newIndex];
                currentIndex = newIndex;
            }
            _timer.Start();
            myMediaElement.Play();
            displayPlayingFile();
        }

        void InitializePropertyValues()
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
            myMediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        // Cập nhật thanh slider 200ms 1 lần

        private void Timer_Tick(object? sender = null, EventArgs? e = null)
        {
            if (!timelineSlider.IsMouseCaptureWithin)
            {
                timelineSlider.Value = myMediaElement.Position.TotalMilliseconds;
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

        private void OnMouseDownPreMedia(object sender, MouseButtonEventArgs e)
        {
            if (shufleMode)
            {
                SetRandomSource();
            }
            else
            {
                var newIndex = listMediaSource.IndexOf(vm.mediaSource) - 1 < 0 ? listMediaSource.Count - 1 : listMediaSource.IndexOf(vm.mediaSource) - 1;
                vm.mediaSource = listMediaSource[newIndex];
                currentIndex = newIndex;
            }
            displayPlayingFile();
        }

        private void OnMouseDownNextMedia(object sender, MouseButtonEventArgs e)
        {
            if (shufleMode)
            {
                SetRandomSource();
            }
            else
            {
                var newIndex = listMediaSource.IndexOf(vm.mediaSource) + 1 >= listMediaSource.Count ? 0 : listMediaSource.IndexOf(vm.mediaSource) + 1;
                vm.mediaSource = listMediaSource[newIndex];
                currentIndex = newIndex;
            }
            displayPlayingFile();
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
                double mousePos = e.GetPosition(timelineSlider).X; // px_dex -200 là tại vì t thêm cái cột bên trái, nếu xoá cột này thì ko trừ nữa
                double sliderValue = mousePos / timelineSlider.ActualWidth * timelineSlider.Maximum;
                previewMediaElement.Position = TimeSpan.FromMilliseconds(sliderValue);
                previewMediaElement.Play();

                Point position = e.GetPosition(this);
                Canvas.SetLeft(previewMediaElement, position.X - 60); //60 là width cái previewMediaElement

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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PlayFileViewModel)this.DataContext;
            var plalist = vm.PlaylistData;
            vm.BackCommand.Execute(plalist);
        }

        private void ShuffleMode_Click(object sender, RoutedEventArgs e)
        {
            this.shufleMode = !shufleMode;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            vm = (PlayFileViewModel)this.DataContext;
            listMediaSource = new List<Uri>();

            foreach (var item in vm.MediaFiles)
            {
                listMediaSource!.Add(fileToUri(item));
            }
            vm.mediaSource = listMediaSource![0];
            currentIndex = 0;

            myMediaElement.Play();

            displayPlayingFile();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mediaFile = (MediaFile)(((Button)(sender)).DataContext);
            currentIndex = listMediaSource.IndexOf(fileToUri(mediaFile));
            vm.mediaSource = listMediaSource[currentIndex];
            displayPlayingFile();
        }
    }
}
