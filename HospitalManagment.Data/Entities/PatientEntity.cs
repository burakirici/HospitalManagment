using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Data.Entities
{
    public class PatientEntity : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string HealthStatus { get; set; }
        public ICollection<AppointmentEntity> Appointments { get; set; }
        public ICollection<PatientDiseaseEntity> PatientDiseases { get; set; }
    }
    public class PatientConfiguration : BaseConfiguration<PatientEntity>
    {
        public override void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SurName).IsRequired();
            builder.Property(x => x.HealthStatus).IsRequired();
            base.Configure(builder);
        }
    }
}
