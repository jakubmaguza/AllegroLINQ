using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AllegroLINQ.src
{
    class ExpressionTreeTranslator
    {
        private AllegroSearchFields _Criteria;
        public string Translate(Expression expression)
        {
            _Criteria = new AllegroSearchFields();
            Visit(expression);
            return _Criteria.BuildURL();
        }

        private void Visit(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Call:
                    VisitMethodCall((MethodCallExpression)expression);
                    break;
                case ExpressionType.Equal:
                    VisitEqual((BinaryExpression)expression);
                    break;
                case ExpressionType.Lambda:
                    Visit(((LambdaExpression)expression).Body);
                    break;
            }
        }
        private void VisitMethodCall(MethodCallExpression expression)
        {
            if ((expression.Method.DeclaringType == typeof(Queryable)) &&
                (expression.Method.Name == "Where"))
            {
                Visit(((UnaryExpression)expression.Arguments[1]).Operand);
            }
            else
            {
                throw new NotSupportedException("Method not supported: " + expression.Method.Name);
            }
        }
        private void VisitEqual(BinaryExpression expression)
        {
            if ((expression.Left.NodeType == ExpressionType.MemberAccess) &&
                (((MemberExpression)expression.Left).Member.Name == "Title"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.Title = (String)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.Title = (String)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for Title: " + expression.Right.NodeType);
            }
            else
            {
                throw new NotSupportedException("Cannot search by: " + ((MemberExpression)expression.Left).Member.Name);
            }
        }
        private Object GetMemberValue(MemberExpression memberExpression)
        {
            MemberInfo memberInfo;
            Object obj;

            if (memberExpression == null)
                throw new ArgumentNullException("memberExpression");

            // Get object
            if (memberExpression.Expression is ConstantExpression)
                obj = ((ConstantExpression)memberExpression.Expression).Value;
            else if (memberExpression.Expression is MemberExpression)
                obj = GetMemberValue((MemberExpression)memberExpression.Expression);
            else
                throw new NotSupportedException("Expression type not supported: " + memberExpression.Expression.GetType().FullName);

            // Get value
            memberInfo = memberExpression.Member;
            if (memberInfo is PropertyInfo)
            {
                PropertyInfo property = (PropertyInfo)memberInfo;
                return property.GetValue(obj, null);
            }
            if (memberInfo is FieldInfo)
            {
                FieldInfo field = (FieldInfo)memberInfo;
                return field.GetValue(obj);
            }
            throw new NotSupportedException("MemberInfo type not supported: " + memberInfo.GetType().FullName);

        }
    }
}
