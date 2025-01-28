using FasterCrmApp.DataAccess.Abstract.Base;
using FasterCrmApp.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfBaseRepository<TEntity, TContext> :
                 IRepository<TEntity>
                 where TEntity : EntityBase
                 where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entity;

        public EfBaseRepository(TContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _entity.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            /*
            İzlenen Varlığı Kontrol Etme:
            DbContext belleğinde aynı ID'ye sahip bir varlık zaten izleniyorsa (trackedEntity), bu varlık bulunur.

            İzlemeyi Kaldırma (Detached):
            Eğer aynı varlık izleniyorsa, izleme durumu kaldırılır (EntityState.Detached) ve çakışma önlenir.

            Yeni Varlığı Bağlama (Attach):
            Güncellenmek istenen varlık DbSet'e eklenir ve izlenmeye başlanır.

            Durumu Güncelleme (Modified):
            Varlığın durumu EntityState.Modified olarak işaretlenir, böylece veritabanında güncellenmesi gerektiği belirtilir.
            */

            var trackedEntity = _entity.Local.FirstOrDefault(e => e.ID == entity.ID);

            if (trackedEntity != null)
                _context.Entry(trackedEntity).State = EntityState.Detached;

            _entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _entity.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
