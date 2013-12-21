using ReTrack.Engine.Models;
using YouTrackSharp.Issues;

namespace ReTrack.Engine.Mappers
{
  public class IssueToShortIssueMapper
      : IMapper<Issue, ShortIssue>
  {
    public string BaseUrl { get; set; }

    public IssueToShortIssueMapper(string baseUrl)
    {
      if (baseUrl.EndsWith("/"))
      {
        baseUrl = baseUrl.TrimEnd('/');
      }
      BaseUrl = baseUrl;
    }

    public ShortIssue Map(Issue origin)
    {
      const string missing = "<missing>";
      dynamic issue = origin;

      // type and state could easily be absent
      string type, state;
      try { type = issue.type; }
      catch { type = missing; }
      try { state = issue.state; }
      catch { state = missing; }

      return new ShortIssue(origin.Id, issue.summary.ToString(), type, state, string.Format("{0}/issue/{1}", BaseUrl, origin.Id));
    }
  }
}