using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Reperesents business logic for manipulating COuntry entitiy
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a cuntry onject to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// 
        /// <returns>
        /// Retuns the country object after addng it (including newly)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Returns all countries from the list
        /// </summary>
        /// <returns>All countries from the list as List of CountryResponse</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Return a country object based on the given countryID
        /// </summary>
        /// <param name="countryID">CountryID (guid) to search</param>
        /// <returns>Matching country as CountryResponse object</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);
    }
}
