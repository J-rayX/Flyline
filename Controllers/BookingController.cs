using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flyline.Data;
using Flyline.ReadModels;
using Flyline.Dtos;
using Flyline.Domain.Errors;


namespace Flyline.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities)
        {
            _entities = entities;
        }


        [HttpGet("{email}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(IEnumerable<BookingRm>), 200)]
        public ActionResult<IEnumerable<BookingRm>> List(string email)
        {
            // Go to the entities, inside the Flights dbase, transform the flight entries to an array
            var bookings = _entities.Flights.ToArray()
                .SelectMany(f => f.Bookings
                    .Where(b => b.PassengerEmail == email)
                    .Select(b => new BookingRm(
                        f.Id,
                        f.Airline,
                        f.Price.ToString(),
                        new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                        new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                        b.NumberOfSeats,
                        email
                        )));

            return Ok(bookings);

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Cancel(BookDto dto)
        {
            var flight = _entities.Flights.Find(dto.FlightId);

            var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if(error == null)
            {
                _entities.SaveChanges();
                return NoContent();
            }

            if (error is NotFoundError)
                return NotFound();

            throw new Exception($"The error of type: {error.GetType().Name} occurrred while cancelling the booking made by {dto.PassengerEmail}");
        }
    }
}
