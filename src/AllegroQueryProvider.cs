﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AllegroLINQ.src
{
    class AllegroQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            return CreateQuery<AllegroItem>(expression);
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            return new AllegroQueryable<T>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return AllegroQueryContext.Execute(expression);
        }

        public T Execute<T>(Expression expression)
        {
            return (T)AllegroQueryContext.Execute(expression);
        }
    }

}
