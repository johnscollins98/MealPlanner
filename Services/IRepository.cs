using System.Linq.Expressions;

namespace MealPlanner;
public interface IRepository<T> where T : class
{
  T? Get(int id);
  IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
  IEnumerable<T> All();
  T Add(T entity);
  T? Delete(int id);
  T? Update(T entity);
  int Commit();
}
