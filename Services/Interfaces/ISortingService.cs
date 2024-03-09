using System.Collections.ObjectModel;
using System.Linq.Expressions;
using WTS.Enums;

namespace WTS.Services.Interfaces
{
    public interface ISortingService<T>
    {
        ObservableCollection<T> Sort(ObservableCollection<T> collection, Expression<Func<T, object>> sortExpression, SortingState sortOrder);
    }
}