using System;
using System.Collections.Generic;
using System.Linq;
using YouTrackSharp.Infrastructure;
using YouTrackSharp.Issues;

namespace ReTrack
{
    public class YouTrackConnectionValidator
    {
        
    }
    public class YouTrackProxy : IDisposable
    {
        private readonly IConnection connection;
        private const int sensibleQueryLimit = 20;

        public YouTrackProxy(ReTrackSettings s) :
            this(s.YouTrackUsername, s.YouTrackPassword, new Uri(s.YouTrackUrl))
        {
        }

        public YouTrackProxy(string username, string password, Uri url)
        {
            connection = new Connection(url.Host, url.Port, url.Scheme == "https", url.PathAndQuery);
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