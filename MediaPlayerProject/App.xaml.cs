using MediaPlayerProject.DbContexts;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.MediaFileCreators;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.PlaylistCreators;
using MediaPlayerProject.Services.PlaylistDelete;
using MediaPlayerProject.Services.PlaylistProviders;
using MediaPlayerProject.Stores;
using MediaPlayerProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MediaPlayerProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=PlaylistList.db";
        private readonly PlaylistList playlistList;
        private readonly NavigationStore navigationStore;
        private readonly PlaylistListDbContextFactory playlistListDbContextFactory;
        public App()
        {
            playlistListDbContextFactory = new PlaylistListDbContextFactory(CONNECTION_STRING);
            IPlaylistCreators playlistCreators = new DatabasePlaylistCreator(playlistListDbContextFactory);
            IPlaylistProvider playlistProvider = new DatabasePlaylistProvider(playlistListDbContextFactory);
            IPlaylistDelete playlistDeletor = new DatabasePlaylistDelete(playlistListDbContextFactory);
            IMediaFileProvider mediaFileProvider = new DatabaseMediaFileProvider(playlistListDbContextFactory);
            IMediaFileCreator mediaFileCreator = new DatabaseMediaFileCreator(playlistListDbContextFactory);

            this.playlistList = new PlaylistList(playlistCreators, playlistProvider, playlistDeletor, mediaFileProvider, mediaFileCreator);
            navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (PlaylistListDbContext playlistListDbContext = playlistListDbContextFactory.CreateDbContext())
            {
                playlistListDbContext.Database.Migrate();
            }

            navigationStore.CurrentViewModel = CreatePlaylistListingViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        private AddPlaylistViewModel CreateAddPlaylistViewModel()
        {
            return new AddPlaylistViewModel(playlistList, new NavigationService(navigationStore, CreatePlaylistListingViewModel));
        }

        private PlaylistListingViewModel CreatePlaylistListingViewModel()
        {
            return PlaylistListingViewModel.LoadViewModel(playlistList,
                new NavigationService(navigationStore, CreateAddPlaylistViewModel),
                (pl) => new NavigationService(navigationStore, () => CreateMediaFileListingViewModel(pl)));
        }

        private MediaFileListingViewModel CreateMediaFileListingViewModel(Playlist playlist)
        {
            return MediaFileListingViewModel.LoadViewModel(playlist, new NavigationService(navigationStore, CreatePlaylistListingViewModel));

        }
    }
}
