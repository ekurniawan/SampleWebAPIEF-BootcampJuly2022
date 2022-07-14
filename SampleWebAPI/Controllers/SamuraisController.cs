using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
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
        public async Task<IEnumerable<Samurai>> Get()
        {
            var results = await _samuraiDAL.GetAll();
            return results;
        }

        /*[HttpGet("{nim}")]
        public Student Get(string nim)
        {
            var result = students.FirstOrDefault(s => s.Nim == nim);
            if (result == null) throw new Exception("data tidak ditemukan");
            return result;
        }*/

    }
}
