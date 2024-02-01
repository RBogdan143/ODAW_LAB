using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    IdStoc = x.stocProdus.Stoc.Id,
                    Stoc = x.stocProdus.Stoc.Nr_Produse
                }).ToListAsync();

            return Ok(produseStocJoin);
        }

        //"GROUP BY" Controller
        [HttpGet("Discount")]
        public async Task<IActionResult> GetDiscounts()
        {
            var discounts = await _backendcontext.Discount
                .Select(x => new 
                {
                    Promo = x.Promo_Code,
                    Discount = x.Discount_Percent

                }).GroupBy(y => y.Discount)
                .ToListAsync();

            return Ok(discounts);

        }

        [HttpPost("Adauga_Produs")]
        public async Task<IActionResult> Create([FromBody] Produse produs, [FromQuery] Stoc stoc)
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

            return Ok(new { message = "Produsul a fost adăugat cu succes." });
        }

        //WHERE Controller
        [HttpPost("Adauga_Stoc_Partener")]
        public async Task<IActionResult> Create([FromBody] Stoc stoc, [FromQuery] Guid IdProdus)
        {
            var newStoc = new Stoc
            {
                Id = Guid.NewGuid(),
                Nr_Produse = stoc.Nr_Produse,
                DateCreated = DateTime.Now,
                StocProduse = new List<StocProdus>()
            };

            var produs = await _backendcontext.Produs
            .Where(p => p.Id == IdProdus)
            .FirstAsync();

            var newStocProdus = new StocProdus
            {
                StocId = newStoc.Id,
                Stoc = newStoc,
                ProdusId = IdProdus,
                Produs = produs
            };

            produs.StocProduse?.Add(newStocProdus);
            newStoc.StocProduse.Add(newStocProdus);

            await _backendcontext.AddAsync(newStoc);
            await _backendcontext.AddAsync(newStocProdus);
            await _backendcontext.SaveChangesAsync();

            return Ok(new { message = "Stocul Partener a fost adăugat cu succes." });
        }

        [HttpPost("Adauga_Discount")]
        public async Task<IActionResult> Create([FromBody] Discounts discount)
        {
            var validare_cod = await _backendcontext.Discount
                .AnyAsync(x => x.Promo_Code == discount.Promo_Code);
            if (!validare_cod)
            {
                var newDiscount = new Discounts
                {
                    Id = Guid.NewGuid(),
                    Promo_Code = discount.Promo_Code,
                    Discount_Percent = discount.Discount_Percent,
                    DateCreated = DateTime.Now
                };

                await _backendcontext.AddAsync(newDiscount);
                await _backendcontext.SaveChangesAsync();

                return Ok(new { message = "Codul a fost adăugat." });
            }
            else
                return BadRequest(new { message = "Codul introdus exista deja!" });
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

        [HttpPut("Update_Stoc_Partener")]
        public async Task<IActionResult> Update([FromQuery] Guid IdStoc, [FromBody] int stoc)
        {
            // Găsim înregistrarea StocProdus existentă
            var stocProdusToUpdate = await _backendcontext.StocProduse
                .Include(sp => sp.Produs)
                .Include(sp => sp.Stoc)
                .FirstOrDefaultAsync(sp => sp.StocId == IdStoc);
            if (stocProdusToUpdate != null)
            {

                // Actualizam proprietățile Stoc
                stocProdusToUpdate.Stoc.Nr_Produse = stoc;
                stocProdusToUpdate.Stoc.DateModified = DateTime.Now;

                // Salvam schimbările în baza de date
                _backendcontext.StocProduse.Update(stocProdusToUpdate);
                await _backendcontext.SaveChangesAsync();

                return Ok(new { message = "Actualizarea a fost realizată cu succes." });
            }

                return NotFound(new { message = "Stocul nu a fost găsit." });
        }

        [HttpDelete("Delete_Produs")]
        public async Task<IActionResult> DeleteProd([FromQuery] Guid idprodus)
        {
            // Găsim înregistrarea StocProdus existentă
            var stocProdusToDelete = await _backendcontext.StocProduse
                .Include(sp => sp.Produs)
                .Include(sp => sp.Stoc)
                .Where(sp => sp.ProdusId == idprodus)
                .ToListAsync();

            if (stocProdusToDelete.Count > 0)
            {
                var produs = stocProdusToDelete[0].Produs;
                for (int i = 0; i< stocProdusToDelete.Count; i++)
                {
                    // Ştergem Stocul selectat
                    _backendcontext.Stocul.Remove(stocProdusToDelete[i].Stoc);
                    await _backendcontext.SaveChangesAsync();
                }

                // Ştergem Produsu la Final
                _backendcontext.Produs.Remove(produs);
                await _backendcontext.SaveChangesAsync();

                return Ok(new { message = "Ştergerea a fost realizată cu succes." });
            }

            return NotFound(new { message = "Produsul nu a fost găsit." });
        }

        [HttpDelete("Delete_Stoc_Partener")]
        public async Task<IActionResult> Delete([FromQuery] Guid idstoc)
        {
            // Găsim înregistrarea StocProdus existentă
            var stocProdusToDelete = await _backendcontext.StocProduse
                .Include(sp => sp.Stoc)
                .FirstOrDefaultAsync(sp => sp.StocId == idstoc);

            if (stocProdusToDelete != null)
            {
                var Length = await _backendcontext.StocProduse
                .Include(sp => sp.Produs)
                .Include(sp => sp.Stoc)
                .Where(sp => sp.ProdusId == stocProdusToDelete.ProdusId)
                .ToListAsync();

                if (Length.Count > 1)
                {
                    // Ştergem Stocul partener
                    _backendcontext.Stocul.Remove(stocProdusToDelete.Stoc);
                    await _backendcontext.SaveChangesAsync();

                    return Ok(new { message = "Ştergerea a fost realizată cu succes." });
                }
                else return BadRequest(new { message = "Stocul nu poate fi şters." });
            }

            return NotFound(new { message = "Stocul nu a fost găsit." });
        }

        [HttpDelete("Delete_Discount")]
        public async Task<IActionResult> DeleteStoc([FromQuery] string code)
        {
            var discounts = await _backendcontext.Discount
                .Where(x => x.Promo_Code == code)
                .FirstOrDefaultAsync();
            if (discounts != null)
            {
                _backendcontext.Discount.Remove(discounts);
                await _backendcontext.SaveChangesAsync();

                return Ok(new { message = "Ştergerea a fost realizată cu succes." });
            }

            return NotFound(new { message = "Discount-ul nu a fost găsit." });
        }
    }
}
