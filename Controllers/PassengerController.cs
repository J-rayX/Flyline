using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flyline.Dtos;
using Flyline.ReadModels;
using Flyline.Domain.Entities;
using Flyline.Data;

namespace Flyline.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;
        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
                )); // Add the booking coming from FE POST to the list of Passengers 
            System.Diagnostics.Debug.WriteLine(_entities.Passengers.Count); // Console log to see new Passenger addition (Debug run mode)
            // return Ok(); // Meant for GET - 200
            // return Created(); // to return 201 for POST


            /* Return CreatedAtAction:
             * returns 201 and redirects/calls the Find() function passing along an ebject
             * where email is the email received from the user 
             * at the point of registration
             * and gives a URL to where the passenger got created.
             * 
             * CreatedAtAction calls the method which returns the desired object that was jsut created
             * and posts the URI which is pointing to the new object 
             * You get the URI in the location field of the Response Header
             * and the email object in the Response body. Combine both to 
             * implement your redirect.
             */
            return CreatedAtAction(nameof(Find), new { email = dto.Email });


            /* placeholder return for function 
             * when you don't have the function body/return yet
             */
            // throw new NotImplementedException(); 
        }



        [HttpGet("{email}")] //specified email as the query/parameter to look for
        public ActionResult<PassengerRm> Find(string email) //using the paramter received from Http Attribute
        {
            /* NOTE: var passenger is of type passengerDto 
               which is different from PassengerRm set above
               in ActionResult
            */
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);
            /*FirstOrDefault in above goes into the Passengers list and finds 1 or none 
               where email is email
             */
            
            if (passenger == null) // passenger not found
                return NotFound();

            /* convert var passenger passengerDto to match passengerRm ReadMode
               to save to database
            */
            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
                );

            return Ok(rm); // returns passenger in response body if found
        }
    }
}
