namespace ReTrack.Navigation
{
  using System.Collections.Generic;
  using JetBrains.IDE;
  using JetBrains.ProjectModel;
  using JetBrains.ReSharper.Feature.Services.Navigation.Search;
  using JetBrains.ReSharper.Feature.Services.Occurences;
  using JetBrains.ReSharper.Psi;
  using JetBrains.UI.PopupWindowManager;
  using JetBrains.Util;
  using JetBrains.Application;

  public class YouTrackIssueOccurence : IOccurence
  {
    readonly TextRange range = new TextRange();

    public bool Navigate(ISolution solution, PopupWindowContextSource windowContext, bool transferFocus,
      TabOptions tabOptions = TabOptions.Default)
    {
      // this literally takes you to the issue
      Shell.Instance.GetComponent<YouTrackService>().NavigateToIssue(IssueId);

      // soon!
      return true;
    }

    public string DumpToString()
    {
      return "Issue search :)";
    }

    public TextRange TextRange {
      get { return range; }
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

    public string IssueId { get; set; }
    public string IssueDescription { get; set; }
    
  }
}