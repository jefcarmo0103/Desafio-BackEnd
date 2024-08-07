using DesafioBackEnd.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Domain.Core.Entities
{
    public class CNHType : BaseEntity
    {
        public string Name { get; set; }
        public bool QualifiedToRentMotorcyle { get; set; }
        public bool Active { get; set; }
    }
}
