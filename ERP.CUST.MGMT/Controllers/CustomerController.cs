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
                try
                {
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    return await JsonSerializer.DeserializeAsync
                        <IEnumerable<CustomerVM>>(contentStream, options);
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return Enumerable.Empty<CustomerVM>();
        }

        [HttpPost]
        [Route("add-customer")]
        public async Task AddCustomer(CustomerVM todoItem)
        {
            var todoItemJson = new StringContent(
                JsonSerializer.Serialize(todoItem),
                Encoding.UTF8,
                Application.Json); // using static System.Net.Mime.MediaTypeNames;

            using var httpResponseMessage =
                await _httpClient.PostAsync($"{AzureWebApiUrl.AddCustomer}", todoItemJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        [HttpPut]
        [Route("update-customer")]
        public async Task SaveItemAsync(CustomerVM customer)
        {
            var customerJson = new StringContent(
                JsonSerializer.Serialize(customer),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage =
                await _httpClient.PutAsync($"{AzureWebApiUrl.GetCustomerById}{customer.Id}", customerJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        [HttpDelete]
        [Route("remove-customer")]
        public async Task DeleteItemAsync(long id)
        {
            using var httpResponseMessage =
                await _httpClient.DeleteAsync($"{AzureWebApiUrl.GetCustomerById}{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
