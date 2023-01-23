using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SequenceAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SequenceAlgorithm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SequenceAlgorthim : ControllerBase
    {
        private readonly ISequenceProcessor _sequenceProcessor;

        public SequenceAlgorthim(ISequenceProcessor sequenceProcessor)
        {
            _sequenceProcessor = sequenceProcessor;
        }

        [HttpGet]
        public string GetSequenceNumbers(int[] input)
        {
            string result = string.Empty;
            result = _sequenceProcessor.ProcessInput(input);
            return result;
        }


    }
}
