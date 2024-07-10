using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using System.Data;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDbContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            //prepara banco para inserir entidade
            dbContext.Trips.Add(entity);
            //persiste esta entidade no banco de dados
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = entity.EndDate,
                StartDate = entity.StartDate,
                Name = entity.Name,
                Id = entity.Id

            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            //funcão devolve um valor booleano TRUE se o nome for vazio/null/em branco
            if (string.IsNullOrWhiteSpace(request.Name))
            {   
                //tratamento exceção, busca mensagem no arquivo ResourceErrorMessage
                throw new JourneyException(ResourceErrorMessage.NAME_EMPTY);
            }
            //valida data com UtcNow, data base para todos os países.
            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {   
                throw new JourneyException(ResourceErrorMessage.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
            }
            //valida janela das datas.
            if (request.EndDate.Date < request.StartDate.Date)
            {   
                throw new JourneyException(ResourceErrorMessage.END_DATE_TRIP_MUST_BE_LATER_START_DATE);   
            }
        }
    }
}
