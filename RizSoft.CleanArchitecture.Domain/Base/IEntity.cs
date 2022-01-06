using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RizSoft.CleanArchitecture.Domain
{
   
        public interface IEntity
        {
            object ObjectId { get; set; }
        }

        public interface IEntity<TKey> : IEntity
        {
            TKey Id { get; set; }
        }
    
}
