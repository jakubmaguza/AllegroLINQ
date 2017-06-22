using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace AllegroLINQ.src
{
    class AllegroSearchFields
    {
        private static readonly string baseURL = "http://allegro.pl/rss.php/search?";
        public string Title { get; set; }
        public bool SearchInDescription { get; set; }
        public string ExcludeWords { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public int CategoryID { get; set; }
        public OfferType OfferType { get; set; }
        public SearchType SearchType { get; set; }
        public bool CollectInPerson { get; set; }
        public bool PayU { get; set; }
        public string BuildURL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(baseURL);
            sb.Append("string=").Append(WebUtility.UrlEncode(Title));
            if (SearchType != SearchType.NotSet)
                sb.Append("&search_type=").Append(SearchType == SearchType.AllWords ? 1 : 3);
            if (SearchInDescription)
                sb.Append("&description=1");
            if (!String.IsNullOrEmpty(ExcludeWords))
                sb.Append("&exclude=").Append(ExcludeWords);
            if (PriceFrom != 0)
                sb.Append("&price_from=").Append(PriceFrom.ToString(CultureInfo.InvariantCulture));
            if (PriceTo != 0)
                sb.Append("&price_to=").Append(PriceTo.ToString(CultureInfo.InvariantCulture));
            if (OfferType != OfferType.NotSet)
                sb.Append("&offer_type=").Append(OfferType == OfferType.BuyNow ? 1 : 2);
            if(CollectInPerson)
                sb.Append("&personal_rec=1");
            if(PayU)
                sb.Append("&pay=1");
            if (CategoryID != 0)
                sb.Append("&category=").Append(CategoryID);
            return sb.ToString();
        }
    }
}
