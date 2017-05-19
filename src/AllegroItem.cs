using System;
using System.Text;

namespace AllegroLINQ.src
{
    class AllegroItem
    {
        public string Title { get; set; }
        public Uri ItemUri { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nTitle - ");
            sb.Append(Title);
            sb.Append("\nItemUri - ");
            sb.Append(ItemUri);
            return sb.ToString();
        }
    }
}