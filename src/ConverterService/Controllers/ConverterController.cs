using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConverterService.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConverterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private readonly ConverterLogic converterLogic;

        public ConverterController(ConverterLogic converterLogic)
        {
            this.converterLogic = converterLogic;
        }

        [HttpGet]
        [Route("[action]/{number:double}")]
        public string Get(double number)
        {
            return converterLogic.ConvertNumberToWord(number);
        }
    }
}