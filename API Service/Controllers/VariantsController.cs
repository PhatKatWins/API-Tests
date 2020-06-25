using API_Service.Models;
using API_Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantsController : ControllerBase
    {
        private readonly DbService _dbService;

        public VariantsController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("all/{phraseId}")]
        public async Task<IEnumerable<Variant>> GetVariants(int phraseId)
        {
            var variants = await _dbService.GetAllVariantsAsync(phraseId);

            return variants;
        }

        [HttpGet("{id}")]
        public async Task<Variant> GetVariant(int id)
        {
            var variant = await _dbService.GetVariantAsync(id);

            return variant;
        }

        [HttpPost]
        public async Task PostVariant(Variant variant)
        {
            await _dbService.InsertVariantAsync(variant);
        }

        [HttpPut]
        public async Task UpdateVariant(Variant variant)
        {
            await _dbService.UpdateVariantAsync(variant);
        }

        [HttpDelete("{id}")]
        public async Task DeleteVariant(int id)
        {
            await _dbService.DeleteVariantAsync(id);
        }
    }
}
