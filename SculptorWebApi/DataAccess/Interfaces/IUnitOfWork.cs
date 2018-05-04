using SculptorWebApi.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace SculptorWebApi.DataAccess.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        I Repository<T, I>() where T : class;
        List<BaseEntity> SaveChanges();
        void Complete();
        void Rollback();
    }
}