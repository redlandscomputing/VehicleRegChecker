using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Reg.Models;
using Newtonsoft.Json;

namespace Reg
{
    internal class RegInterface
    {
        // the API key
        private const string ApiKey = "HybH0yr4Hj3eEgybT9pkn6B7PA769YDa8kt4wKdp";

        // the API key name
        private const string ApiKeyName = "x-api-key";

        // the API url
        private const string ApiUrl = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=";

        // single instance of a HTTP client used for accessing the API
        private static HttpClient client = new HttpClient();

        /// <summary>
        /// Method for getting MOT information for a car given a valid registration.
        /// </summary>
        /// <param name="carRegistration">The registration of the vehicle to query.</param>
        /// <returns>Array of vehicle MOT information if the registration is valid, otherwise null is returned.</returns>
        public static async Task<List<CarDetails>> GetRegDetails(string carRegistration)
        {
            try
            {
                // add the API key/value header if not already added to the client
                if (!client.DefaultRequestHeaders.Any())
                {
                    client.DefaultRequestHeaders.Add(ApiKeyName, ApiKey);
                }

                // perform a GET on the API
                var response = await client.GetAsync($"{ApiUrl}{carRegistration}");

                // was the get succesfull?
                if ((response != null) && (response.StatusCode == System.Net.HttpStatusCode.OK))
                {
                    // get the response data JSON and concert into our model
                    return JsonConvert.DeserializeObject<List<CarDetails>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    // the get fails, so serialise the error message
                    List<CarDetails> carDetails = new List<CarDetails>();
                    carDetails.Add(JsonConvert.DeserializeObject<CarDetails>(await response.Content.ReadAsStringAsync()));
                    return carDetails;
                }
            }
            catch (Exception ex)
            {
                // there was an unknown exception, so catch add the exception information to the model
                List<CarDetails> carDetails = new List<CarDetails>();
                carDetails.Add(new CarDetails()
                {
                    ErrorMessage = $"Exception: {ex.Message}"
                }) ;
                return carDetails;
            }
        }
    }
}
