using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly BackendContext _backendcontext;

        public AdminController(BackendContext backendcontext)
        {
            _backendcontext = backendcontext;
        }

        //JOIN Controller
        [HttpGet("Produse+Stoc")]
        public async Task<IActionResult> GetProduseStocJoin()
        {
            var produseStocJoin = await _backendcontext.Produs
        .Join(_backendcontext.StocProduse,
              produs => produs.Id,
              stocProdus => stocProdus.ProdusId,
              (produs, stocProdus) => new { produs, stocProdus })
        .Select(x => new
        {
            IdProdus = x.produs.Id,
            NumeProdus = x.produs.Nume,
            PretProdus = x.produs.Pret,
            DescriereProdus = x.produs.Descriere,
            ImagineProdus = x.produs.Imagine,
            IdStoc = x.stocProdus.Stoc.Id,
            Stoc = x.stocProdus.Stoc.Nr_Produse
        }).ToListAsync();

            return Ok(produseStocJoin);
        }

        [HttpPost("Adauga_Produs")]
        public async Task<IActionResult> Create([FromQuery] Produse produs, [FromQuery] Stoc stoc)
        {
            var newProdus = new Produse
            {
                Id = Guid.NewGuid(),
                Nume = produs.Nume,
                Pret = produs.Pret,
                Imagine = produs.Imagine,
                Descriere = produs.Descriere,
                DateCreated = DateTime.Now,
                StocProduse = new List<StocProdus>()
            };

            var newStoc = new Stoc
            {
                Id = Guid.NewGuid(),
                Nr_Produse = stoc.Nr_Produse,
                DateCreated = DateTime.Now,
                StocProduse = new List<StocProdus>()
            };

            var newStocProdus = new StocProdus
            {
                StocId = newStoc.Id,
                Stoc = newStoc,
                ProdusId = newProdus.Id,
                Produs = newProdus
            };

            newProdus.StocProduse.Add(newStocProdus);
            newStoc.StocProduse.Add(newStocProdus);

            await _backendcontext.AddAsync(newProdus);
            await _backendcontext.AddAsync(newStoc);
            await _backendcontext.AddAsync(newStocProdus);
            await _backendcontext.SaveChangesAsync();

            return Ok(newProdus);
        }

        //INCLUDE Controller
        [HttpPut("Update_Produs")]
        public async Task<IActionResult> Update([FromBody] StocProdus stocProdus)
        {
            // Găsim înregistrarea StocProdus existentă
            var stocProdusToUpdate = await _backendcontext.StocProduse
                .Include(sp => sp.Produs)
                .Include(sp => sp.Stoc)
                .FirstOrDefaultAsync(sp => sp.ProdusId == stocProdus.ProdusId && sp.StocId == stocProdus.StocId);

            if (stocProdusToUpdate != null)
            {
                // Actualizam proprietățile Produs
                stocProdusToUpdate.Produs.Nume = stocProdus.Produs.Nume;
                stocProdusToUpdate.Produs.Pret = stocProdus.Produs.Pret;
                stocProdusToUpdate.Produs.Descriere = stocProdus.Produs.Descriere;
                stocProdusToUpdate.Produs.Imagine = stocProdus.Produs.Imagine;
                stocProdusToUpdate.Produs.DateModified = DateTime.Now;

                // Actualizam proprietățile Stoc
                stocProdusToUpdate.Stoc.Nr_Produse = stocProdus.Stoc.Nr_Produse;
                stocProdusToUpdate.Stoc.DateModified = DateTime.Now;

                // Salvam schimbările în baza de date
                _backendcontext.StocProduse.Update(stocProdusToUpdate);
                await _backendcontext.SaveChangesAsync();

                return Ok(new { message = "Actualizarea a fost realizată cu succes." });
            }

            return NotFound(new { message = "Stocul sau Produsul nu a fost găsit." });
        }
    }
}
