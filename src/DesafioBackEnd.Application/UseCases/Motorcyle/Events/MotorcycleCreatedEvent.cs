using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.UseCases.Motorcyle.Events
{
    public record MotorcycleCreatedEvent(Guid MotorCycleId, int Year)
    {
        public string QUEUE_DESTINATION { get { return "motorcycleCreatedQueue"; } }
    }
}
