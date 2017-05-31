using System.Net;
using System.Text;

namespace AllegroLINQ.src
{
    class AllegroSearchFields
    {
        private static readonly string baseURL = "http://allegro.pl/rss.php/search?";
        public string Title { get; set; }

        public string BuildURL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(baseURL);
            sb.Append("string=").Append(WebUtility.UrlEncode(Title));
            return sb.ToString();
        }
    }
}
