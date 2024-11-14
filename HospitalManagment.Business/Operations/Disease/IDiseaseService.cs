using HospitalManagment.Business.Operations.Disease.Dto;
using HospitalManagment.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagment.Business.Operations.Disease
{
    public interface IDiseaseService
    {
        Task<ServiceMessage> AddDisease(AddDiseaseDto disease);
    }
}
