using API_Service.Models;
using API_Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Service.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class PhrasesController : ControllerBase
    {
        private readonly DbService _dbService;

        public PhrasesController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IEnumerable<Phrase>> GetPhrase()
        {
            var phrases = await _dbService.GetAllPrhasesAsync();

            return phrases;
        }

        [HttpGet("{id}")]
        public async Task<Phrase> GetPhrase(int id)
        {
            var phrase = await _dbService.GetPhraseAsync(id);

            return phrase;
        }

        [HttpPost]
        public async Task PostPhrase(Phrase phrase)
        {
            await _dbService.InsertPhraseAsync(phrase);
        }

        [HttpDelete("{id}")]
        public async Task DeletePhrase(int id)
        {
            await _dbService.DeleteAllVariantsAsync(id);

            await _dbService.DeletePhraseAsync(id);
        }

        [HttpPut]
        public async Task UpdatePhrase(Phrase phrase)
        {
            await _dbService.UpdatePhraseAsync(phrase);
        }
    }
}
