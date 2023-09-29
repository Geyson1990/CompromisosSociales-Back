using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contable.Application.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> LikeAllLeft<T>(this IQueryable<T> query, string[] words, string field)
        {
            if (words.Count() == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T));

            var body = words.Select(word => 
            Expression.Call(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
            Expression.Constant(EF.Functions), Expression.Property(parameter, typeof(T).GetProperty(field)),
            Expression.Constant($@"%{word}")))
            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.And(current, call) : (Expression)call);

            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        public static IQueryable<T> LikeAllRigth<T>(this IQueryable<T> query, string[] words, string field)
        {
            if (words.Count() == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T));

            var body = words.Select(word =>
            Expression.Call(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
            Expression.Constant(EF.Functions), Expression.Property(parameter, typeof(T).GetProperty(field)),
            Expression.Constant($@"{word}%")))
            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.And(current, call) : (Expression)call);

            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        public static IQueryable<T> LikeAllBidirectional<T>(this IQueryable<T> queryable, params Expression<Func<T, bool>>[] predicates)
        {
            if (predicates == null || predicates.Count() == 0)
                return queryable;

            var parameter = Expression.Parameter(typeof(T));

            return queryable.Where(
                Expression.Lambda<Func<T, bool>>(
                    predicates.Aggregate<Expression<Func<T, bool>>, Expression>(null, (current, predicate) =>
                    {
                        var visitor = new ParameterSubstitutionVisitor(predicate.Parameters[0], parameter);
                        return current != null ? Expression.OrElse(current, visitor.Visit(predicate.Body)) : visitor.Visit(predicate.Body);
                    }),
               parameter));
        }

        public static IQueryable<T> LikeAllBidirectional<T>(this IQueryable<T> query, string[] words, string field)
        {
            if (words.Count() == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T));

            var body = words.Select(word =>
            Expression.Call(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
            Expression.Constant(EF.Functions), Expression.Property(parameter, typeof(T).GetProperty(field)),
            Expression.Constant($@"%{word}%")))
            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.And(current, call) : (Expression)call);

            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        public static IQueryable<T> LikeAnyRight<T>(this IQueryable<T> query, string[] words, string field)
        {
            if (words.Count() == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T));

            var body = words.Select(word =>
            Expression.Call(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
            Expression.Constant(EF.Functions), Expression.Property(parameter, typeof(T).GetProperty(field)),
            Expression.Constant($@"{word}%")))
            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.Or(current, call) : (Expression)call);

            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        public static IQueryable<T> LikeAnyLeft<T>(this IQueryable<T> query, string[] words, string field)
        {
            if (words.Count() == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T));

            var body = words.Select(word =>
            Expression.Call(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
            Expression.Constant(EF.Functions), Expression.Property(parameter, typeof(T).GetProperty(field)),
            Expression.Constant($@"%{word}")))
            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.Or(current, call) : (Expression)call);

            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }


        public static IQueryable<T> LikeAnyBidirectional<T>(this IQueryable<T> query, string[] words, string field)
        {
            if (words.Count() == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T));
            var l = typeof(T).GetProperties();

            var body = words.Select(word =>
            Expression.Call(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
            Expression.Constant(EF.Functions), Expression.Property(parameter, typeof(T).GetProperty(field)),
            Expression.Constant($@"%{word}%")))
            .Aggregate<MethodCallExpression, Expression>(null, (current, call) => current != null ? Expression.Or(current, call) : (Expression)call);

            return query.Where(Expression.Lambda<Func<T, bool>>(body, parameter));
        }

        private class ParameterSubstitutionVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _destination;
            private readonly ParameterExpression _source;

            public ParameterSubstitutionVisitor(ParameterExpression source, ParameterExpression destination)
            {
                _source = source;
                _destination = destination;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ReferenceEquals(node, _source) ? _destination : base.VisitParameter(node);
            }
        }
    }
}
