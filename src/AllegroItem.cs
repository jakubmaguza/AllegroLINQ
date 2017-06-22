using System;
using System.Text;

namespace AllegroLINQ.src
{
    class AllegroItem
    {
        public string Title { get; set; }
        public bool SearchInDescription { get; set; }
        public string ExcludeWords { get; set; }
        public double Price { get; set; }
        public int CategoryID { get; set; }
        public OfferType OfferType { get; set; }
        public SearchType SearchType { get; set; }
        public bool CollectInPerson { get; set; }
        public bool PayU { get; set; }
        public Uri ItemUri { get; set; }
        public DateTime PubDate { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nTitle - ");
            sb.Append(Title);
            sb.Append("\nItemUri - ");
            sb.Append(ItemUri);
            sb.Append("\nPublication Date - ");
            sb.Append(PubDate.ToString("F"));
            return sb.ToString();
        }

    }
    enum OfferType
    {
        NotSet,
        BuyNow,
        Auction
    }
    enum SearchType
    {
        NotSet,
        AllWords,
        AnyWords
    }

    public class Results
    {
        public string Title { get; set; }
        public Uri ItemUri { get; set; }
        public DateTime PubDate { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\nTitle - ");
            sb.Append(Title);
            sb.Append("\nItemUri - ");
            sb.Append(ItemUri);
            sb.Append("\nPublication Date - ");
            sb.Append(PubDate.ToString("F"));
            return sb.ToString();
        }
    }
}