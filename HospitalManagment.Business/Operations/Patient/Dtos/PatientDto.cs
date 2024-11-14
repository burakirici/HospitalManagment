using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Business.Operations.Patient.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string HealthStatus { get; set; }
        public List<PatientDiseaseDto> Diseases { get; set; }
    }
}
