using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IComentarioRepository
    {
        Task<Response<List<ComentarioModel>>> GetByIdEstudiante(int id);

        Task<Response<List<ComentarioModel>>> GetByIdCandidata(int id);

        Task<Response<string>> Add(ComentarioModel comentario);

        Task<Response<string>> Delete(int id);
    }
}
