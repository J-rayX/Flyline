using Flyline.Domain.Entities;
using Flyline.Domains.Entities;
using System;

namespace Flyline.Data
{
    public class Entities
    {
        // create a list of datatype NewPassengerDto to hold the received bookings from frontend
        public IList<Passenger> Passengers = new List<Passenger>();
        public List<Flight> Flights = new List<Flight>();

    }
}
