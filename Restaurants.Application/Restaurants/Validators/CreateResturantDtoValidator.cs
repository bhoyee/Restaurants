using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Validators
{
    public class CreateResturantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {
        //applying some custom rule 
        private readonly List<string> validCategories = ["Italian", "African", "American", "Indian", "Mexican", "Japanese"];
        
        public CreateResturantDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.");

            // the custom rule apply here
            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Invalid category. Please choose from the valid categories");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (xx-xxx).");

        }
    }
}
