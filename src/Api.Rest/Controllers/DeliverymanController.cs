using DesafioBackEnd.Application.Communication.Requests;
using DesafioBackEnd.Application.Communication.Responses;
using DesafioBackEnd.Application.Orchestration;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Application.Communication.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverymanController : ControllerBase
    {
        private readonly ApplicationManager _applicationManager;
        public DeliverymanController(ApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [HttpPost(Name = "CreateDeliveryman")]
        public async Task<CreateDeliverymanResponse> Create([FromForm]CreateDeliverymanRequest request)
        {
            var command = new CreateDeliverymanCommand(request.Name, request.CNPJ, request.BirthdayDate.ToDateTime(TimeOnly.MinValue), request.NumberCNH, request.TypeCNH, request.FormFile);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new CreateDeliverymanResponse
            {
                GenerateId = operationResult.Data,
                Messages = messages
            };
        }


        [HttpPut(template: "update-photo")]
        public async Task<UpdateDeliverymanResponse> Update([FromForm] DeliverymanUpdateRequest request)
        {
            var command = new UpdatePhotoDeliverymanCommand(request.Id, request.Photo);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new UpdateDeliverymanResponse
            {
                UpdatedId = operationResult.Data,
                Messages = messages
            };
        }
    }
}
