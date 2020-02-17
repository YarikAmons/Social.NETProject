using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Interfaces {
    public interface IRepository<T>:IDisposable where T:class{
        Task<T> GetById<Tid>(Tid id);
        Task<T> GetByFriendId<Tid>(Tid IdFriend);
        IQueryable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(T item);

    }
}
