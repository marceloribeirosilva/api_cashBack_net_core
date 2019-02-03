using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashBack.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashBack.Controllers
{
    [Produces("application/json")]
    public class VendaController : Controller
    {
        private IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpGet]
        [Route("api/vendas/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var venda = _vendaService.ObterVendaPorId(id);
                if (venda != null)
                    return new ObjectResult(venda);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

        }

        [HttpGet]
        [Route("api/vendas/{dataInicial}/{dataFinal}")]
        public IActionResult Get(string dataInicial, string dataFinal, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            try
            {
                if (DateTime.TryParse(dataInicial, out DateTime dtInicial) && DateTime.TryParse(dataFinal, out DateTime dtFinal))
                {
                    return new JsonResult(_vendaService.ObterTodasVendas(dtInicial, dtFinal, offset, limit));
                }
                else
                {
                    return new BadRequestObjectResult("Não foi possível determinar as datas iniciais e finais da pesquisa");
                }                
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

        }
    }
}