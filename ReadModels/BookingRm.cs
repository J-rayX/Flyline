namespace Flyline.ReadModels
{
    public record BookingRm(
        Guid FlighyIf,
        string Airline,
        string Price,
        TimePlaceRm Arrival,
        TimePlaceRm Departure,
        int NumberOfBookedSeats,
        string PassengerEmail
        );
}
