using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPatrocinadorRepository
    {

        Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest);

        Task<Response<string>> Add(PatrocinadorModel patrocinador);

        Task<Response<string>> Update(PatrocinadorModel patrocinador);

        Task<Response<string>> Delete(int id);

        Task<Response<PatrocinadorModel>> GetById(int id);

        Task<Response<List<PatrocinadorModel>>> GetAll();

    }
}
