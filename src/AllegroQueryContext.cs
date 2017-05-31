using System.Linq.Expressions;
using System.Xml.Linq;

namespace AllegroLINQ.src
{
    class AllegroQueryContext
    {
        public static object Execute(Expression expression)
        {
            var stringURL = new AllegroQueryContext().TranslateExpressionTree(expression);
            XElement _allegroRSS = XElement.Load(stringURL);
            return AllegroHelper.ParseAllegroXml(_allegroRSS);
        }

        private string TranslateExpressionTree(Expression expression)
        {
            return new ExpressionTreeTranslator().Translate(expression);
        }
    }
}
