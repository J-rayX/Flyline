﻿namespace Flyline.Domains.Entities
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

    }

}