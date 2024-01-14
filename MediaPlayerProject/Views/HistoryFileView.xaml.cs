﻿using MediaPlayerProject.Models;
using MediaPlayerProject.ViewModels;
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

namespace MediaPlayerProject.Views
{
    /// <summary>
    /// Interaction logic for HistoryFileView.xaml
    /// </summary>
    public partial class HistoryFileView : UserControl
    {
        public HistoryFileView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (HistoryFileViewModel)this.DataContext;
            var mediafile = (MediaFile)((ListViewItem)sender).DataContext;
            viewModel.GetMediaFileData = () => mediafile;
            viewModel.PlayMediaFileCommand.Execute(null);
        }
    }
}
