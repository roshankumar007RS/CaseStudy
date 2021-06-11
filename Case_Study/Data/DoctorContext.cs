using Case_Study.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_Study.Data
{
    public class DoctorContext: DbContext
    {
        public DoctorContext(DbContextOptions<DoctorContext> options) : base(options)
        {

        }
        public DbSet<Doctor> Doctor { get; set; }
    }
}
