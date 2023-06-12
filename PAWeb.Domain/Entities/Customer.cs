using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PAWeb.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            Transactions = new List<Transaction>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
