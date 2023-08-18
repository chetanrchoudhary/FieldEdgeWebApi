namespace ERP.CUST.MGMT.Model
{
    public class CustomerVM
    {
        public string Id { get; set; }
        public string Salutation { get; set; }
        public string Initials { get; set; }
        public string Firstname { get; set; }
        public string Firstname_Ascii { get; set; }
        public string Gender { get; set; }
        public string Firstname_Country_Rank { get; set; }
        public string Firstname_Country_Frequency { get; set; }
        public string Lastname { get; set; }
        public string Lastname_Ascii { get; set; }
        public string Lastname_Country_Rank { get; set; }
        public string Lastname_Country_Frequency { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country_Code { get; set; }
        public string Country_Code_Alpha { get; set; }
        public string Country_Name { get; set; }
        public string Primary_Language_Code { get; set; }
        public string Primary_Language { get; set; }
        public decimal? Balance { get; set; }
        public string Phone_Number { get; set; }
        public string Currency { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime? Timestamp { get; set; }
        public object ETag { get; set; }
    }
}
