using Shopmaster.Domain.Entites;

namespace Shopmaster.Api.Common.Dtos;

public class AdvertsEditDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }
    public Price Price { get; set; }
    public int CategoryId { get; set; }
}