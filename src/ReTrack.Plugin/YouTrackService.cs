using System.Linq.Expressions;
using JetBrains.DataFlow;
using JetBrains.Reflection;
using JetBrains.ReSharper.Feature.Services.CallHierarchy.Impl;
using ReTrack.Settings;

namespace ReTrack
{
  using JetBrains.Application;
  using JetBrains.Application.Settings;
  using Engine;
  using Microsoft.VisualStudio.Shell.Interop;

  [ShellComponent]
  public class YouTrackService : YouTrackProxy
  {
    private readonly IVsWebBrowsingService wbs;
    private readonly Lifetime lifetime;
    private readonly IContextBoundSettingsStoreLive store;

    public YouTrackService(ISettingsStore store, IVsWebBrowsingService wbs, Lifetime lifetime)
    {
      this.wbs = wbs;
      this.lifetime = lifetime;
      this.store = store.BindToContextLive(lifetime, ContextRange.ApplicationWide);
    }

    public void NavigateToIssue(string id)
    {
      var url = store.GetValueProperty<ReTrackSettingsReSharper, string>(lifetime, x => x.YouTrackUrl);

      string path = string.Format("{0}/issue/{1}", url.Value, id);
      IVsWindowFrame _;
      if (wbs != null) wbs.Navigate(path, 0, out _);
    }
  }
}