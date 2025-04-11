using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETErudio.Model;
using RestWithASPNETErudio.Business;
using System.Reflection.Metadata.Ecma335;

namespace RestWithASPNETErudio.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private IBookBusiness _bookBusiness;

        public BooksController(ILogger<BooksController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }
<<<<<<< HEAD

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

=======
>>>>>>> bd4ec5005d7389e9ea9fd7b9f332580a24f1757f
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }
<<<<<<< HEAD

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
=======
>>>>>>> bd4ec5005d7389e9ea9fd7b9f332580a24f1757f
    }
}

