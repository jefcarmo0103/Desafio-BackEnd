using DesafioBackEnd.Application.Communication.Base;
using DesafioBackEnd.Domain.Core.Entities;

namespace DesafioBackEnd.Application.Communication.Responses
{
    public class CheckReturnValueResponse : ResultBase
    {
        public RentResponse Rent { get; set; }
        public decimal EstimatedReturnValue { get; set; }
    }

    public record DeliveryManResponse(Guid id, string name);
    public record RentPlanResponse(Guid id, string description, bool hasAnticipationFee, decimal percentageAnticipation, decimal delayPrice);
    public record MotorCycleResponse(Guid id, string model, int year);

    public record RentResponse(Guid id, DeliveryManResponse deliveryMan, RentPlanResponse rentPlan, MotorCycleResponse motorCycle, string beginDate, string expectedEndDate, string endDate);


    public static class RentExtension
    {
        public static RentResponse ToResponse(this Rent rent)
        {
            var deliveryman = new DeliveryManResponse(rent.DeliveryMan.Id, rent.DeliveryMan.Name);
            var rentPlan = new RentPlanResponse(rent.Plan.Id, rent.Plan.Description, rent.Plan.HasFeeForAnticipation, rent.Plan.AnticipationFeePercentage, rent.Plan.DelayFeePrice);
            var motorcycle = new MotorCycleResponse(rent.Motorcycle.Id, rent.Motorcycle.Model, rent.Motorcycle.Year);

            return new RentResponse(
                rent.Id, 
                deliveryman, 
                rentPlan,
                motorcycle, 
                rent.Begin.ToString("dd/MM/yyyy"), 
                rent.ExpectedEnd.ToString("dd/MM/yyyy"), 
                rent.End?.ToString("dd/MM/yyyy") ?? string.Empty); 
        }
    }
}
