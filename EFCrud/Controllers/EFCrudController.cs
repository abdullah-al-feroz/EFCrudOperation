using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFCrudController : ControllerBase
    {
        private static List<Person> person = new List<Person>
        {
           
            new Person{
            Id = 1,
            Name="Sharif",
            FirstName="Md",
            LastName="Parker",
            Place="Dhaka"
            },

            new Person{
            Id = 2,
            Name="Saleh",
            FirstName="Md",
            LastName="Hossain",
            Place="Dhaka"
            }
        };
        private readonly DataContext context;

        public EFCrudController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            //var person = new List<Person>
            //{
            //    new Person{
            //        Id = 1,
            //        Name="Sharif",
            //        FirstName="Md",
            //        LastName="Parker",
            //        Place="Dhaka"
            //    }
            //    };
            return Ok(await context.Persons.ToListAsync());


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var aperson = await context.Persons.FindAsync(id);
            if (aperson == null)
                return BadRequest("aperson not found");
            return Ok(aperson);
        }
        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person aperson)
        {
            context.Persons.Add(aperson);
            await context.SaveChangesAsync();
            return Ok(await context.Persons.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person request)
        {
            var dbaperson = await context.Persons.FindAsync(request.Id);
            if (dbaperson == null)
                return BadRequest("Person not found");

            dbaperson.Name=request.Name;
            dbaperson.FirstName=request.FirstName;
            dbaperson.LastName=request.LastName;  
            dbaperson.Place=request.Place;

            await context.SaveChangesAsync();   

            return Ok(await context.Persons.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Person>>> Delete(int id)
        {
            var dbaperson = await context.Persons.FindAsync(id);
            if (dbaperson == null)
                return BadRequest("Person not found");
            context.Persons.Remove(dbaperson);
            await context.SaveChangesAsync();
            //return Okd(bperson);
            return Ok(await context.Persons.ToListAsync());
        }

    }
}
    
