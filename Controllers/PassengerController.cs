using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flyline.Dtos;

namespace Flyline.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        // create a list of datatype NewPassengerDto to hold the received bookings from frontend
        static private IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            Passengers.Add(dto); // Add the booking coming from FE POST to the list of Passengers 
            System.Diagnostics.Debug.WriteLine(Passengers.Count); // Console log to see new Passenger addition (Debug run mode)
            return Ok();
            // throw new NotImplementedException(); // placeholder return for function when you don't have the function body/return yet
        }
    }
}
