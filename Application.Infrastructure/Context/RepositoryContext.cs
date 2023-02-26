using Application.Domain.Dao;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Context
{
	public class RepositoryContext : DbContext
	{
		public RepositoryContext(DbContextOptions options) : base(options) { }

		public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
    }
}

