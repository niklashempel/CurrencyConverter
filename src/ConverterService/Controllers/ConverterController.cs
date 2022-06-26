using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConverterService.Business;
using ConverterService.Models;
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
        public NumberWord ConvertNumberToWord(double number)
        {
            return new NumberWord
            {
                Word = converterLogic.ConvertNumberToWord(number)
            };
        }
    }
}