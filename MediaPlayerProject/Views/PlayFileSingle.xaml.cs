using MediaPlayerProject.Helpers;
using MediaPlayerProject.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MediaPlayerProject.Views
{
    /// <summary>
    /// Interaction logic for PlayFileSingle.xaml
    /// </summary>
    public partial class PlayFileSingle : UserControl
    {


        private void UC_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private bool _isPlaying;
        private void SwitchPlayPauseButton(bool notPlay)
        {
            _isPlaying = !notPlay;
            if (!notPlay)
            {
                this.PlayButton.Visibility = Visibility.Collapsed;
                this.PauseButton.Visibility = Visibility.Visible;
            }
            else
            {
                this.PlayButton.Visibility = Visibility.Visible;
                this.PauseButton.Visibility = Visibility.Collapsed;
            }
        }

        private DispatcherTimer _timer;
        private DispatcherTimer _previewTimer;
        private DateTime _lastUpdate = DateTime.Now;

        private PlayFileSingleViewModel? Vm { get; set; }

        void TogglePlay(bool play)
        {
            if (play)
            {
                myMediaElement.Play();
                _timer.Start();
                SwitchPlayPauseButton(false);
            }
            else
            {
                myMediaElement.Pause();
                _timer.Stop();
                SwitchPlayPauseButton(true);
            }
        }

        public PlayFileSingle()

        {
            InitializeComponent();
            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;

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

        void OnMouseDownPlayMedia(object sender, RoutedEventArgs e)
        {
            myMediaElement.Play();
            _timer.Start();
            SwitchPlayPauseButton(false);
        }

        void OnMouseDownPauseMedia(object sender, RoutedEventArgs e)
        {
            myMediaElement.Pause();
            _timer.Stop();
            SwitchPlayPauseButton(true);
        }

        void OnMouseStartOverMedia(object sender, RoutedEventArgs e) // Tua video lại từ đầu, chưa có nút
        {
            myMediaElement.Stop();
            _timer.Stop();
            myMediaElement.Play();
            _timer.Start();
            SwitchPlayPauseButton(false);
        }

        private void ChangeMediaSpeedRatio(object sender, SelectionChangedEventArgs e)
        {
            string? selectedSpeedRatio = ((ComboBoxItem)speedRatioComboBox.SelectedItem).Content.ToString();
            if (selectedSpeedRatio != null)
            {
                double speedRatio = double.Parse(selectedSpeedRatio.Trim('x'));
                myMediaElement.SpeedRatio = speedRatio;
            }
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            OnMouseStartOverMedia(sender, null!);
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

        // Nút tua + tua ngược 5s
        private void OnSkipBackward(object sender, RoutedEventArgs e)
        {
            myMediaElement.Position = myMediaElement.Position.Subtract(TimeSpan.FromSeconds(5));
        }

        private void OnSkipForward(object sender, RoutedEventArgs e)
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

                Point position = e.GetPosition(this.SliderContainer);
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
            Vm!.BackCommand.Execute(null);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Vm = (PlayFileSingleViewModel)this.DataContext;
            myMediaElement.Play();
            _timer.Start();
            SwitchPlayPauseButton(false);

            HotkeysManager.AddHotkey(ModifierKeys.Alt, Key.S, () => TogglePlay(!_isPlaying));
        }

        private void Dispatcher_ShutdownStarted(object? sender, EventArgs e)
        {
            // TODO:
            //Vm!.SaveTimeSpan();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.ShutdownStarted -= Dispatcher_ShutdownStarted;
            // TODO:
            //Vm!.SaveTimeSpan();
        }
    }
}
