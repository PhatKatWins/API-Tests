using API_Service.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API_Client.Services
{
    public class APIService
    {
        private readonly HttpClient _client = new HttpClient();

        public APIService()
        {
            _client.BaseAddress = new Uri("https://localhost:44305/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Phrase>> GetAllPhrasesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/phrases");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Phrase>>();
            }

            return null;
        }

        public async Task<Phrase> GetPhraseAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/phrases/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Phrase>();
            }

            return null;
        }

        public async Task<bool> PostPhraseAsync(Phrase phrase)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<Phrase>("api/phrases", phrase);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePhraseAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/phrases/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePhraseAsync(Phrase phrase)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync<Phrase>($"api/phrases", phrase);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Variant>> GetAllVariantsAsync(int phraseID)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/variants/all/{phraseID}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Variant>>();
            }

            return null;
        }

        public async Task<Variant> GetVariantAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/variants/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Variant>();
            }

            return null;
        }

        public async Task<bool> PostVariantAsync(Variant variant)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<Variant>("api/variants", variant);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateVariantAsync(Variant variant)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync<Variant>("api/variants", variant);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteVariantAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/variants/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }


    }
}
