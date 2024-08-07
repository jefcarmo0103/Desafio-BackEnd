using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Requests
{
    public class MotorcycleUpdateRequest
    {
        public Guid Id { get; set; }
        public string Plate { get; set; }
    }
}
