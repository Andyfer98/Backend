using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IVotoRepository
    {
        Task<Response<VotoModel>> GetByIdEstudiante(int id);

        Task<Response<List<VotoModel>>> GetByIdCandidata(int id);

        Task<Response<string>> Add(VotoModel voto);

        Task<Response<string>> Delete(int id);
    }
}
