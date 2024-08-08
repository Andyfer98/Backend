using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICandidataRepository
    {

        Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest);

        Task<Response<string>> Add(CandidataModel candidata);

        Task<Response<string>> Update(CandidataModel candidata);

        Task<Response<string>> Delete(int id);

        Task<Response<CandidataModel>> GetById(int id);

        Task<Response<List<CandidataModel>>> GetAll();

    }
}
