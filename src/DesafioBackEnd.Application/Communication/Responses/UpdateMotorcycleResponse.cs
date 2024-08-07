using DesafioBackEnd.Application.Communication.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Responses
{
    public class UpdateMotorcycleResponse : ResultBase
    {
        public  Guid UpdatedId { get; set; }
    }
}
