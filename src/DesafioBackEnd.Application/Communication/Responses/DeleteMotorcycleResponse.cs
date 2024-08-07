using DesafioBackEnd.Application.Communication.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Responses
{
    public class DeleteMotorcycleResponse : ResultBase
    {
        public Guid DeletedId { get; set; }
    }
}
