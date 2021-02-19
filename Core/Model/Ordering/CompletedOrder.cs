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
    public class CompletedOrder : Order
    {
        public DateTime CompleteDate { get; set; }

        public CompletedOrder()
        {
        }


    }
}
