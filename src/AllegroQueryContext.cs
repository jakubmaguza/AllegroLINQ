using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace AllegroLINQ.src
{
    class AllegroQueryContext
    {
        public static object Execute(Expression expression)
        {
            return new List<AllegroItem>
            {
                new AllegroItem {Title = "C# cośtam", ItemUri = new Uri("http://www.c1.com/")},
                new AllegroItem {Title = "C#", ItemUri = new Uri("http://www.c2.com/")}
            };
        }
    }
}
