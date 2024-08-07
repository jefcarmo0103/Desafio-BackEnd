using DesafioBackEnd.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Entities
{
    public class RentPlan : BaseEntity
    {
        public string Description { get; set; }
        public int QtyDays { get; set; }
        public bool HasFeeForAnticipation { get; set; }
        public decimal Price { get; set; }
        public decimal AnticipationFeePercentage { get; set; }
        public decimal DelayFeePrice { get; set; }
        public bool Active { get; set; }
    }
}
