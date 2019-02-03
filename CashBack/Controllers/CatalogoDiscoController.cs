using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashBack.Models;
using CashBack.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashBack.Controllers
{
    [Produces("application/json")]   
    public class CatalogoDiscoController : Controller
    {
        private ICatalogoDiscoService _catalogoDiscoService;

        public CatalogoDiscoController(ICatalogoDiscoService catalogo)
        {
            _catalogoDiscoService = catalogo;
        }

        [HttpGet]
        [Route("api/CatalogoDiscos/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var disco = _catalogoDiscoService.ObterDiscoPorID(id);
                if (disco != null)
                    return new ObjectResult(disco);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

        }

        [HttpGet]
        [Route("api/CatalogoDiscos/genero/{genero}")]
        public IActionResult Get(string genero, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            try
            {
                return new JsonResult(_catalogoDiscoService.ListarTodosPorGenero(genero, offset, limit));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            
        }
    }
}