using Flyline.ReadModels;
using Flyline.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Flyline.Dtos;
using System;
using Flyline.Domain.Errors;

namespace Flyline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        

        private readonly ILogger<FlightController> _logger;

        static Random random = new Random();


        static private Flight[] flights = new Flight[]
        {
        new (   Guid.NewGuid(),
                "Overland",
                random.Next(90, 5000).ToString(),
                new TimePlace("Lagos",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlace("Benin",DateTime.Now.AddHours(random.Next(4, 10))),
                    2),
        new (   Guid.NewGuid(),
                "Arik Airways",
                random.Next(90, 5000).ToString(),
                new TimePlace("Kaduna",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlace("Ibadan",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Green Africa",
                random.Next(90, 5000).ToString(),
                new TimePlace("Ilorin",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Port Harcourt",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Aero",
                random.Next(90, 5000).ToString(),
                new TimePlace("Amsterdam",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlace("Glasgow, Scotland",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Value Jet",
                random.Next(90, 5000).ToString(),
                new TimePlace("Abuja",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlace("London, England",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Azman",
                random.Next(90, 5000).ToString(),
                new TimePlace("Zurich",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlace("Warsaw",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Dana Air",
                random.Next(90, 5000).ToString(),
                new TimePlace("Praha Ruzyne",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlace("Paris",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853))
        };

        // static private IList<Booking> Bookings = new List<Booking>();
        // static: makes it active throughout the runtime. Non-static would only be available for a single request
        // non-static: won't get information about previous bookings made in the runtime

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRmList = flights.Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                ));

            return flightRmList;
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                );

            return Ok(readModel);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]

        public IActionResult Book(BookDto dto)
        //public void Book(BookDto dto)
        {
            System.Diagnostics.Debug.WriteLine($"Booking a new flight {dto.FlightId}");

            var flight = flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
                return NotFound();

            var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if(error is OverbookError)
            {
                return Conflict(new { message = "Not enough seats." });
            }

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }
    }
}
