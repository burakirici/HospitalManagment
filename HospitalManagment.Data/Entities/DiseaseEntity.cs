using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Data.Entities
{
    public class DiseaseEntity : BaseEntity
    {
        public string DiseaseName { get; set; }
        public ICollection<PatientDiseaseEntity> PatientDiseases { get; set; }
    }

    public class DiseaseConfiguration : BaseConfiguration<DiseaseEntity>
    {
        public override void Configure(EntityTypeBuilder<DiseaseEntity> builder)
        {
            builder.Property(x => x.DiseaseName).IsRequired();
            base.Configure(builder);
        }
    }
}
