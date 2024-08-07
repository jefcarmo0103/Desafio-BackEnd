using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Requests
{
    public class CreateRentRequest
    {
        public Guid DeliveryManId { get; set; }
        public Guid RentPlanId { get; set; }
        public Guid MotorcycleId { get; set; }
    }
}
