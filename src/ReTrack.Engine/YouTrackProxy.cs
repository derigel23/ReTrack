using System;
using System.Collections.Generic;
using YouTrackSharp.Infrastructure;
using YouTrackSharp.Issues;

namespace ReTrack
{
  public class YouTrackProxy : IDisposable
  {
    private readonly IConnection connection;

    public YouTrackProxy(string username, string password, string host, int port = 80, bool useSSL = false, string path = null)
    {
      connection = new Connection(host, port, useSSL, path);
      connection.Authenticate(username, password);
    }

    public IEnumerable<Issue> Query(string s)
    {
      IssueManagement im = new IssueManagement(connection);
      return im.GetIssuesBySearch(s);
    }

    public void Dispose()
    {
      if (connection.IsAuthenticated) connection.Logout();
    }
  }
}
