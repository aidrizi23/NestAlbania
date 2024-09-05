using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Data;
using NestAlbania.Models;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    [Authorize]
    [Route("country")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        
        [Route("list")]
        public async Task<IActionResult> Index(int page =1)
        {
            int pageSize = 10;
            var countries = await _countryService.GetPaginatedCountries(page = 1, pageSize = 10);
            return View(countries);
        }
        
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var country = await _countryService.GetCountryById(id);
            if (country == null) return NotFound();
            return View(country);
        }
        
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var country = await _countryService.GetCountryById(id);
            if (country == null) return NotFound();
            else
            {
                await _countryService.DeleteCountry(country);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            CountryForCreationDto dto = new CountryForCreationDto();
            return View(dto);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CountryForCreationDto dto)
        {
            if (dto == null) return BadRequest();
            Country country = new Country();
            country.Name = dto.Name;
            await _countryService.CreateCountry(country);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var country = await _countryService.GetCountryById(id);
            if (country == null) return NotFound();
            return View(country);
        }
        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(Country country)
        {
            if (country == null) return BadRequest();
            await _countryService.EditCountry(country);
            return RedirectToAction("Index");
        }

    }
}
