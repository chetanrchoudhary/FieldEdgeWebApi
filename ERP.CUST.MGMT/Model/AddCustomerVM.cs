namespace ERP.CUST.MGMT.Model
{
    public class AddCustomerVM
    {
        public string? Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Country_Code { get; set; }
        public string? Phone_Number { get; set; }
        public string? Gender { get; set; }
        public decimal? Balance { get; set; }
    }
}
