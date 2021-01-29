using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Users;

namespace Core.Model.Ordering
{
    [Table("Completed Orders")]
    public class CompletedOrder
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
