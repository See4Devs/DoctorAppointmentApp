using Application.Domain.Dao;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;

namespace Application.Infrastructure.Repositories
{
    public class PatientRepository : IDataRepository<Patient>
    {

        readonly RepositoryContext _repositoryContext;

        public PatientRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public void Add(Patient entity)
        {
            _repositoryContext.Patient.Add(entity);
            _repositoryContext.SaveChanges();
        }

        public void Delete(Patient entity)
        {
            _repositoryContext.Patient.Remove(entity);
            _repositoryContext.SaveChanges();
        }

        public Patient Get(int id)
        {
            var result = _repositoryContext.Patient
                  .FirstOrDefault(e => e.PatientId == id);
            return result;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _repositoryContext.Patient.ToList();
        }

        public void Update(Patient dbEntity, Patient entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Address = entity.Address;
            dbEntity.Email = entity.Email;
            dbEntity.DateOfBirth = entity.DateOfBirth;

            _repositoryContext.SaveChanges();
        }
    }
}