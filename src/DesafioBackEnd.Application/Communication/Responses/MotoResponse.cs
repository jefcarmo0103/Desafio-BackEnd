using DesafioBackEnd.Application.Communication.Base;
using DesafioBackEnd.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.Communication.Responses
{
    public class MotoResultResponse : ResultBase
    {
        public MotoResponse Data { get; set; }

    }

    public class MotoResponse {
        public MotoResponse()
        {
            
        }
        public MotoResponse(Guid id, string plate, int year, string model) {
            Id = id;
            Plate = plate;
            Year = year;
            Model = model;
        }

        public Guid Id { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
    }

    public static class MotorcycleExtension
    {
        public static MotoResponse ToResponse(this Motorcycle response)
        {
            return new MotoResponse(response.Id, response.Plate, response.Year, response.Model);
        }
    }
    
}
