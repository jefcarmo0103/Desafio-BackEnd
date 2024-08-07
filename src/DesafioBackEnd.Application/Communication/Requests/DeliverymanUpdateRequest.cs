using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Requests
{
    public class DeliverymanUpdateRequest
    {
        public Guid Id { get; set; }
        public IFormFile Photo { get; set; }
    }
}
