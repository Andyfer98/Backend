using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IArticuloRepository
    {
        Task<Response<ArticuloModel>> GetById(int id);

        Task<Response<string>> Add(ArticuloModel articulo);

        Task<Response<string>> Put(ArticuloModel articulo);

        Task<Response<string>> Delete(int id);

        Task<Response<List<ArticuloModel>>> GetAll();
    }
}
