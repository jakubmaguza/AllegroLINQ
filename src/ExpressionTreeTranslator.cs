﻿using System;
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
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.LessThan:
                    VisitLessThanOrEqual((BinaryExpression)expression);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.GreaterThan:
                    VisitGreaterThanOrEqual((BinaryExpression)expression);
                    break;

            }
        }
        private void VisitLessThanOrEqual(BinaryExpression expression)
        {
            if ((expression.Left.NodeType == ExpressionType.MemberAccess) &&
                (((MemberExpression)expression.Left).Member.Name == "Price"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.PriceTo = (Double)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.PriceTo = (Double)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for Price: " + expression.Right.NodeType);
            }
        }
        private void VisitGreaterThanOrEqual(BinaryExpression expression)
        {
            if ((expression.Left.NodeType == ExpressionType.MemberAccess) &&
                (((MemberExpression)expression.Left).Member.Name == "Price"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.PriceFrom = (Double)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.PriceFrom = (Double)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for Price: " + expression.Right.NodeType);
            }
        }
        private void VisitMethodCall(MethodCallExpression expression)
        {
            if ((expression.Method.DeclaringType == typeof(Queryable)) && (expression.Method.Name == "Where"))
            {
                Visit(expression.Arguments[0]);
                Visit(((UnaryExpression)expression.Arguments[1]).Operand);
            }
            else
            {
                throw new NotSupportedException("Method not supported: " + expression.Method.Name);
            }
        }
        private void VisitEqual(BinaryExpression expression)
        {
            if ((expression.Left.NodeType == ExpressionType.MemberAccess) && (((MemberExpression)expression.Left).Member.Name == "Title"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.Title = (String)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.Title = (String)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for Title: " + expression.Right.NodeType);
            }
            else if ((expression.Left.NodeType == ExpressionType.MemberAccess) && (((MemberExpression)expression.Left).Member.Name == "PayU"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.PayU = (bool)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.PayU = (bool)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for PayU: " + expression.Right.NodeType);
            }
            else if ((expression.Left.NodeType == ExpressionType.MemberAccess) && (((MemberExpression)expression.Left).Member.Name == "SearchInDescription"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.SearchInDescription = (bool)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.SearchInDescription = (bool)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for SearchInDescription: " + expression.Right.NodeType);
            }
            else if ((expression.Left.NodeType == ExpressionType.MemberAccess) && (((MemberExpression)expression.Left).Member.Name == "CollectInPerson"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.CollectInPerson = (bool)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.CollectInPerson = (bool)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for CollectInPerson: " + expression.Right.NodeType);
            }
            else if ((expression.Left.NodeType == ExpressionType.MemberAccess) && (((MemberExpression)expression.Left).Member.Name == "ExcludeWords"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.ExcludeWords = (String)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.ExcludeWords = (String)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for ExcludeWords: " + expression.Right.NodeType);
            }
            else if ((expression.Left.NodeType == ExpressionType.MemberAccess) && (((MemberExpression)expression.Left).Member.Name == "CategoryID"))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.CategoryID = (Int32)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.CategoryID = (Int32)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for CategoryID: " + expression.Right.NodeType);
            }
            else if ((expression.Left is UnaryExpression) &&
                     (((UnaryExpression)expression.Left).Operand.Type == typeof(OfferType)))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.OfferType = (OfferType)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.OfferType = (OfferType)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for OfferType: " + expression.Right.NodeType);
            }
            else if ((expression.Left is UnaryExpression) &&
                     (((UnaryExpression)expression.Left).Operand.Type == typeof(SearchType)))
            {
                if (expression.Right.NodeType == ExpressionType.Constant)
                    _Criteria.SearchType = (SearchType)((ConstantExpression)expression.Right).Value;
                else if (expression.Right.NodeType == ExpressionType.MemberAccess)
                    _Criteria.SearchType = (SearchType)GetMemberValue((MemberExpression)expression.Right);
                else
                    throw new NotSupportedException("Expression type not supported for SearchType: " + expression.Right.NodeType);
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
