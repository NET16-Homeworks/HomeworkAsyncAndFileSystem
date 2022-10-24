using HomeworkAsyncAndFileSystem.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HomeworkAsyncAndFileSystem.Models
{
    //тут была попытка в самовалидацию, но чёт не пошло
    public class CustomerModel //: IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int OrderedProductId { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var errors = new List<ValidationResult>();
        //    List<int> productIds = new List<int>();
        //    var _products = ProductAndCustomerService.GetProducts();
        //    foreach (var product in _products)
        //    {
        //        productIds.Add(product.Id);
        //    }

        //    if (string.IsNullOrWhiteSpace(FirstName))
        //    {
        //        errors.Add(new ValidationResult("Введите имя!"));
        //    }

        //    if (string.IsNullOrWhiteSpace(LastName))
        //    {
        //        errors.Add(new ValidationResult("Введите фамилию!"));
        //    }

        //    if (string.IsNullOrWhiteSpace(Email))
        //    {
        //        errors.Add(new ValidationResult("Введите е-меил!"));
        //    }

        //    if (!productIds.Contains(OrderedProductId))
        //    {
        //        errors.Add(new ValidationResult("Такого товара не сузествует!"));
        //    }

        //    return errors;
        //}
    }
}
