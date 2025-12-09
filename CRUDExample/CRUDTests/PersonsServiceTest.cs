using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonsServiceTest
    {
        //private PemFields
        private readonly IPersonsService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;

        //consturctor
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();
            _countriesService = new CountriesService();
            _testOutputHelper = testOutputHelper;
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

        #region GetAllPersons

        //The GetAllPersons() should return an empty list by default
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> persons_from_get = _personService.GetAllPersons();

            //Assert
            Assert.Empty(persons_from_get);
        }

        //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            //arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse country_response_1 = 
            _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 = 
            _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",Email = "smith@mail.com", Gender = ServiceContracts.Enums.GenderOptions.Male, Address = "address", CountryID = country_response_1.CountryID, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "john",Email = "smith@mail.com", Gender = ServiceContracts.Enums.GenderOptions.Male, Address = "address", CountryID = country_response_1.CountryID, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "kate",Email = "smith@mail.com", Gender = ServiceContracts.Enums.GenderOptions.Male, Address = "address", CountryID = country_response_1.CountryID, DateOfBirth = DateTime.Parse("2000-01-01"), ReceiveNewsLetters = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //act
            List<PersonResponse> persons_list_from_get = _personService.GetAllPersons();

            //print person_list_from_getall
            _testOutputHelper.WriteLine("actual: ");
            foreach (PersonResponse person_response_from_get in persons_list_from_get)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            //assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_get);
            }
        }

        #endregion

        #region GetFilteredPersons
        //if the search text is empty and search by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            //arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse country_response_1 =
            _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 =
            _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@mail.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "john",
                Email = "smith@mail.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "kate",
                Email = "smith@mail.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //act
            List<PersonResponse> persons_list_from_search = _personService.GetFilteredPersons(nameof(PersonResponse.PersonName), "");

            //print person_list_from_getall
            _testOutputHelper.WriteLine("actual: ");
            foreach (PersonResponse person_response_from_get in persons_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            //assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                Assert.Contains(person_response_from_add, persons_list_from_search);
            }
        }

        // First we will add few persons; and then we will search based on person name with some search string. it should return the matching persons
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            //arrange
            CountryAddRequest country_request_1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest country_request_2 = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse country_response_1 =
            _countriesService.AddCountry(country_request_1);
            CountryResponse country_response_2 =
            _countriesService.AddCountry(country_request_2);

            PersonAddRequest person_request_1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "smith@mail.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest person_request_2 = new PersonAddRequest()
            {
                PersonName = "john",
                Email = "smith@mail.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

            PersonAddRequest person_request_3 = new PersonAddRequest()
            {
                PersonName = "kate",
                Email = "smith@mail.com",
                Gender = ServiceContracts.Enums.GenderOptions.Male,
                Address = "address",
                CountryID = country_response_1.CountryID,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
                person_request_1, person_request_2, person_request_3
            };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = _personService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

            //print person_response_list_from_add
            _testOutputHelper.WriteLine("Expected: ");
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                _testOutputHelper.WriteLine(person_response_from_add.ToString());
            }

            //act
            List<PersonResponse> persons_list_from_search = _personService.GetFilteredPersons(nameof(PersonResponse.PersonName), "ma");

            //print person_list_from_get
            _testOutputHelper.WriteLine("actual: ");
            foreach (PersonResponse person_response_from_get in persons_list_from_search)
            {
                _testOutputHelper.WriteLine(person_response_from_get.ToString());
            }

            //assert
            foreach (PersonResponse person_response_from_add in person_response_list_from_add)
            {
                if (person_response_from_add.PersonName != null)
                {
                    if (person_response_from_add.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person_response_from_add, persons_list_from_search);
                    }
                }
            }
        }
        #endregion
    }
}
