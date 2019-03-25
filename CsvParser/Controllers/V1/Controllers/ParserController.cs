using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvParser.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CsvParser.Controllers.V1.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IParserProvider _parserProvider;

        public ParserController(IParserProvider parserProvider)
        {
            _parserProvider = parserProvider;
        }

        // GET api/v1/Parser/Convert
        [HttpGet]
        public IActionResult Get(string data)
        {
            try
            {
                return Ok(_parserProvider.ParseCsv(data));
            }
            catch(ArgumentException)
            {
                return BadRequest("A bad parameter was passed in. Verify that the data being passed in is url encoded correctly.");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]string data)
        {
            try
            {
                return Ok(_parserProvider.ParseCsv(data));
            }
            catch (ArgumentException)
            {
                return BadRequest("A bad parameter was passed in.");
            }
        }
    }
}
