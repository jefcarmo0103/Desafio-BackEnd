using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Requests
{
    public class CreateDeliverymanRequest
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateOnly BirthdayDate { get; set; }
        public long NumberCNH { get; set; }
        public Guid TypeCNH { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
