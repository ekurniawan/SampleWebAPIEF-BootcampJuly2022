using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private List<Student> students;

        public SamuraisController()
        {
            students = new List<Student>()
            {
                new Student{Nim="88997788",Nama="Erick Kurniawan"},
                new Student{Nim="99776655",Nama="Kenshin Himura"},
                new Student{Nim="66557788",Nama="Tanjiro Kamado"}
            };
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return students;
        }

        [HttpGet("{nim}")]
        public Student Get(string nim)
        {
            var result = students.FirstOrDefault(s => s.Nim == nim);
            if (result == null) throw new Exception("data tidak ditemukan");
            return result;
        }

    }
}
