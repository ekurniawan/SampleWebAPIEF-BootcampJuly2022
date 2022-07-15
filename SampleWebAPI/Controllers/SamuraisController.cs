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
        public async Task<SamuraiReadDTO> Get(int id)
        {
            SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");

            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;
            return samuraiDTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<SamuraiReadDTO>> Hello(string name)
        {
            List<SamuraiReadDTO> samuraiDtos = new List<SamuraiReadDTO>();
            var results = await _samuraiDAL.GetByName(name);
            foreach(var result in results)
            {
                samuraiDtos.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SamuraiCreateDTO samuraiCreateDto)
        {
            try
            {
                var newSamurai = new Samurai
                {
                    Name = samuraiCreateDto.Name
                };

                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDto = new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                };
                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(SamuraiReadDTO samuraiDto)
        {
            try
            {
                var updateSamurai = new Samurai
                {
                    Id = samuraiDto.Id,
                    Name = samuraiDto.Name
                };
                var result = await _samuraiDAL.Update(updateSamurai);
                return Ok(samuraiDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _samuraiDAL.Delete(id);
                return Ok($"Data samurai dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
