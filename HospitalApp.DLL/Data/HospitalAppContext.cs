using HospitalApp.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.DLL.Data
{
    public class HospitalAppContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public HospitalAppContext(DbContextOptions options) : base(options)
        {
        }
    }
}
