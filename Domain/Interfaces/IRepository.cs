using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Domain.Interfaces
{
    /// <summary>
    /// IReadonly Repository currently will work only on this cause we are not going to persist
    /// this
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IReadOnlyEntityRepository<TEntity,in TId> where TEntity : Entity<TId>
    {
        IList<TEntity> List(SearchCriteria searchCriteria);
        IList<TEntity> ListWithPaging(SearchCriteria searchCriteria, out int noOfrecords);
        TEntity GetById(TId id);
    }
}
