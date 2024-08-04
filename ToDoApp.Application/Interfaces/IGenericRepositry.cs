using System.Linq.Expressions;

namespace ToDoApp.Application.Interfaces;

public interface IGenericRepositry<T> where T : class
{
    List<T> GetAll(Expression<Func<T, bool>>[] where = null, params Expression<Func<T, object>>[] children);
    T GetById(int id);
    T Create(T entity);
    bool Update(T entity);
    bool Delete(int id);

}