using DesafioBackEnd.Application.Communication.Requests;
using DesafioBackEnd.Application.Communication.Responses;
using DesafioBackEnd.Application.Orchestration;
using DesafioBackEnd.Application.UseCases.Moto.Commands;
using DesafioBackEnd.Application.Communication.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioBackEnd.Application.UseCases.Moto.Queries;

namespace Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleController : ControllerBase
    {
        private readonly ApplicationManager _applicationManager;
        public MotorcycleController(ApplicationManager applicationManager)
        {

            _applicationManager = applicationManager;

        }

        [HttpPost(Name = "CreateMotorcycle")]
        public async Task<CreateMotorcycleResponse> Create(CreateMotorcycleRequest request)
        {
            var command = new CreateMotorcycleCommand(request.Year, request.Model, request.Plate);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new CreateMotorcycleResponse
            {
                GenerateId = operationResult.Data,
                Messages = messages
            };
        }


        [HttpPut(Name = "UpdateMotorcycle")]
        public async Task<UpdateMotorcycleResponse> Update(MotorcycleUpdateRequest request)
        {
            var command = new UpdateMotorcycleCommand(request.Id, request.Plate);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new UpdateMotorcycleResponse
            {
                UpdatedId = operationResult.Data,
                Messages = messages
            };
        }

        [HttpDelete(Name = "DeleteMotorcyle")]
        public async Task<DeleteMotorcycleResponse> Delete(Guid id)
        {
            var command = new DeleteMotorcyleCommand(id);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new DeleteMotorcycleResponse
            {
                DeletedId = operationResult.Data,
                Messages = messages
            };
        }

        [HttpGet(Name = "GetAllMotorcycles")]
        public async Task<IEnumerable<MotoResponse>> GetAll()
        {
            var command = new GetMotorcyclesQuery();
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);

            if(operationResult is { Data: not null})
                return operationResult.Data;
            else
                return Enumerable.Empty<MotoResponse>();
        }

        [HttpGet(template: "{plate}/byPlate")]
        public async Task<MotoResultResponse> GetByPlate(string plate)
        {
            var command = new GetMotorcycleByPlateQuery(plate);
            var operationResult = await _applicationManager.ExecuteOperationWithResultAsync(command);
            var messages = operationResult.Messages.Select(x => x.ToMessageBase());

            return new MotoResultResponse
            {
                Data = operationResult.Data,
                Messages = messages
            };

        }
    }
}
