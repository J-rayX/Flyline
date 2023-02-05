
namespace Flyline.Domain.Entities
{
    public record Booking(
       string PassengerEmail,
       byte NumberOfSeats); // byte: so we could use up to number 255 . Can't do more than 255. But it uses less meomory than integer
}
