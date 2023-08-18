namespace ERP.CUST.MGMT.Utils
{
    public static class AzureWebApiUrl
    {

        public static string GetCustomers = $"{BaseApiUrl.BaseUrl}Customers";
        public static string AddCustomer = $"{BaseApiUrl.BaseUrl}Customer";
        public static string GetCustomerById = $"{BaseApiUrl.BaseUrl}Customer/";

    }

    public static class BaseApiUrl
    {
        public static string BaseUrl = "https://getinvoices.azurewebsites.net/api/";
    }
}
