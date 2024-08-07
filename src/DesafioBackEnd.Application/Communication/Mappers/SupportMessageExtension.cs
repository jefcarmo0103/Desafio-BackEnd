using DesafioBackEnd.Application.Communication.Base;
using DesafioBackEnd.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Mappers
{
    public static class SupportMessageExtension
    {
        public static MessageBase ToMessageBase(this SupportMessage message)
        {
            return new MessageBase(message.message, message.sucess);
        }
    }
}
