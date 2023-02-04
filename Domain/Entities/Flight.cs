using Flyline.Domain.Errors;
using Flyline.ReadModels;

namespace Flyline.Domains.Entities
{
    public record Flight(
        Guid Id,
        string Airline,
        string Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        )

    {
        public IList<Booking> Bookings = new List<Booking>();

        // make RemainingNumberOfSeats a property to make it immutable
        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

        internal object? MakeBooking(string passengerEmail, byte numberOfSeats)
        {
            var flight = this;

            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }


            flight.Bookings.Add(
                new Booking(
                        passengerEmail,
                        numberOfSeats
            )
            );

            flight.RemainingNumberOfSeats -= numberOfSeats; // -= decrement dto.number of seats from RemainingNumberOfSeats
            return null;
        }

    }

}
