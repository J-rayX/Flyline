using System.ComponentModel.DataAnnotations;


namespace Flyline.Dtos
{
    public record BookDto(
       [Required] Guid FlightId,
       [Required][EmailAddress][StringLength(100, MinimumLength = 3)] string PassengerEmail,
       [Required][Range(1, 254)] byte NumberOfSeats); // byte: so we could use up to number 255 . Can't do more than 255. But it uses less meomory than integer
}
