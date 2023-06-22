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

namespace BeautyQueenApi.Controllers
{
    [Route("api/gallery")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public GalleryController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetByService(int? serviceId, string? search)
        {
            if (_context.Photo == null)
            {
                return NotFound();
            }
            var photos = await _context.Photo.ToListAsync();

            if(serviceId != null)
            {
                photos = photos.Where(photo => photo.ServiceId == serviceId).ToList();
            }

            if(search != null)
            {
                return Ok(photos.Where(photo => photo.Title.ToLower().Contains(search.ToLower())));
            }

            return Ok(photos);
        }

        [HttpGet("favorites")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return Ok(await _context.Photo.Take(8).ToListAsync());
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Photo>> GetPhoto(int id)
        //{
        //  if (_context.Photo == null)
        //  {
        //      return NotFound();
        //  }
        //    var photo = await _context.Photo.FindAsync(id);

        //    if (photo == null)
        //    {
        //        return NotFound();
        //    }

        //    return photo;
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(PhotoDto photoDto)
        {
            if (_context.Photo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Photo'  is null.");
            }

            var photo = new Photo
            {
                Title = photoDto.Title,
                ServiceId = photoDto.ServiceId,
                Source = photoDto.Source
            };

            _context.Photo.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPhoto), new { id = photo.Id }, photo);
        }

        [HttpPost("upload")]
        public async Task<ActionResult<List<string>>> UploadImage([FromForm] ImagesDto fileList)
        {
            var fileNames = new List<string>();

            if(fileList.Images == null)
            {
                return BadRequest("Images is null");
            }

            foreach(var file in fileList.Images)
            {
                fileNames.Add(await _imageService.UploadImage("/files/gallery/", file));
            }

            return fileNames;
        }

        [HttpDelete("photo/{name}")]
        public ActionResult DeleteImage(string name)
        {
            try
            {
                _imageService.DeleteImage("/files/gallery/", name);
                return Ok();
            } catch(Exception)
            {
                return BadRequest("File does not exists");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (_context.Photo == null)
            {
                return NotFound();
            }
            var photo = await _context.Photo.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photo.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int id)
        {
            return (_context.Photo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
