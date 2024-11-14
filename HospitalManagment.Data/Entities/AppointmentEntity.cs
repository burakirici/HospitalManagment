using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Data.Entities
{
    public class AppointmentEntity : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public int DoctorId { get; set; }
        public DoctorEntity Doctor { get; set; }
        public int PatientId { get; set; }
        public PatientEntity Patient { get; set; }
    }

    public class AppointmentConfiguration : BaseConfiguration<AppointmentEntity>
    {
        public override void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("DoctorId", "PatientId");
            base.Configure(builder);
        }
    }
}
