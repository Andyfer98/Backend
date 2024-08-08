using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPremioRepository
    {

        Task<Response<string>> Add(PremioModel premio);

        Task<Response<string>> Update(PremioModel premio);

        Task<Response<string>> Delete(int id);

        Task<Response<List<PremioModel>>> GetAllById(int id);

        Task<Response<List<PremioModel>>> GetAll();
    }
}
