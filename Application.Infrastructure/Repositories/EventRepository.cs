using Application.Domain.Dao;
using Application.Domain.Interfaces;
using Application.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {

        readonly RepositoryContext _repositoryContext;

        public EventRepository(RepositoryContext context)
        {
            _repositoryContext = context;
        }


        public async Task<string> AddAsync(Event entity)
        {
            await _repositoryContext.Event.AddAsync(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Created";
        }

        public async Task<string> DeleteAsync(Event entity)
        {
            _repositoryContext.Event.Remove(entity);
            await _repositoryContext.SaveChangesAsync();
            return "Successfully Deleted";
        }

        public async Task<Event> GetAsync(int id)
        {
            return await _repositoryContext.Event
                  .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _repositoryContext.Event.ToListAsync();
        }

        public async Task<string> UpdateAsync(Event dbEntity, Event entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Description = entity.Description;
            dbEntity.StartTime = entity.StartTime;
            dbEntity.EndTime = entity.EndTime;
            await _repositoryContext.SaveChangesAsync();

            return "Successfully Updated";
        }
    }
}


