using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samuraiDAL;
        public SamuraisController(ISamurai samuraiDAL)
        {
            _samuraiDAL = samuraiDAL;
        }

        [HttpGet]
        public async Task<IEnumerable<SamuraiReadDTO>> Get()
        {
            List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();

            var results = await _samuraiDAL.GetAll();
            foreach (var result in results)
            {
                samuraiDTO.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDTO;
        }

        [HttpGet("{id}")]
        public async Task<Samurai> Get(int id)
        {
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Samurai samurai)
        {
            try
            {
                var result = await _samuraiDAL.Insert(samurai);
                return CreatedAtAction("Get", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
