using CnpjWebApi.Models;
using CnpjWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CnpjWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CnpjController : ControllerBase
    {
        private readonly EmpresaService _empresaService;

        public CnpjController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CnpjEmpresa>>> Get() => await _empresaService.Get();


        [HttpGet("{cnpj:length(14)}")]
        public async Task<ActionResult<CnpjEmpresa>> Get(string cnpj)
        {
            var model = await _empresaService.Get(cnpj);

            if(model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

    }
}
