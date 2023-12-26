using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class LoanMVC
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LoanRequest { get; set; }

        public Decimal Amount { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public int StatusId { get; set; }

        public Status status { get; set; }

        public string WaringMessage { get; set; }

    }
}
