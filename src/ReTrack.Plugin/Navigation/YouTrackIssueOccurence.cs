using System.Collections.Generic;
using JetBrains.IDE;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Navigation.Search;
using JetBrains.ReSharper.Feature.Services.Occurences;
using JetBrains.ReSharper.Psi;
using JetBrains.UI.PopupWindowManager;
using JetBrains.Util;

namespace ReTrack.Navigation
{
  public class YouTrackIssueOccurence : IOccurence
  {
    public bool Navigate(ISolution solution, PopupWindowContextSource windowContext, bool transferFocus,
      TabOptions tabOptions = TabOptions.Default)
    {
      // soon!
      return false;
    }

    public string DumpToString()
    {
      return "Issue search :)";
    }

    public TextRange TextRange {
      get { return new TextRange(); }
    }

    public ProjectModelElementEnvoy ProjectModelElementEnvoy
    {
      get { return null; }
    }

    public DeclaredElementEnvoy<ITypeMember> TypeMember
    {
      get { return null; }
    }

    public DeclaredElementEnvoy<ITypeElement> TypeElement
    {
      get { return null; }
    }

    public DeclaredElementEnvoy<INamespace> Namespace { get { return null; }}
    public OccurenceType OccurenceType {get { return OccurenceType.Occurence; }}
    public bool IsValid { get { return true; } }
    public object MergeKey { get { return null; } }
    public IList<IOccurence> MergedItems {get { return null; }}
    public OccurencePresentationOptions PresentationOptions { get; set; }
  }
}