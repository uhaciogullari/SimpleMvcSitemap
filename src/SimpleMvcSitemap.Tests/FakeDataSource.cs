using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMvcSitemap.Tests
{
    public class FakeDataSource : IQueryable<SampleData>, IQueryProvider
    {
        private readonly IEnumerable<SampleData> _items;
        private int? _count;
        private bool _canEnumerateResult;

        public FakeDataSource(IEnumerable<SampleData> items)
        {
            _items = items;
            ElementType = typeof(SitemapNode);
            Provider = this;
            Expression = Expression.Constant(this);
            _canEnumerateResult = true;
        }

        public FakeDataSource() : this(Enumerable.Empty<SampleData>()) { }

        public IEnumerator<SampleData> GetEnumerator()
        {
            if (_canEnumerateResult)
            {
                //to make sure its enumerated only once
                _canEnumerateResult = false;
                return _items.GetEnumerator();
            }

            throw new NotSupportedException("You should not be enumerating the results...");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get; private set; }
        public Type ElementType { get; private set; }
        public IQueryProvider Provider { get; private set; }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression is MethodCallExpression)
            {
                MethodCallExpression methodCallExpression = expression as MethodCallExpression;

                string[] supportedMethodNames = {"Skip", "Take"};
                string methodName = methodCallExpression.Method.Name;
                if ( supportedMethodNames.Contains(methodName))
                {
                    Expression argument = methodCallExpression.Arguments.ElementAt(1);
                    if (argument is ConstantExpression)
                    {
                        ConstantExpression constantExpression = argument as ConstantExpression;
                        if (methodName == "Skip")
                        {
                            SkippedItemCount = (int) constantExpression.Value;
                        }
                        if (methodName == "Take")
                        {
                            TakenItemCount = (int)constantExpression.Value;
                        }
                        return (IQueryable<TElement>)this;
                    }
                }
            }


            throw new NotImplementedException();
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            if (expression is MethodCallExpression)
            {
                MethodCallExpression methodCallExpression = expression as MethodCallExpression;
                if (_count.HasValue && methodCallExpression.Method.Name == "Count")
                {
                    return ChangeType<TResult>(_count.Value);
                }
            }

            throw new NotImplementedException("Expression is not supported");
        }

        public FakeDataSource WithCount(int count)
        {
            _count = count;
            return this;
        }

        public FakeDataSource WithEnumerationDisabled()
        {
            _canEnumerateResult = false;
            return this;
        }

        public int? SkippedItemCount { get; private set; }

        public int? TakenItemCount { get; private set; }

        public static T ChangeType<T>(object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}