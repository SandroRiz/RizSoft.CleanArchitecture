using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizSoft.CleanArchitecture.Domain
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        object IEntity.ObjectId
        {
            get => Id;
            set => Id = (TKey)value;
        }

        public virtual TKey Id { get; set; }
    }
}
