using MediatR;

namespace Restuarants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoCommand:IRequest<string>
{
    public string FileName { get; set; }=default!;
    public int Restaurant { get; set; }
    public Stream File { get; set; }=default!;
}