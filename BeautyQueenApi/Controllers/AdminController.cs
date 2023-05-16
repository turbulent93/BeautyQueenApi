using BeautyQueenApi.Dtos;
using BeautyQueenApi.Interfaces;
using BeautyQueenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BeautyQueenApi.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> Get(string? registered)
        {
            try
            {
                IEnumerable<User> users = await _adminService.Get(registered);
                return Ok(users.ToList());
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Authorize]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto adminDto) {
            try
            {
                await _adminService.Register(adminDto);
                return Ok("Success");
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login(RegisterDto adminDto)
        {
            try
            {
                return Ok(await _adminService.Login(adminDto));
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthDto>> RefreshToken(AuthDto loginDto)
        {
            try
            {
                return Ok(await _adminService.RefreshToken(loginDto));
            } catch(Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
