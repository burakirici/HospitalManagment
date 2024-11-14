using HospitalManagment.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Data.Entities
{
    public class DoctorEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Specialty { get; set; }

        public DepartmentType DepartmentType { get; set; }
         
        public ICollection<AppointmentEntity> Appointments { get; set; }
    }

    public class DoctorConfiguration : BaseConfiguration<DoctorEntity>
    {
        public override void Configure(EntityTypeBuilder<DoctorEntity> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Specialty).IsRequired();
            base.Configure(builder);
        }
    }
}
