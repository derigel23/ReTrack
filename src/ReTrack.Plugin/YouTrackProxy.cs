using System;
using YouTrackSharp.Infrastructure;

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

    public void Dispose()
    {
      if (connection.IsAuthenticated) connection.Logout();
    }
  }
}
