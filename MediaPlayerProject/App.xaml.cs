using MediaPlayerProject.DbContexts;
using MediaPlayerProject.Helpers;
using MediaPlayerProject.Models;
using MediaPlayerProject.Services;
using MediaPlayerProject.Services.HistoryFileProvider;
using MediaPlayerProject.Services.MediaFileCreator;
using MediaPlayerProject.Services.MediaFIlePoolProvider;
using MediaPlayerProject.Services.MediaFileProviders;
using MediaPlayerProject.Services.NavigationServiceProvider;
using MediaPlayerProject.Services.PlaylistCreators;
using MediaPlayerProject.Services.PlaylistDelete;
using MediaPlayerProject.Services.PlaylistProviders;
using MediaPlayerProject.Services.RemoveMediaFile;
using MediaPlayerProject.Services.RemoveMediaFilePool;
using MediaPlayerProject.Services.SaveMediaFileTimeSpan;
using MediaPlayerProject.Stores;
using MediaPlayerProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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

        public IHost Host
        {
            get;
        }
        public static T GetService<T>()
    where T : class
        {
            var host = (Current as App)!.Host;
            var obj = host.Services.GetService(typeof(T));
            if (obj is not T serviecRes)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }
            return serviecRes;
        }

        public App()
        {

            Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                UseContentRoot(AppContext.BaseDirectory).
                ConfigureServices((context, services) =>
                {
                    services.AddSingleton(typeof(PlaylistListDbContextFactory), x => ActivatorUtilities.CreateInstance(x, typeof(PlaylistListDbContextFactory), CONNECTION_STRING));
                    services.AddSingleton<IPlaylistCreators, DatabasePlaylistCreator>();
                    services.AddSingleton<IPlaylistProvider, DatabasePlaylistProvider>();
                    services.AddSingleton<IPlaylistDelete, DatabasePlaylistDelete>();
                    services.AddSingleton<IMediaFileProvider, DatabaseMediaFileProvider>();
                    services.AddSingleton<IMediaFileCreator, DatabaseMediaFileCreator>();
                    services.AddSingleton<IRemoveMediaFile, DatabaseRemoveMediaFile>();
                    services.AddSingleton<IMediaFIlePoolProvider, DatabaseMediaFilePoolProvider>();
                    services.AddSingleton<IMediaFileCreator, DatabaseMediaFileCreator>();
                    services.AddSingleton<IRemoveMediaFilePool, DatabaseRemoveMediaFilePool>();
                    services.AddSingleton<ISaveMediaFileTimeSpan, SaveMediaFileTimeSpan>();
                    services.AddSingleton<IHistoryFileProvider, HistoryFileProvider>();

                    // Navigation
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<INavigationServiceProvider, NavigationServiceProvider>();
                }).Build();

            this.playlistList = new PlaylistList();
            this.playlistListDbContextFactory = GetService<PlaylistListDbContextFactory>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (PlaylistListDbContext playlistListDbContext = playlistListDbContextFactory.CreateDbContext())
            {
                playlistListDbContext.Database.Migrate();
            }
            var ns = GetService<NavigationStore>();
            ns.CurrentViewModel = PlaylistListingViewModel.LoadViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
            HotkeysManager.SetupSystemHook();

            base.OnStartup(e);
        }
    }
}
