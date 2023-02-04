using Flyline.Domain.Entities;
using Flyline.Domains.Entities;
using System;

namespace Flyline.Data
{
    public class Entities
    {
        // create a list of datatype NewPassengerDto to hold the received bookings from frontend
        static public IList<Passenger> Passengers = new List<Passenger>();

        static Random random = new Random();
        static public Flight[] Flights = new Flight[]
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

    }
}
