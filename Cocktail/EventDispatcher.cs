using System;
using System.Collections.Generic;
using IdeaBlade.Core;
using IdeaBlade.EntityModel;

namespace Cocktail
{
    internal class EventDispatcher<T> where T : EntityManager
    {
        private readonly IEnumerable<EntityManagerDelegate<T>> _interceptors;

        public EventDispatcher(IEnumerable<EntityManagerDelegate<T>> interceptors)
        {
            _interceptors = interceptors;
        }

        internal void InstallEventHandlers(IAuthenticationService authenticationService)
        {
            authenticationService.PrincipalChanged +=
                new EventHandler<EventArgs>(OnPrincipalChanged).MakeWeak(
                    eh => authenticationService.PrincipalChanged -= eh);
        }

        internal void InstallEventHandlers(T manager)
        {
            manager.Cleared +=
                new EventHandler<EntityManagerClearedEventArgs>(OnCleared).MakeWeak(eh => manager.Cleared -= eh);
            manager.EntityChanged +=
                new EventHandler<EntityChangedEventArgs>(OnEntityChanged).MakeWeak(eh => manager.EntityChanged -= eh);
            manager.EntityChanging +=
                new EventHandler<EntityChangingEventArgs>(OnEntityChanging).MakeWeak(
                    eh => manager.EntityChanging -= eh);
            manager.EntityServerError +=
                new EventHandler<EntityServerErrorEventArgs>(OnEntityServerError).MakeWeak(
                    eh => manager.EntityServerError -= eh);
            manager.Fetching +=
                new EventHandler<EntityFetchingEventArgs>(OnFetching).MakeWeak(eh => manager.Fetching -= eh);
            manager.Queried +=
                new EventHandler<EntityQueriedEventArgs>(OnQueried).MakeWeak(eh => manager.Queried -= eh);
            manager.Querying +=
                new EventHandler<EntityQueryingEventArgs>(OnQuerying).MakeWeak(eh => manager.Querying -= eh);
            manager.Saving += new EventHandler<EntitySavingEventArgs>(OnSaving).MakeWeak(eh => manager.Saving -= eh);
            manager.Saved += new EventHandler<EntitySavedEventArgs>(OnSaved).MakeWeak(eh => manager.Saved -= eh);
        }

        internal void OnQueried(object sender, EntityQueriedEventArgs e)
        {
            _interceptors.ForEach(i => i.OnQueried((T) sender, e));
        }

        internal void OnFetching(object sender, EntityFetchingEventArgs e)
        {
            _interceptors.ForEach(i => i.OnFetching((T) sender, e));
        }

        internal void OnEntityServerError(object sender, EntityServerErrorEventArgs e)
        {
            _interceptors.ForEach(i => i.OnEntityServerError((T) sender, e));
        }

        internal void OnEntityChanging(object sender, EntityChangingEventArgs e)
        {
            _interceptors.ForEach(i => i.OnEntityChanging((T) sender, e));
        }

        internal void OnCleared(object sender, EntityManagerClearedEventArgs e)
        {
            _interceptors.ForEach(i => i.OnCleared((T) sender, e));
        }

        internal void OnEntityChanged(object sender, EntityChangedEventArgs e)
        {
            _interceptors.ForEach(i => i.OnEntityChanged((T) sender, e));
        }

        internal void OnPrincipalChanged(object sender, EventArgs e)
        {
            PrincipalChanged(sender, e);
        }

        internal void OnQuerying(object sender, EntityQueryingEventArgs e)
        {
            _interceptors.ForEach(i => i.OnQuerying((T) sender, e));

            Querying(sender, e);
        }

        internal void OnSaving(object sender, EntitySavingEventArgs e)
        {
            _interceptors.ForEach(i => i.OnSaving((T) sender, e));

            Saving(sender, e);
        }

        internal void OnSaved(object sender, EntitySavedEventArgs e)
        {
            _interceptors.ForEach(i => i.OnSaved((T) sender, e));

            Saved(sender, e);
        }

        internal event EventHandler<EventArgs> PrincipalChanged = delegate { };
        internal event EventHandler<EntityQueryingEventArgs> Querying = delegate { };
        internal event EventHandler<EntitySavingEventArgs> Saving = delegate { };
        internal event EventHandler<EntitySavedEventArgs> Saved = delegate { };
    }
}