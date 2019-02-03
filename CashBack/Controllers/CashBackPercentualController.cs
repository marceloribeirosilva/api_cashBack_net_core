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
    [Route("api/CashBackPercentual")]
    public class CashBackPercentualController : Controller
    {
        private ICashBackPercentualService _service;

        public CashBackPercentualController(ICashBackPercentualService cashBackPercentualService)
        {
            _service = cashBackPercentualService;
        }

        [HttpGet("{genero}")]
        public IActionResult Get(string genero)
        {
            var valor = _service.ObterCashBack(genero);
            return Ok(valor);
        }
    }
}