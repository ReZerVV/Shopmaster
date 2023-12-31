using System.Runtime.CompilerServices;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.Create;

public class AdvertsCreateHandler : IRequestHandler<AdvertsCreateRequest, AdvertsCreateResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAdvertRepository _advertRepository;

    public AdvertsCreateHandler(IHttpContextAccessor httpContextAccessor, IAdvertRepository advertRepository, ICategoryRepository categoryRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _advertRepository = advertRepository;
        _categoryRepository = categoryRepository;
    }

    public Task<AdvertsCreateResponse> Handle(AdvertsCreateRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_categoryRepository.GetById(request.CategoryId) is not Category category)
        {
            throw new ApplicationException("Category not found");
        }

        Advert advert = new Advert
        {
            Id = Guid.NewGuid(),
            SellerId = userId,
            Title = request.Title,
            Description = request.Description,
            Location = request.Location,
            Price = request.Price,
            Images = request.Images,
            CategoryId = request.CategoryId
        };

        _advertRepository.Add(advert);

        return Task.FromResult(
            new AdvertsCreateResponse(
                Id: advert.Id.ToString()
            )
        );
    }
}
