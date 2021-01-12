using System;
using Microsoft.AspNetCore.Mvc;
using TestRoulette.DTO;
using TestRoulette.Models;
using TestRoulette.Services;

namespace TestRoulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouletteController : Controller
    {
        private IRouletteService rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }
     
        [HttpPost]
        public IActionResult NewRulette()
        {
            Roulette roulette = rouletteService.create();
            return Ok(roulette);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(rouletteService.GetAll());
        }
        
       
        [HttpPut("{id}/open")]
        public IActionResult Open([FromRoute(Name = "id")] string id)
        {
            try
            {
                rouletteService.Open(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }
        
        [HttpPut("{id}/close")]
        public IActionResult Close([FromRoute(Name = "id")] string id)
        {
            try
            {
                Roulette roulette = rouletteService.Close(id);
                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
        }

        [HttpPost("{id}/bet")]
        public IActionResult Bet([FromHeader(Name = "user-id")] string UserId, [FromRoute(Name = "id")] string id,
            [FromBody] BetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Bad Request"
                });
            }

            try
            {
                Roulette roulette = rouletteService.Bet(id, UserId, request.position, request.money);
                return Ok(roulette);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(405);
            }
            
        }
    }
}