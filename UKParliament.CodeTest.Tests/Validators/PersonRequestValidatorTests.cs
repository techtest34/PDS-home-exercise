using FluentValidation.TestHelper;
using UKParliament.CodeTest.Services.RequestResponseModels;
using UKParliament.CodeTest.Services.Validators;
using Xunit;

namespace UKParliament.CodeTest.Tests.Validators
{
    public class PersonRequestValidatorTests
    {
        private PersonRequestValidator _validator;

        public PersonRequestValidatorTests()
        {
            _validator = new PersonRequestValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_FirstNameInvalid_HasErrors(string firstName)
        {
            var request = new PersonRequest { FirstName = firstName };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(person => person.FirstName);
        }

        [Theory]
        [InlineData("John")]
        public void Validate_FirstNameValid_HasNoErrors(string firstName)
        {
            var request = new PersonRequest { FirstName = firstName };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(person => person.FirstName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_LastNameInvalid_HasErrors(string lastName)
        {
            var request = new PersonRequest { LastName = lastName };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(person => person.LastName);
        }

        [Theory]
        [InlineData("John")]
        public void Validate_LastNameValid_HasNoErrors(string lastName)
        {
            var request = new PersonRequest { LastName = lastName };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(person => person.LastName);
        }

        [Theory]
        [InlineData(null)]
        public void Validate_DateOfBirthInvalid_HasErrors(DateOnly dateOfBirth)
        {
            var request = new PersonRequest { DateOfBirth = dateOfBirth };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(person => person.DateOfBirth);
        }

        [Fact]
        public void Validate_DateOfBirthValid_HasNoErrors()
        {
            var request = new PersonRequest { DateOfBirth = new DateOnly(2000, 12, 25) };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(person => person.DateOfBirth);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        public void Validate_DepartmentIdInvalid_HasErrors(int departmentId)
        {
            var request = new PersonRequest { DepartmentId = departmentId };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(person => person.DepartmentId);
        }

        [Fact]
        public void Validate_DepartmentIdValid_HasNoErrors()
        {
            var request = new PersonRequest { DepartmentId = 1 };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveValidationErrorFor(person => person.DepartmentId);
        }
    }
}
