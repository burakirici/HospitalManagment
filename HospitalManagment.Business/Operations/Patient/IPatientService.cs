using HospitalManagment.Business.Operations.Patient.Dtos;
using HospitalManagment.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Business.Operations.Patient
{
    public interface IPatientService
    {
        Task<ServiceMessage> AddPatient(AddPatientDto patient);
        Task<PatientDto> GetPatient(int id);

        Task<List<PatientDto>> GetAllPatients();

        Task<ServiceMessage> DiseaseDiagnoses(int id, string newHealthStatus);

        Task<ServiceMessage> DeletePatient(int id);
        Task<ServiceMessage> UpdatePatient(UpdatePatientDto patient);
    }
}
