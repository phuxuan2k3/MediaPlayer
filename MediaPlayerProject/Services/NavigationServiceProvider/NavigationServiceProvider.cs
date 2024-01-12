using MediaPlayerProject.Stores;
using MediaPlayerProject.ViewModels;
using System;

namespace MediaPlayerProject.Services.NavigationServiceProvider
{
    public class NavigationServiceProvider : INavigationServiceProvider
    {
        public NavigationService GetNavigationService(Func<ViewModelBase> _createVM)
        {
            var store = App.GetService<NavigationStore>();
            return new NavigationService(store, _createVM);
        }
    }
}
