using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Base
{
    public class ResultBase
    {
        public IEnumerable<MessageBase> Messages { get; set; }
    }

    public record MessageBase(string message, bool success) { }
}
