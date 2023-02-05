﻿using Flyline.Domain.Errors;
using Flyline.ReadModels;

namespace Flyline.Domain.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Booking> Bookings = new List<Booking>();


        public Flight()
        {

        }

        public Flight(
            Guid id,
            string airline,
            string price,
            TimePlace departure,
            TimePlace arrival,
            int remainingNumberOfSeats
        )
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;
        }


        // make RemainingNumberOfSeats a property to make it immutable
        //public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

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
