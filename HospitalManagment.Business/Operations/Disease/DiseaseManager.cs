using HospitalManagment.Business.Operations.Disease.Dto;
using HospitalManagment.Business.Types;
using HospitalManagment.Data.Entities;
using HospitalManagment.Data.Repositories;
using HospitalManagment.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Business.Operations.Disease
{
    public class DiseaseManager : IDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<DiseaseEntity> _repository;
        public DiseaseManager(IUnitOfWork unitOfWork, IRepository<DiseaseEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<ServiceMessage> AddDisease(AddDiseaseDto disease)
        {
            var hasDisease = _repository.GetAll(x => x.DiseaseName.ToLower()== disease.DiseaseName.ToLower()).Any();
            if(hasDisease)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Disease is Already Exist!"
                };
            }

            var diseaseEntity = new DiseaseEntity
            {
                DiseaseName = disease.DiseaseName,
            };
            _repository.Add(diseaseEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("An error occurred");
            }
            return new ServiceMessage
            {
                IsSucceed = true
            };
        }
    }
}
