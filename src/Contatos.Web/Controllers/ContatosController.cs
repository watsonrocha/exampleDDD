using System.Collections.Generic;
using System.Linq;
using Contatos.Domain.Interfaces;
using Contatos.Domain.Models;
using Contatos.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Web.Controllers
{
     [Route("api/[controller]")]
    public class ContatosController : Controller
    {
        private readonly ContatoService _contatoService;
        private readonly IRepository<Contato> _contatoRepository;

        public ContatosController(ContatoService contatoService,
            IRepository<Contato> contatoRepository)
        {
            _contatoService = contatoService;
            _contatoRepository = contatoRepository;
        }

         [HttpGet]
         public IEnumerable<ContatoDTO> GetContatos()
         {
             var contatos = _contatoRepository.GetAll();
            
            var resultado = contatos.Select(c => new ContatoDTO{ Id = c.Id, Nome = c.Nome, Email= c.Email });

            return resultado;
         }


         [HttpGet("{id}")]
         public  ActionResult<Contato> GetContato(int id)
         {
             var contato =  _contatoRepository.GetById(id);
             if (contato == null)
             {
                 return NotFound(new { message = $"Contato de id={id} não encontrado" });
             }
             return contato;
         }
    }
}