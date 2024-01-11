﻿using MediaPlayerProject.Commands;
using MediaPlayerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaPlayerProject.ViewModels
{
    public class AddPlaylistViewModel : ViewModelBase
    {
        public string? PlaylistName { get; set; }
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public AddPlaylistViewModel(PlaylistList playlistList, Services.NavigationService playlistListViewNaviationService)
        {
            SubmitCommand = new AddPlaylistCommand(this, playlistList, playlistListViewNaviationService);
            CancelCommand = new NavigateCommand(playlistListViewNaviationService);
        }
    }
}
