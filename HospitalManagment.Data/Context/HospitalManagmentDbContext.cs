using HospitalManagment.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Data.Context
{
    public class HospitalManagmentDbContext : DbContext
    {
        public HospitalManagmentDbContext(DbContextOptions<HospitalManagmentDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new DiseaseConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientDiseaseConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());


            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                });



            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DoctorEntity> Doctors => Set<DoctorEntity>();
        public DbSet<PatientEntity> Patients => Set<PatientEntity>();
        public DbSet<AppointmentEntity> Appointments => Set<AppointmentEntity>();
        public DbSet<PatientDiseaseEntity> PatientDiseases => Set<PatientDiseaseEntity>();
        public DbSet<DiseaseEntity> Diseases => Set<DiseaseEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();    
       


    }
}
