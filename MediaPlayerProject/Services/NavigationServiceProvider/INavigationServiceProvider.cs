using MediaPlayerProject.ViewModels;
using System;

namespace MediaPlayerProject.Services.NavigationServiceProvider
{
    public interface INavigationServiceProvider
    {
        NavigationService GetNavigationService(Func<ViewModelBase> _createVM);
    }
}
