using System;
using Application.Domain.Dao;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;

namespace Application.Infrastructure.Repositories
{
    public class DoctorRepository : IDataRepository<Doctor>
    {

        readonly RepositoryContext _repositoryContext;

        public DoctorRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public void Add(Doctor entity)
        {
            _repositoryContext.Doctor.Add(entity);
            _repositoryContext.SaveChanges();
        }

        public void Delete(Doctor entity)
        {
            _repositoryContext.Doctor.Remove(entity);
            _repositoryContext.SaveChanges();
        }

        public Doctor Get(int id)
        {
            return _repositoryContext.Doctor
                  .FirstOrDefault(e => e.DoctorId == id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _repositoryContext.Doctor.ToList();
        }

        public void Update(Doctor dbEntity, Doctor entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Specialty = entity.Specialty;

            _repositoryContext.SaveChanges();
        }
    }
}


