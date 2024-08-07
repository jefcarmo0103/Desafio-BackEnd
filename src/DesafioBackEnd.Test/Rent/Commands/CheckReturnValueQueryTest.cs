using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Locacao.Queries;
using DesafioBackEnd.Domain.Core.Interfaces.Engines;
using FluentAssertions;
using Moq;

namespace DesafioBackEnd.Test.Rent.Commands
{
    public class CheckReturnValueQueryTest
    {
        private readonly Mock<ICalculatorEstimatedPriceEngine> _mockCalculatorEstimatedPriceEngine;
        private readonly Mock<IRentRepository> _mockRentRepository;

        public CheckReturnValueQueryTest()
        {
            _mockCalculatorEstimatedPriceEngine = new();
            _mockRentRepository = new();
        }

        [Fact]
        public async Task Handle_Should_OperatioFail_WhenBackIntentionDateIsYesterday()
        {
            //Arrange
            var rentId = Guid.NewGuid();
            var backIntentionDate = DateTime.Now.AddDays(-1).Date;

            var query = new CheckReturnValueQuery(rentId, backIntentionDate);
            var queryHandler = new CheckReturnValueQueryHandler(_mockRentRepository.Object, _mockCalculatorEstimatedPriceEngine.Object);

            //Act 
            var result = await queryHandler.Handle(query, default);

            //Assert
            result.Messages.ToList().Exists(x => !x.sucess).Should().BeTrue();
        }

    }
}
