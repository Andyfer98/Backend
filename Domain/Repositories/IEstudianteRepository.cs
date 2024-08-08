using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEstudianteRepository
    {
        Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest);

        Task<Response<EstudianteModel>> GetById(int id);

        Task<Response<string>> Add(EstudianteModel estudiante);

        Task<Response<string>> Put(EstudianteModel estudiante);

        Task<Response<string>> Delete(int id);
    }
}
