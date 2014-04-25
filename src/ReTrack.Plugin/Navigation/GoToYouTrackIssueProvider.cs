using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Feature.Services.Goto;
using JetBrains.ReSharper.Feature.Services.Navigation.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.Text;

namespace ReTrack.Navigation
{
  [ShellFeaturePart]
  class GoToYouTrackIssueProvider : IGotoEverythingProvider
  {
    private YouTrackService yt;

    public GoToYouTrackIssueProvider(YouTrackService yt)
    {
      this.yt = yt;
    }

    public bool IsApplicable(INavigationScope scope, GotoContext gotoContext, IdentifierMatcher matcher)
    {
      // not sure why we have to explicitly do this
      if (matcher.Filter.Length == 0) return false;

      // oops: this means feature only works with open sln
      return scope is SolutionNavigationScope;
    }

    public IEnumerable<MatchingInfo> FindMatchingInfos(IdentifierMatcher matcher, INavigationScope scope, GotoContext gotoContext,
      Func<bool> checkForInterrupt)
    {
      var indices = matcher.MatchingIndicies(matcher.Filter);
      var matchingInfo = new MatchingInfo(matcher.Filter, indices);
      yield return matchingInfo;
    }

    public IEnumerable<IOccurence> GetOccurencesByMatchingInfo(MatchingInfo navigationInfo, INavigationScope scope, GotoContext gotoContext,
      Func<bool> checkForInterrupt)
    {
      // send this off to the server to be processed
      foreach (string s in yt.GetCompletionOptionsFor(navigationInfo.Identifier))
        yield return new YouTrackIssueOccurence { IssueId = navigationInfo.Identifier, IssueDescription = s };

    }

    public Func<int, int> ItemsPriorityFunc { get { return _ => _; } }
  }
}