using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeautyQueenApi.Data;
using BeautyQueenApi.Models;
using BeautyQueenApi.Dtos;
using BeautyQueenApi.Interfaces;
using BeautyQueenApi.Services;

namespace BeautyQueenApi.Controllers
{
    [Route("api/promo")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceService _serviceService;
        private readonly IImageService _imageService;

        public PromotionsController(ApplicationDbContext context, IServiceService serviceService, IImageService imageService)
        {
            _context = context;
            _serviceService = serviceService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetPromo()
        {
          if (_context.Promo == null)
          {
              return NotFound();
          }
            return await _context.Promo
                .Include(x => x.Services)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Promotion>> GetPromotion(int id)
        {
          if (_context.Promo == null)
          {
              return NotFound();
          }
            var promotion = await _context.Promo.FindAsync(id);

            _context.Entry(promotion).Collection(x => x.Services).Load();

            if (promotion == null)
            {
                return NotFound();
            }

            return promotion;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion(int id, PromoDto promoDto)
        {
            var promotion = new Promotion
            {
                Id = id,
                Title = promoDto.Title,
                Description = promoDto.Description,
                Image = promoDto.Image,
                Discount = promoDto.Discount,
                PeriodFrom = promoDto.PeriodFrom,
                PeriodTo = promoDto.PeriodTo
            };

            _context.Entry(promotion).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            _context.Entry(promotion).Collection(x => x.Services).Load();

            await SetServices(promotion, promoDto.ServiceIds);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Promotion>> PostPromotion(PromoDto promoDto)
        {
            if (_context.Promo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Promo'  is null.");
            }
            var promotion = new Promotion
            {
                Title = promoDto.Title,
                Description = promoDto.Description,
                Image = promoDto.Image,
                Discount = promoDto.Discount,
                PeriodFrom = promoDto.PeriodFrom,
                PeriodTo = promoDto.PeriodTo
            };

            _context.Promo.Add(promotion);
            await _context.SaveChangesAsync();

            _context.Entry(promotion).Collection(x => x.Services).Load();

            await SetServices(promotion, promoDto.ServiceIds);

            return CreatedAtAction("GetPromotion", new { id = promotion.Id }, promotion);
        }

        private async Task SetServices(Promotion promotion, List<int> serviceIds)
        {
            foreach (var serviceId in serviceIds)
            {
                Service? service = await _serviceService.Find(serviceId);

                if (service == null)
                {
                    throw new Exception($"Service with id {serviceId} is not found");
                }

                promotion.Services.Add(service);
            }

            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            if (_context.Promo == null)
            {
                return NotFound();
            }
            var promotion = await _context.Promo.FindAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            _context.Promo.Remove(promotion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("photo")]
        public async Task<IActionResult> UploadPhoto(IFormFile image)
        {
            try
            {
                return Ok(await _imageService.UploadImage("/files/promo", image));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("photo/{name}")]
        public IActionResult RemovePhoto(string name)
        {
            try
            {
                _imageService.DeleteImage("files/promo", name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
