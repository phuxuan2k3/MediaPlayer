using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
using System.Windows.Controls;

namespace MediaPlayerProject.Views
{
    /// <summary>
    /// Interaction logic for MediaFilePoolView.xaml
    /// </summary>
    public partial class MediaFilePoolView : UserControl
    {
        public MediaFilePoolView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = (MediaFilePoolViewModel)this.DataContext;
            var mediafile = (MediaFile)((ListViewItem)sender).DataContext;
            viewModel.GetMediaFileData = () => mediafile;
            viewModel.PlayMediaFileCommand.Execute(null);

            HistoryHelper historyHelper = new HistoryHelper();
            historyHelper.WriteGuidToTextFile(mediafile.Id);
        }
    }
}
