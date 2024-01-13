using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public int currentIndex { get; set; } = -1;
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

        // Trung >>>>

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

            //vm.mediaSource = listMediaSource[sourceIndex];
            //currentIndex = sourceIndex;
            playAFile(sourceIndex);
        }

        // Trung <<<<<

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
            // Trung <<<<
            if (shufleMode)
            {
                SetRandomSource();
            }
            else
            {
                var newIndex = listMediaSource.IndexOf(vm.mediaSource) + 1 >= listMediaSource.Count ? 0 : listMediaSource.IndexOf(vm.mediaSource) + 1;
                //vm.mediaSource = listMediaSource[newIndex];
                //currentIndex = newIndex;
                playAFile(newIndex);
            }
            _timer.Start();
            myMediaElement.Play();
            // Trung >>>>
        }

        void InitializePropertyValues()
        {
            // Trung <<<<
            myMediaElement.Volume = (double)volumeSlider.Value;
            // Trung >>>>
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

        // Trung <<<<

        private void OnMouseDownPreMedia(object sender, MouseButtonEventArgs e)
        {
            saveTimeSpan();

            if (shufleMode)
            {
                SetRandomSource();
            }
            else
            {
                var newIndex = listMediaSource.IndexOf(vm.mediaSource) - 1 < 0 ? listMediaSource.Count - 1 : listMediaSource.IndexOf(vm.mediaSource) - 1;
                //vm.mediaSource = listMediaSource[newIndex];
                //currentIndex = newIndex;
                playAFile(newIndex);
            }
        }

        private void OnMouseDownNextMedia(object sender, MouseButtonEventArgs e)
        {
            saveTimeSpan();

            if (shufleMode)
            {
                SetRandomSource();
            }
            else
            {
                var newIndex = listMediaSource.IndexOf(vm.mediaSource) + 1 >= listMediaSource.Count ? 0 : listMediaSource.IndexOf(vm.mediaSource) + 1;
                //vm.mediaSource = listMediaSource[newIndex];
                //currentIndex = newIndex;
                playAFile(newIndex);
            }
        }

        // Trung >>>>

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
            previewMediaElement.Volume = 0f;
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

        // Xuan <<<<

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            saveTimeSpan();

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
            //vm.mediaSource = listMediaSource![0];
            //currentIndex = 0;
            if (listMediaSource.Count > 0)
            {
                playAFile(0);
            }

            myMediaElement.Play();
            _timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var curPosition = myMediaElement.Position;
            //todo

            var mediaFile = (MediaFile)(((Button)(sender)).DataContext);
            //currentIndex = listMediaSource.IndexOf(fileToUri(mediaFile));
            //vm.mediaSource = listMediaSource[currentIndex];
            playAFile(listMediaSource.IndexOf(fileToUri(mediaFile)));
        }

        private void playAFile(int index)
        {
            saveTimeSpan();

            currentIndex = index;
            vm.mediaSource = listMediaSource[currentIndex];
            myMediaElement.Position = vm.MediaFiles[index].StartTime;
            myMediaElement.Play();
            displayPlayingFile();
        }

        private  void saveTimeSpan()
        {
            if (string.IsNullOrEmpty(vm.mediaSource?.ToString()) == false && currentIndex != -1)
            {
                var currentFile = vm.MediaFiles[currentIndex];
                currentFile.StartTime = myMediaElement.Position;
                 vm.SaveTimeSpanCommand.Execute(currentFile);
            }
        }

        // Xuan >>>>
    }
}
