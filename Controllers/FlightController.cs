using Flyline.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Flyline.Dtos;

namespace Flyline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        

        private readonly ILogger<FlightController> _logger;

        static Random random = new Random();


        static private FlightRm[] flights = new FlightRm[]
        {
                new (   Guid.NewGuid(),
                "Overland Airways",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Los Angeles",DateTime.Now.AddHours(random.Next(1, 3))),
                new TimePlaceRm("Istanbul",DateTime.Now.AddHours(random.Next(4, 10))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Arik Airways",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Munchen",DateTime.Now.AddHours(random.Next(1, 10))),
                new TimePlaceRm("Schiphol",DateTime.Now.AddHours(random.Next(4, 15))),
                random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "GreeA frica",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Vizzola-Ticino",DateTime.Now.AddHours(random.Next(4, 18))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Air Peace",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Amsterdam",DateTime.Now.AddHours(random.Next(1, 21))),
                new TimePlaceRm("Glasgow, Scotland",DateTime.Now.AddHours(random.Next(4, 21))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Dana Air",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Zurich",DateTime.Now.AddHours(random.Next(1, 23))),
                new TimePlaceRm("Baku",DateTime.Now.AddHours(random.Next(4, 25))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Azman",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Ljubljana",DateTime.Now.AddHours(random.Next(1, 15))),
                new TimePlaceRm("Warsaw",DateTime.Now.AddHours(random.Next(4, 19))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "ValueJet",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Praha Ruzyne",DateTime.Now.AddHours(random.Next(1, 55))),
                new TimePlaceRm("Paris",DateTime.Now.AddHours(random.Next(4, 58))),
                    random.Next(1, 853)),
        new (   Guid.NewGuid(),
                "Aero",
                random.Next(90, 5000).ToString(),
                new TimePlaceRm("Le Bourget",DateTime.Now.AddHours(random.Next(1, 58))),
                new TimePlaceRm("Zagreb",DateTime.Now.AddHours(random.Next(4, 60))),
                    random.Next(1, 853))
            };

        static private IList<BookDto> Bookings = new List<BookDto>();
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
         => flights;


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

            return Ok(flight);
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

            var flightFound = flights.Any(f => f.Id == dto.FlightId);

            if (flightFound == false)
                return NotFound();

            Bookings.Add(dto);
            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }
    }
}
