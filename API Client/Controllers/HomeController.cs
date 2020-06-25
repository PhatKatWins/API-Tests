using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API_Client.Models;
using System.Net.Http;
using API_Client.Services;
using API_Service.Models;
using System.Threading;

namespace API_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly APIService _apiService;
        public HomeController(ILogger<HomeController> logger, APIService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Phrase> phrases = null;

            try
            {
                phrases = await _apiService.GetAllPhrasesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("API down.");
            }

            return View(phrases);
        }

        public async Task<IActionResult> Details(int id)
        {
            DetailsViewModel vm = new DetailsViewModel();
            try
            {
                vm.Phrase = await _apiService.GetPhraseAsync(id);
                vm.Variants = await _apiService.GetAllVariantsAsync(id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return View(vm);
        }

        public async Task<IActionResult> EditPhrase(int? id, bool isNew)
        {
            ViewBag.isNew = isNew;
            if (id == null)
            {
                Phrase phrase = new Phrase
                {
                    Text = "Write phrase here."
                };

                return View(phrase);
            }
            else
            {
                Phrase phrase = await GetPhraseAsync((int)id);

                return View(phrase);
            }
        }

        public async Task<IActionResult> DeletePhrase(int id)
        {
            try
            {
                await _apiService.DeletePhraseAsync(id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return Content("Error while deleting.");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SaveNewPhrase(string text)
        {
            try
            {
                await _apiService.PostPhraseAsync(new Phrase() { Text = text });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return Content("Error while posting.");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdatePhrase(int id, string text)
        {
            Phrase phrase = await GetPhraseAsync(id);
            if (phrase != null)
            {
                phrase.Text = text;

                try
                {
                    await _apiService.UpdatePhraseAsync(phrase);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return Content("Error while updating.");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditVariant(int? id, int? phraseId, string phraseText, bool isNew)
        {
            ViewBag.PhraseText = phraseText;
            ViewBag.IsNew = isNew;
            //Creating a new Variant to edit
            if (id == null)
            {
                Variant variant = new Variant
                {
                    PhraseId = (int)phraseId,
                    Language = "Language goes here.",
                    Text = "Text goes here."
                };

                return View(variant);
            }
            //Editing an existing variant
            else
            {
                Variant variant = null;
                try
                {
                    variant = await _apiService.GetVariantAsync((int)id);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }


                return View(variant);
            }
        }

        public async Task<IActionResult> SaveNewVariant(int phraseId, string language, string text)
        {

            if (await GetPhraseAsync(phraseId) != null)
            {
                Variant variant = new Variant
                {
                    PhraseId = (int)phraseId,
                    Language = language,
                    Text = text
                };

                try
                {
                    await _apiService.PostVariantAsync(variant);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    //Handle with custom error view later
                    return Content("Error while posting");
                }
            }
            else
            {
                //Place for a custom message to the user: "couldn't find phrase" or some such.
            }

            return RedirectToAction("Details", "Home", new { id = phraseId });
        }

        public async Task<IActionResult> UpdateVariant(int id, int phraseId, string language, string text)
        {
            if (await GetPhraseAsync(phraseId) != null)
            {
                Variant variant = null;
                try
                {
                    variant = await _apiService.GetVariantAsync((int)id);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    //Custom message to the user: "Variant doesnt exist".
                }

                if (variant != null)
                {
                    variant.Language = language;
                    variant.Text = text;

                    try
                    {
                        await _apiService.UpdateVariantAsync(variant);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        return Content("Error while posting");
                    }
                }
            }

            return RedirectToAction("Details", "Home", new { id = phraseId });
        }

        public async Task<IActionResult> DeleteVariant(int id, int phraseId)
        {

            try
            {
                await _apiService.DeleteVariantAsync(id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return Content("Error while deleting");
            }

            return RedirectToAction("Details", "Home", new { id = phraseId });
        }

        private async Task<Phrase> GetPhraseAsync(int id)
        {
            Phrase phrase = null;
            try
            {
                phrase = await _apiService.GetPhraseAsync(id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            return (phrase);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
