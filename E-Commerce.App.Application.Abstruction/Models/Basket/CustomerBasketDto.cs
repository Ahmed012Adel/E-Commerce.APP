using System.ComponentModel.DataAnnotations;

namespace E_Commerce.App.Application.Abstruction.Models.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public required string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}