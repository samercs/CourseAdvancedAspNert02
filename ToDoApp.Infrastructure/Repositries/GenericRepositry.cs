using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using ToDoApp.Application.Interfaces;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Repositries;

public class GenericRepositry<T> : IGenericRepositry<T> where T : class
{
    private readonly AppDbContext _databaseContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepositry(AppDbContext context)
    {
        _databaseContext = context;
        _dbSet = _databaseContext.Set<T>();
    }

    public T Create(T entity)
    {
        _dbSet.Add(entity);
        _databaseContext.SaveChanges();
        return entity;
    }

    public bool Delete(int id)
    {
        var item = _dbSet.Find(id);
        if (item is null)
        {
            throw new Exception("Item not found");
        }
        _dbSet.Remove(item);
        _databaseContext.SaveChanges();
        return true;
    }

    public List<T> GetAll(Expression<Func<T, bool>>[] where =null, params Expression<Func<T, object>>[] children)
    {
        var query =  _dbSet.AsQueryable();
        if (children != null && children.Any())
        {
            foreach (var child in children)
            {
                query = query.Include(child);
            }
        }

        if (where != null && where.Any())
        {
            foreach (var w in where)
            {
                query = query.Where(w);
            }
        }
        return query.ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public bool Update(T entity)
    {
        _dbSet.Update(entity);
        _databaseContext.SaveChanges();
        return true;
    }
}