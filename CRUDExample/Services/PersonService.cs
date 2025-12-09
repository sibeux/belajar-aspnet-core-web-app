using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PersonService : IPersonsService
    {
        public PersonResponse AddPerson(PersonAddRequest?personAddRequest)
        {
            throw new NotImplementedException();
        }

        public List<PersonResponse> GetAllPersons()
        {
            throw new NotImplementedException();
        }
    }
}
