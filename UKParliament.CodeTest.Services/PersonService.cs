using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using UKParliament.CodeTest.Data.Repositories;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IPersonMapper _mapper;
    private readonly IValidator<PersonRequest> _validator;

    public PersonService(IPersonRepository personRepository, IPersonMapper mapper, IValidator<PersonRequest> validator)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<PersonResponse>> GetPerson(int id)
    {
        var person = await _personRepository.GetPerson(id);

        if (person == null)
        {
            return Result.NotFound();
        }

        return Result.Success(_mapper.MapToResponse(person));
    }

    public async Task<Result<IEnumerable<PersonResponse>>> GetPeople()
    {
        var people = await _personRepository.GetPeople();

        var response = _mapper.MapToResponse(people);

        return Result.Success(response);
    }

    public async Task<Result<PersonResponse>> CreatePerson(PersonRequest person)
    {
        var validationResult = await _validator.ValidateAsync(person);

        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var createdPerson = await _personRepository.CreatePerson(_mapper.MapToPerson(person));

        return Result.Created(_mapper.MapToResponse(createdPerson));
    }

    public async Task<Result<PersonResponse>> UpdatePerson(int id, PersonRequest request)
    {
        if (id != request.Id) // The PUT spec says that the entire object to update should be sent, hence the validation!
        {
            return Result.Invalid();
        }

        if (await _personRepository.GetPerson(id) == null)
        {
            return Result.NotFound();
        }

        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var updatedPerson = await _personRepository.UpdatePerson(_mapper.MapToPerson(request));

        return Result.Success(_mapper.MapToResponse(updatedPerson));
    }
}