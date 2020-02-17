using Social.NetWork.DAL.EF;
using Social.NetWork.DAL.Entities;
using Social.NetWork.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social.NetWork.DAL.Repositories {
    public class Repository<T> : IRepository<T> where T: class{

        protected readonly ApplicationContext Database;
        public Repository(ApplicationContext context) => Database = context;
        public virtual IQueryable<T> GetAll() => Database.Set<T>().AsQueryable();

        public virtual void Create(T item) => Database.Set<T>().Add(item);
        public virtual void Update(T item) => Database.SetModified(item);
        public virtual void Delete(T item) => Database.Set<T>().Remove(item);
        public void Dispose() => Database.Dispose();

        public virtual async Task<T> GetById<Tid>(Tid id) => await Database.Set<T>().FindAsync(id);
        public virtual async Task<T> GetByFriendId<Tid>(Tid IdFriend) => await Database.Set<T>().FindAsync(IdFriend);
    }
}
