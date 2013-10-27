namespace Mocks.Lamda.Test
{
    using System;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
    {
        string Find(Expression<Func<TEntity, bool>> predicate);

        Entity FindEntity(Expression<Func<TEntity, bool>> predicate);
    }
}