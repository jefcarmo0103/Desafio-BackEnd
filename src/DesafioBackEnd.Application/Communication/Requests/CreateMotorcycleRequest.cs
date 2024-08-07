using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Requests
{
    public class CreateMotorcycleRequest
    {
        public string Plate { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }

    }
}
