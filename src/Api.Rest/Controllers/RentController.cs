using DesafioBackEnd.Application.Communication.Requests;
using DesafioBackEnd.Application.Communication.Responses;
using DesafioBackEnd.Application.Orchestration;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Application.Communication.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioBackEnd.Application.UseCases.Locacao.Commands;
using DesafioBackEnd.Application.UseCases.Locacao.Queries;

namespace Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly ApplicationManager _applicationManager;
        public RentController(ApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }


        [HttpPost]
        public async Task<CreateDeliverymanResponse> Create(CreateRentRequest request)
        {
            var command = new CreateRentCommand(request.DeliveryManId, request.RentPlanId, request.MotorcycleId);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new CreateDeliverymanResponse
            {
                GenerateId = operationResult.Data,
                Messages = messages
            };
        }

        [HttpGet(template: "check-return-value/rentId/{rentId}/returnIntentionDate/{returnDate}")]
        public async Task<CheckReturnValueResponse> CheckReturnValue(Guid rentId, DateOnly returnDate)
        {
            var command = new CheckReturnValueQuery(rentId, returnDate.ToDateTime(TimeOnly.MinValue));
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            if(operationResult is { Data: null })
                return new CheckReturnValueResponse { Messages =  messages };

            return new CheckReturnValueResponse
            {
                EstimatedReturnValue = operationResult.Data.EstimatedReturnValue,
                Rent = operationResult.Data.Rent,
                Messages = messages
            };
            
        }


    }
}
