using ERP.CUST.MGMT.Model;
using ERP.CUST.MGMT.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ERP.CUST.MGMT.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("customers")]
        public async Task<IEnumerable<CustomerVM>> GetCustomers()
        {
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            AzureWebApiUrl.GetCustomers)
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return await JsonSerializer.DeserializeAsync
                    <IEnumerable<CustomerVM>>(contentStream, options);


            }
            return Enumerable.Empty<CustomerVM>();
        }


        [HttpGet]
        [Route("customer/{id}")]
        public async Task<CustomerVM> GetCustomerById(int id)
        {
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            AzureWebApiUrl.GetCustomerById + id)
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
            };

            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return await JsonSerializer.DeserializeAsync
                        <CustomerVM>(contentStream, options);

            }
            return new CustomerVM();
        }


        [HttpPost]
        [Route("add-customer")]
        public async Task AddCustomer([FromBody] AddCustomerVM customer)
        {
            var customerJson = new StringContent(
                JsonSerializer.Serialize(customer),
                Encoding.UTF8,
                Application.Json); // using static System.Net.Mime.MediaTypeNames;

            using var httpResponseMessage =
                await _httpClient.PostAsync($"{AzureWebApiUrl.AddCustomer}", customerJson);

            httpResponseMessage.EnsureSuccessStatusCode();

        }

        [HttpPost]
        [Route("update-customer")]
        public async Task SaveItemAsync(CustomerVM customer)
        {
            var customerJson = new StringContent(
                JsonSerializer.Serialize(customer),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PostAsync($"{AzureWebApiUrl.GetCustomerById}{customer.Id}", customerJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        [HttpDelete]
        [Route("remove-customer/{id}")]
        public async Task DeleteItemAsync(long id)
        {
            using var httpResponseMessage =
                await _httpClient.DeleteAsync($"{AzureWebApiUrl.GetCustomerById}{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
