using System.Text;

namespace ReTrack
{
  using JetBrains.Application;
  using JetBrains.Application.Settings;
  using Engine;
  using Microsoft.VisualStudio.Shell.Interop;
  using JetBrains.DataFlow;
  using Settings;
  using System.Collections.Generic;
  using System.Net;
  using System.Xml.Linq;
  using NuGet;

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

    public IEnumerable<string> GetCompletionOptionsFor(string filter)
    {
      var url = store.GetValueProperty<ReTrackSettingsReSharper, string>(lifetime, x => x.YouTrackUrl);
      string path = url.Value + "/rest/issue/intellisense?filter=" + WebUtility.HtmlEncode(filter);

      var req = WebRequest.Create(path);
      var resp = req.GetResponse();
      var str = resp.GetResponseStream();
      string s = str.ReadToEnd();

      var xe = XElement.Parse(s);
      
      // there are lots of *awesome* highlighting info here that we simply must use
      var suggest = xe.Element("suggest");
      if (suggest != null)
      {
        var items = suggest.Elements("item");
        foreach (var item in items)
        {
          // concatenate prefix, option and suffix
          var prefixItem = item.Element("prefix");
          var optionItem = item.Element("option");
          var suffixItem = item.Element("suffix");

          var sb = new StringBuilder();
          if (prefixItem != null) sb.Append(prefixItem.Value);
          if (optionItem != null) sb.Append(optionItem.Value);
          if (suffixItem != null) sb.Append(suffixItem.Value);

          yield return sb.ToString();
        }
      }
    }
  }
}