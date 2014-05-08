namespace ReTrack.Engine
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Mappers;
  using Models;
  using YouTrackSharp.Infrastructure;
  using YouTrackSharp.Issues;
  using YouTrackSharp.Projects;

  public class YouTrackProxy : IDisposable
  {
    protected IConnection Connection;
    public string BaseUrl { get; set; }
    private const int sensibleQueryLimit = 20;

    public YouTrackProxy(ReTrackSettings s) :
      this(s.Username, s.Password, new Uri(s.Url))
    {
      BaseUrl = s.Url;
    }

    public YouTrackProxy(string username, string password, Uri url)
    {
      Initialize(username, password, url);
    }

    public void Initialize(string username, string password, Uri url)
    {
      Connection = new Connection(url.Host, url.Port, url.Scheme == Uri.UriSchemeHttps, url.GetComponents(UriComponents.Path, UriFormat.UriEscaped).TrimEnd('/'));
      Connection.Authenticate(username, password);
    }

    protected YouTrackProxy()
    {
      
    }

    /// <summary>
    /// Simple query, yields <c>ShortProject</c> objects. Can drill down later if needed.
    /// </summary>
    /// <param name="startsWith"></param>
    /// <returns></returns>
    public IList<ShortProject> Projects(string startsWith)
    {
      startsWith = startsWith.ToLowerInvariant();

      var pm = new ProjectManagement(Connection);
      var mapper = new ProjectToShortProjectMapper();
      return pm.GetProjects().Where(p => p.Name.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase) || p.ShortName.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase))
          .Select(mapper.Map)
          .ToList();
    }

    /// <summary>
    /// Simple query, yields <c>ShortIssue</c> objects. Can drill down later if needed.
    /// </summary>
    /// <param name="projectShortName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public IList<ShortIssue> Query(string projectShortName, string query)
    {
      var im = new IssueManagement(Connection);
      if (!string.IsNullOrEmpty(projectShortName))
      {
        query = string.Format("project: {0} {1}", projectShortName, query);
      }

      var mapper = new IssueToShortIssueMapper(BaseUrl);
      return im.GetIssuesBySearch(query, sensibleQueryLimit, 0)
          .Select(mapper.Map)
          .ToList();
    }

    /// <summary>
    /// Simple query, yields <c>ShortComment</c> objects.
    /// </summary>
    /// <param name="issueId"></param>
    /// <returns></returns>
    public IList<ShortComment> QueryComments(string issueId)
    {
      var im = new IssueManagement(Connection);

      var mapper = new CommentToShortCommentMapper();
      return im.GetCommentsForIssue(issueId)
          .Select(mapper.Map)
          .ToList();
    }

    public void AttachFile(string issueId, string pathToFile)
    {
      var im = new IssueManagement(Connection);
      im.AttachFileToIssue(issueId, pathToFile);
    }

    public void SubmitComment(string issueId, string text)
    {
      var im = new IssueManagement(Connection);
      im.ApplyCommand(issueId, null, text);
    }

    public void Dispose()
    {
      if (Connection != null && Connection.IsAuthenticated) Connection.Logout();
    }
  }
}