using System.Collections.ObjectModel;
using System.Linq.Expressions;
using WTS.Enums;
using WTS.Services.Interfaces;

namespace WTS.Services
{
    public class SortingService<T> : ISortingService<T> where T : class
    {
        public ObservableCollection<T> Sort(ObservableCollection<T> collection, Expression<Func<T, object>> sortExpression, SortingState sortOrder)
        {
            var compiledExpression = sortExpression.Compile();

            var sortedCollection = sortOrder == SortingState.Ascending
                ? collection.OrderBy(compiledExpression)
                : collection.OrderByDescending(compiledExpression);

            return new ObservableCollection<T>(sortedCollection);
        }
    }
}