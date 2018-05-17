using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    /// <summary>
    /// Base class for all entities 
    /// </summary>
    /// <typeparam name="TId">Type of the ID (Ex:string , int )</typeparam>
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
