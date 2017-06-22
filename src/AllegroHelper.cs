using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AllegroLINQ.src
{
    static class AllegroHelper
    {
        public static IEnumerable<AllegroItem> ParseAllegroXml(XElement xel)
        {
            var channelElements = xel.Elements().First().Elements("item");
            foreach (var employee in channelElements)
            {
                AllegroItem ai = new AllegroItem();
                ai.Title = employee.Element("title").Value;
                ai.ItemUri = new Uri(employee.Element("link").Value);
                ai.PubDate = DateTime.Parse(employee.Element("pubDate").Value);
                yield return ai;
            }
        }
        public static AllegroQueryable<AllegroItem> AllegroItems()
        {
            return new AllegroQueryable<AllegroItem>();
        }
    }
}
