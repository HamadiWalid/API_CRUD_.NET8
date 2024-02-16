using API_CRUD.Data;
using API_CRUD.Models;
using API_CRUD.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace API_CRUD.Controllers
{
    [Route("api/ClientApi")]    
    [ApiController] //tell that is an API
    public class ClientApiController : ControllerBase
    {
        //Get all client
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //for documentation 
        public ActionResult <IEnumerable<ClientDto>> GetClients()
        {
            return Ok (ClientStore.clientList);
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
                return BadRequest();

            var client = ClientStore.clientList.FirstOrDefault(c => c.Id == id);

            if(client == null)
                return NotFound();  

            return Ok(client);
        }

        //Create a client
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClientDto> CreateClient([FromBody]ClientDto client)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest();
            //} custum validation in case we dont use this attribute [ApiController] who gives us the possibility to use data annotation in our models
           
            if (ClientStore.clientList.FirstOrDefault(c=>c.Name.ToLower() == client.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustumError", "The client already exists");
                return BadRequest(ModelState);
           
            }

            if (client == null)
                return BadRequest();

            if (client.Id >0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            client.Id = ClientStore.clientList.OrderByDescending(c => c.Id).FirstOrDefault().Id+1 ;  
        
            ClientStore.clientList.Add(client); 
            return CreatedAtRoute("GetClient",new { id = client.Id } ,client);
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

            var client = ClientStore.clientList.FirstOrDefault(c => c.Id == id);

            if (client == null)
                return NotFound();

            ClientStore.clientList.Remove(client);  
            return NoContent();
        }


    }
}
