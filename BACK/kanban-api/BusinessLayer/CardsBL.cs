using kanban_api.Models;
using kanban_api.Utils;

namespace kanban_api.BusinessLayer
{
    public class CardsBL
    {
        public void ValidateInsert(CardsPost card)
        {
            var validateModel = ValidateModel.Start();

            validateModel.Fail(string.IsNullOrWhiteSpace(card.Titulo), "Título deve ser preenchido.")
                .Fail(string.IsNullOrWhiteSpace(card.Conteudo), "Conteúdo deve ser preenchido.")
                .Fail(string.IsNullOrWhiteSpace(card.Lista), "Nome da lista deve ser preenchido.")
                .Validate(StatusCodes.Status400BadRequest);
        }
    }
}
