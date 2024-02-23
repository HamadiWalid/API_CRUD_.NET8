using API_CRUD.Data;
using API_CRUD.Models;
using API_CRUD.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API_CRUD.Controllers
{
    [Route("api/ClientApi")]    
    [ApiController] //tell that is an API
    public class ClientApiController : ControllerBase
    {
        private readonly ILogger<ClientApiController> _logger;
        private readonly ApplicationDbContext _context;
        public ClientApiController(ILogger<ClientApiController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }



        //Get all client
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //for documentation 
        public ActionResult <IEnumerable<ClientDto>> GetClients()
        {
            _logger.LogInformation("Get all client");//teste info log
            return Ok (_context.Clients);
        }


        //Get client by id
        [HttpGet("{id:int}",Name ="GetClient")] // for parametr
        //[HttpGet("id")] // for route endpoint

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult <ClientDto> GetClient(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get client error with Id = " + id);//teste error log
                return BadRequest();
            }

            var client = _context.Clients.FirstOrDefault(c => c.Id == id);

            if(client == null)
                return NotFound();  

            return Ok(client);
        }

        //Create a client
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientDto> CreateClient([FromBody]ClientDto clientDto)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest();
            //} custum validation in case we dont use this attribute [ApiController] who gives us the possibility to use data annotation in our models
           
            if (ClientStore.clientList.FirstOrDefault(c=>c.Name.ToLower() == clientDto.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustumError", "The client already exists");
                return BadRequest(ModelState);
           
            }

            if (clientDto == null)
                return BadRequest();

            if (clientDto.Id >0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            //clientDto.Id = ClientStore.clientList.OrderByDescending(c => c.Id).FirstOrDefault().Id+1 ;  
            //ClientStore.clientList.Add(clientDto); // for hard coded data thats not needed in database because id is an identity column

            Client client = new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                PhoneNumber = clientDto.PhoneNumber,
                Email = clientDto.Email,
                Order = clientDto.Order,
            };

            _context.Clients.Add(client);    
            _context.SaveChanges(); 

            return CreatedAtRoute("GetClient",new { id = clientDto.Id } ,clientDto);
        }

        //Delete a client
        [HttpDelete("{id:int}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteClient(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var client = _context.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
                return NotFound();

            _context.Clients.Remove(client);
            _context.SaveChanges();

            return NoContent();
        }


        //Update a client
        [HttpPut("{id:int}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateClient(int id,[FromBody] ClientDto clientDto)
        {
            if(clientDto == null || id!= clientDto.Id) 
                 return BadRequest();

            //var client = ClientStore.clientList.FirstOrDefault(c => c.Id == id);
            //if(client == null)
            //    return NotFound();

            //client.Name=clientDto.Name;
            //client.Address = clientDto.Address;
            //client.PhoneNumber = clientDto.PhoneNumber;
            //client.Email = clientDto.Email;
            //client.Order = clientDto.Order;//for hard coded data


            Client client = new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                PhoneNumber = clientDto.PhoneNumber,
                Email = clientDto.Email,
                Order = clientDto.Order,
            };

            try
            {
                _context.Update(client);
                _context.SaveChanges();
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }


        //Update partial client
        [HttpPatch("{id:int}", Name = "UpdatePartialClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialClient(int id, JsonPatchDocument<ClientDto> clientDtoPatch)
        {
            if (clientDtoPatch == null || id == 0)
                return BadRequest();

            var client = _context.Clients.AsNoTracking().FirstOrDefault(c => c.Id == id);


            ClientDto clientDto = new ClientDto()
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Order = client.Order,
            };


            if (client == null )
                return NotFound();
            
            clientDtoPatch.ApplyTo(clientDto,ModelState);

            Client model = new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                PhoneNumber = clientDto.PhoneNumber,
                Email = clientDto.Email,
                Order = clientDto.Order,
            };

           
                _context.Update(model);
                _context.SaveChanges();
          
            if (!ModelState.IsValid)
                return BadRequest();
    
            return NoContent();
        }

    }
}
