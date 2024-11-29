using FluentValidation;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services.Validators
{
    public class PersonRequestValidator : AbstractValidator<PersonRequest>
    {
        public PersonRequestValidator()
        {
            RuleFor(person => person.FirstName).NotEmpty();
            RuleFor(person => person.LastName).NotEmpty();
            RuleFor(person => person.DateOfBirth).NotEmpty();
            RuleFor(person => person.DepartmentId).NotEmpty();
        }
    }
}
