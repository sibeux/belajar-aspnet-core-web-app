using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        //private PemFields
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;

        //consturctor
        public PersonsServiceTest()
        {
            _personService = new PersonService();
            _countriesService = new CountriesService();
        }

        #region AddPerson

        //When we supply null value as PersonAddRequest, it shoul throw ArgumentNullException
        [Fact]
        public void AddPerson_NullPerson()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        //When we supply null value as PersonName, it should throw Argumentxception
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { 
                PersonName = null
            };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }

        //When we supply proper person details, it should insert the person into the person list; and it should return an object of PersonResponse, which includes with the newly generated person id
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            // Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { 
                PersonName = "Person Name...", Email = "person@mail.com", Address = "sample address", CountryID = Guid.NewGuid(), Gender = ServiceContracts.Enums.GenderOptions.Male, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true
            };

            //Act
            PersonResponse person_response_from_add = 
            _personService.AddPerson(personAddRequest);
            List<PersonResponse> person_list =
            _personService.GetAllPersons();

            // Assert
            Assert.True(person_response_from_add.PersonID != Guid.Empty);
            Assert.Contains(person_response_from_add, person_list);
        }


        #endregion

        #region GetPersonByPersonID

        // if we supply null as personID, it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            //arange
            Guid? personID = null;

            //act
            PersonResponse? person_response_from_get =
            _personService.GetPersonByPersonID(personID);

            //Assert
            Assert.Null(person_response_from_get);
        }

        //if we supply a valid person id, it should return the valid person details as PersonResponse object
        [Fact]
        public void GetPersonByPersonID_WithPersonID()
        {
            //Arange
            CountryAddRequest country_request = new CountryAddRequest() 
            { 
                CountryName = "Canada"    
            };
            CountryResponse country_response = _countriesService.AddCountry(country_request);

            //act
            PersonAddRequest person_request = new PersonAddRequest()
            {
                PersonName = "person name", Email = "email@mail.com", Address = "address", CountryID = country_response.CountryID, DateOfBirth = DateTime.Parse("2000-01-01"), Gender = ServiceContracts.Enums.GenderOptions.Male, ReceiveNewsLetters = false
            };

            PersonResponse person_response_from_add = _personService.AddPerson(person_request);

            PersonResponse? person_response_from_get = 
            _personService.GetPersonByPersonID(person_response_from_add.PersonID);

            //Assert
            Assert.Equal(person_response_from_add, person_response_from_get);
        }

        #endregion
    }
}
