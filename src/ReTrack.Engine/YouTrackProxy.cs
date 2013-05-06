using System;
using System.Collections.Generic;
using System.Linq;
using YouTrackSharp.Infrastructure;
using YouTrackSharp.Issues;

namespace ReTrack
{
  public class YouTrackProxy : IDisposable
  {
    private readonly IConnection connection;
    private const int sensibleQueryLimit = 20;

    public YouTrackProxy(ReTrackSettings s) : 
      this(s.YouTrackUsername, s.YouTrackPassword, s.YouTrackHost, s.YouTrackPort, s.YouTrackUseSsl, s.YouTrackPath)
    {
      
    }

    public YouTrackProxy(string username, string password, string host, int port = 80, bool useSSL = false, 
      string path = null)
    {
      connection = new Connection(host, port, useSSL, path);
      connection.Authenticate(username, password);
    }

    /// <summary>
    /// Simple query, yields <c>ShortIssue</c> objects. Can drill down later if needed.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public IList<ShortIssue> Query(string s)
    {
      var im = new IssueManagement(connection);
      return im.GetIssuesBySearch(s, sensibleQueryLimit, 0)
               .Select((dynamic i) => new ShortIssue(i.Id, i.summary))
               .ToList();
    }

    public void Dispose()
    {
      if (connection.IsAuthenticated) connection.Logout();
    }
  }
}
