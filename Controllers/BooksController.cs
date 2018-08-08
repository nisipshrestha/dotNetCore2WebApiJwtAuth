using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : Controller
    {
        // GET: api/Books
        [HttpGet, Authorize]
        public IEnumerable<Book> Get()
        {
            var currentUser = HttpContext.User;
            int userAge = 0;

            var resultBookList = new Book[] {
                new Book { Author = "Ray Bradbury",Title = "Fahrenheit 451" ,AgeRestriction=false},
                new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude" ,AgeRestriction=false},
                new Book { Author = "George Orwell", Title = "1984" ,AgeRestriction=false},
                new Book { Author = "Anais Nin", Title = "Delta of Venus" ,AgeRestriction=false}
            };

            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
                userAge = DateTime.Today.Year - birthDate.Year;
            }

            if (userAge>18)
            {
                resultBookList = resultBookList.Where(b=>!b.AgeRestriction).ToArray();
            }
            return resultBookList;
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Books
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
