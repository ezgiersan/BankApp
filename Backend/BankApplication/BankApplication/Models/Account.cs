using Newtonsoft.Json;

namespace BankApplication.Models
{
    public class Account
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Balance { get; set; }
    }
}
