using Domain.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAnuncioRepository
    {
        //Task<Response<string>> Authentication(AuthenticationRequest authenticationRequest);

        Task<Response<AnuncioModel>> GetById(int id);

        Task<Response<string>> Add(AnuncioModel anuncio);

        Task<Response<string>> Put(AnuncioModel anuncio);

        Task<Response<string>> Delete(int id);

        Task<Response<List<AnuncioModel>>> GetAll();
    }
}
