using HospitalManagment.Business.Operations.Patient.Dtos;
using HospitalManagment.Business.Types;
using HospitalManagment.Data.Entities;
using HospitalManagment.Data.Repositories;
using HospitalManagment.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Business.Operations.Patient
{
    public class PatientManager : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PatientEntity> _patientRepository;
        private readonly IRepository<PatientDiseaseEntity> _patientDiseaseRepository;

        public PatientManager(IUnitOfWork unitOfWork, IRepository<PatientEntity> patientRepository, IRepository<PatientDiseaseEntity> patientDiseaseRepository)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = patientRepository;
            _patientDiseaseRepository = patientDiseaseRepository;
        }

        public async Task<ServiceMessage> AddPatient(AddPatientDto patient)
        {
            var hasPatient = _patientRepository.GetAll(x => x.Name.ToLower() == patient.Name.ToLower()).Any();

            if(hasPatient)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The patient already exists in the system"
                };
            }

            await _unitOfWork.BeginTransaction();

            var patientEntity = new PatientEntity
            {
                Name = patient.Name,
                SurName = patient.SurName,
                HealthStatus = patient.HealthStatus

            };

            _patientRepository.Add(patientEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("A problem was encountered during patient registration");
            }

            foreach (var diseaseId in patient.DiseaseIds)
            {
                var patientDisease = new PatientDiseaseEntity
                {
                    PatientId = patientEntity.Id,
                    DiseaseId = diseaseId
                };

                _patientDiseaseRepository.Add(patientDisease);

            }

                try
                {
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransaction();
                }
                catch (Exception)
                {
                    await _unitOfWork.RollBackTransaction();

                    throw new Exception("A problem was encountered during patient registration, the process was restarted");
                }
               
            
            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public async Task<ServiceMessage> DeletePatient(int id)
        {
            var patient = _patientRepository.GetById(id);
            if (patient == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "The patient to be deleted could not be found."
                };
            }

            _patientRepository.Delete(id);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while deleting a Patient.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }

        public async Task<ServiceMessage> DiseaseDiagnoses(int id, string newHealthStatus)
        {
            var patient = _patientRepository.GetById(id);

            if (patient == null)
            {
                return new ServiceMessage
                { 
                IsSucceed = false,
                Message = "No patient found matching this id"
                };
            }
            patient.HealthStatus = newHealthStatus;
            _patientRepository.Update(patient);


            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("An error occurred while changing the patient health status.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public async Task<List<PatientDto>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAll()
                .Select(x => new PatientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.SurName,
                    HealthStatus = x.HealthStatus,
                    Diseases = x.PatientDiseases.Select(f => new PatientDiseaseDto
                    {
                        Id = f.Id,
                        DiseaseName = f.Disease.DiseaseName
                    }).ToList()
                }).ToListAsync();

            return patients;
        }

        public async Task<PatientDto> GetPatient(int id)
        {
            var patient = await _patientRepository.GetAll(x => x.Id == id)
                .Select(x => new PatientDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.SurName,
                    HealthStatus = x.HealthStatus,
                    Diseases = x.PatientDiseases.Select(f => new PatientDiseaseDto
                    {
                        Id = f.Id,
                        DiseaseName = f.Disease.DiseaseName
                    }).ToList()
                }).FirstOrDefaultAsync();

            return patient;
        }

        public async Task<ServiceMessage> UpdatePatient(UpdatePatientDto patient)
        {
            var patientEntity = _patientRepository.GetById(patient.Id);

            if (patientEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Patient Not Found!"
                };
            }

            await _unitOfWork.BeginTransaction();

            patientEntity.Name = patient.Name;
            patientEntity.SurName = patient.SurName;
            patientEntity.HealthStatus = patient.HealthStatus;
            

            _patientRepository.Update(patientEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("An error was encountered while updating patient information.");
            }

            var patientDiseases = _patientDiseaseRepository.GetAll(x => x.PatientId == x.PatientId).ToList();

            foreach (var patientDisease in patientDiseases)
            {
                _patientDiseaseRepository.Delete(patientDisease, false); // -> HARD DELETE 
            }
            foreach (var diseaseId in patient.DiseaseIds)
            {
                var patientDisease = new PatientDiseaseEntity
                {
                    PatientId = patientEntity.Id,
                    DiseaseId = diseaseId,
                };
                _patientDiseaseRepository.Add(patientDisease);
            }
            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("An error occurred while updating patient information and the process was rolled back.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }
    }
}
