using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LoginDAL
{
    class LoginRepository<T> : IRepository<T> where T : LoginEntity
    {
        private LoginContext _db = null;

        public LoginRepository(LoginContext context = null)
        {
            _db = context != null ? context : new LoginContext();
        }
        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }

        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.id == id).FirstOrDefault();
            _db.Set<T>().Remove(currentEntity);
            return _db.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> match)
        {
            return _db.Set<T>().Where(match).ToList();
        }

        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus operationStatus = UpdateStatus.Failed;

            try
            {
                LoginEntity currentEntity = GetByExpression(ent => ent.id == updatedEntity.id).FirstOrDefault();
                _db.Entry(currentEntity).OriginalValues["timer"] = updatedEntity.timer;
                _db.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);

                if (_db.SaveChanges() == 1)
                    operationStatus = UpdateStatus.Ok;
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                operationStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);
            }
            return operationStatus;
        }
    }

}