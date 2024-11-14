using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Data.Entities
{
    public class PatientDiseaseEntity : BaseEntity
    {
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }


        public PatientEntity Patient { get; set; }
        public DiseaseEntity Disease { get; set; }
    }

    public class PatientDiseaseConfiguration : BaseConfiguration<PatientDiseaseEntity>
    {
        public override void Configure(EntityTypeBuilder<PatientDiseaseEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("PatientId", "DiseaseId");
            base.Configure(builder);
        }
    }
}
