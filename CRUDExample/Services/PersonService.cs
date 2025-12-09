using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services
{
    public class PersonService : IPersonsService
    {
        //private field
        private readonly List<Person> _person;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonService()
        {
            _person = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            // check if PersonAddRequest is not null
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }

            // validate PersonName
            //if (string.IsNullOrWhiteSpace(personAddRequest.PersonName))
            //{
            //    throw new ArgumentException("PersonName can't be blank");
            //}

            //model validation
            ValidationHelper.ModelValidation(personAddRequest);

            // convert personAddRequest into Person type
            Person person = personAddRequest.ToPerson();

            //generate PersonID
            person.PersonId = Guid.NewGuid();

            // add person object to persons lsit
            _person.Add(person);

            // convert the perosn object into PersonResponse type
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public PersonResponse? GetPersonByPersonID(Guid? PersonID)
        {
            throw new NotImplementedException();
        }
    }
}
