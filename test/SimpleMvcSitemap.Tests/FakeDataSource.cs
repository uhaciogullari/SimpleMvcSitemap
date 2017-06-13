using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMvcSitemap.Tests
{
    public class FakeDataSource : IQueryable<SampleData>, IQueryProvider
    {
        private int? count;
        private bool canEnumerateResult;
        public IEnumerable<SampleData> Items { get; private set; }

        public FakeDataSource()
        {
            Items = Enumerable.Empty<SampleData>();
            ElementType = typeof(SitemapNode);
            Provider = this;
            Expression = Expression.Constant(this);
            canEnumerateResult = true;
        }

        

        public IEnumerator<SampleData> GetEnumerator()
        {
            if (canEnumerateResult)
            {
                //to make sure its enumerated only once
                canEnumerateResult = false;
                return Items.GetEnumerator();
            }

            throw new NotSupportedException("You should not be enumerating the results...");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            if (expression is MethodCallExpression)
            {
                MethodCallExpression methodCallExpression = (MethodCallExpression) expression;

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
                MethodCallExpression methodCallExpression = (MethodCallExpression) expression;
                if (count.HasValue && methodCallExpression.Method.Name == "Count")
                {
                    return ChangeType<TResult>(count.Value);
                }
            }

            throw new NotImplementedException("Expression is not supported");
        }

        public FakeDataSource WithCount(int count)
        {
            this.count = count;
            return this;
        }

        public FakeDataSource WithEnumerationDisabled()
        {
            canEnumerateResult = false;
            return this;
        }

        public FakeDataSource WithItemsToBeEnumerated(IEnumerable<SampleData> items)
        {
            Items = items;
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