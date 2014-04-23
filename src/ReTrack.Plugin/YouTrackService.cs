using System;

namespace ReTrack
{
  using JetBrains.Application;
  using JetBrains.Application.Settings;
  using Engine;
  using Settings;

  [ShellComponent]
  public class YouTrackService : YouTrackProxy
  {
    public YouTrackService(ISettingsStore store)
    {
      var ctxStore = store.BindToContextTransient(ContextRange.ApplicationWide);
      var settings = ctxStore.GetKey<ReTrackSettingsReSharper>(SettingsOptimization.OptimizeDefault);

      // dull sanity check for now
      if (!string.IsNullOrWhiteSpace(settings.Url))
        Initialize(settings.Username, settings.Password, new Uri(settings.Url));
    }
  }
}