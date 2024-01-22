using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly BackendContext _backendcontext;

        public ClientController(BackendContext backendcontext)
        {
            _backendcontext = backendcontext;
        }

        [AllowAnonymous]
        [HttpGet("Lista_Produse")]
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
                    Produs = x.produs.Nume,
                    PretProdus = x.produs.Pret,
                    DescriereProdus = x.produs.Descriere,
                    ImagineProdus = x.produs.Imagine,
                    Stoc = x.stocProdus.Stoc.Nr_Produse
                }).ToListAsync();

            return Ok(produseStocJoin);
        }

        [HttpPost("Adauga_Cos")]
        public async Task<IActionResult> Create([FromBody] Cos_CumparaturiDTO cos)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            var produs = await _backendcontext.Produs
                .Where(p => p.Id == cos.IdProdus)
                .FirstAsync();

            var disount = await _backendcontext.Discount
                .Where(o => o.Promo_Code == cos.PromoCode)
                .FirstAsync();

            var Me = await _backendcontext.Users
                .Where(i => i.Id.ToString() == userId)
                .FirstAsync();

            if (disount != null)
            {
                var newCos = new Cos_Cumparaturi
                {
                    Produse_Alese = new List<Produse>(),
                    Discount = disount,
                    UserId = Me.Id,
                    user = Me
                };

                Me.Cos = newCos;

                newCos.Produse_Alese.Add(produs);
                await _backendcontext.AddAsync(newCos);
                await _backendcontext.SaveChangesAsync();

                return Ok(newCos);
            }
            else
                {
                    var newCos = new Cos_Cumparaturi
                    {
                        Produse_Alese = new List<Produse>(),
                        UserId = Me.Id,
                        user = Me
                    };
                    
                    newCos.Produse_Alese.Add(produs);
                    await _backendcontext.AddAsync(newCos);
                    await _backendcontext.SaveChangesAsync();

                    return Ok(newCos);
                }
        }

        [HttpPut("Modifica_Cos")]
        public async Task<IActionResult> Update([FromBody] Guid? IdProdus, [FromQuery] string? PromoCode)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            var cos = await _backendcontext.Cos
                .FirstOrDefaultAsync(c => c.UserId.ToString() == userId);

            if (IdProdus != null)
            {
                var produs = await _backendcontext.Produs
                .FirstAsync(p => p.Id == IdProdus);

                cos.Produse_Alese.Add(produs);

                if (PromoCode != null && cos.Discount == null)
                {
                    var code = await _backendcontext.Discount
                        .FirstAsync(p => p.Promo_Code == PromoCode);

                    cos.Discount = code;

                    _backendcontext.Cos.Update(cos);
                    await _backendcontext.SaveChangesAsync();

                    return Ok();
                }
                else
                    {
                        _backendcontext.Cos.Update(cos);
                        await _backendcontext.SaveChangesAsync();

                        return Ok();
                    }
            }
            else
                if (PromoCode != null)
                {
                    var code = await _backendcontext.Discount
                        .FirstAsync(p => p.Promo_Code == PromoCode);

                    cos.Discount = code;

                    _backendcontext.Cos.Update(cos);
                    await _backendcontext.SaveChangesAsync();

                    return Ok();
                }
                else return BadRequest();
        }

        [HttpDelete("Scoate_Produse")]
        public async Task<IActionResult> Delete([FromBody] Guid IdProdus)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            var cos = await _backendcontext.Cos
                .FirstOrDefaultAsync(c => c.UserId.ToString() == userId);

            var produs = await _backendcontext.Produs
            .FirstAsync(p => p.Id == IdProdus);

            if (cos.Produse_Alese.Contains(produs))
            {
                cos.Produse_Alese.Remove(produs);

                _backendcontext.Cos.Update(cos);
                await _backendcontext.SaveChangesAsync();

                return Ok();
            }
            else return NotFound();
        }

        [HttpDelete("Tranzactie")]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            var Me = await _backendcontext.Users
                .Where(i => i.Id.ToString() == userId)
                .FirstAsync();

            var cos = await _backendcontext.Cos
                .FirstOrDefaultAsync(c => c.UserId.ToString() == userId);

            if (Me.Balanta_Cont >= cos.Total_Plata)
            {
                Me.Balanta_Cont -= cos.Total_Plata;
                Me.Cos = null;
                _backendcontext.Users.Update(Me);
                _backendcontext.Cos.Remove(cos);
                await _backendcontext.SaveChangesAsync();
                return Ok(new { message = "Tranzactie Complata!" });
            }
            else
            {
                return BadRequest(new { message = "Fonduri Insuficiente!" });
            }
        }
    }
}
