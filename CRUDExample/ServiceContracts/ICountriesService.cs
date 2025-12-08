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
    }
}
