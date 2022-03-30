using kanban_api.BusinessLayer;
using kanban_api.Interfaces;
using kanban_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kanban_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardsBL _cardsBL;
        private readonly IBaseRepository<Cards> _repository;

        public CardsController(IBaseRepository<Cards> repository, CardsBL cardsBL)
        {
            _repository = repository;
            _cardsBL = cardsBL;
        }
        // GET: <CardsController>
        [HttpGet, Authorize]
        [ProducesResponseType(typeof(List<Cards>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get()
        {
            var cards = await _repository.Get();
            return Ok(cards);
        }

        // POST <CardsController>
        [HttpPost, Authorize]
        [ProducesResponseType(typeof(Cards), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CardsPost cardPost)
        {
            _cardsBL.ValidateInsert(cardPost);

            Cards card = new Cards(cardPost.Titulo, cardPost.Conteudo, cardPost.Lista);

            _ = await _repository.Insert(card);

            var newCard = await _repository.GetByKey(card.Id);

            return Created(String.Empty, newCard);
        }

        // PUT <CardsController>/5
        [HttpPut("{id}"), Authorize]
        [ProducesResponseType(typeof(Cards), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Cards card)
        {
            _cardsBL.ValidateUpdate(id, card);

            var databaseCard = await _repository.GetByKey(id);

            if (databaseCard == null)
            {
                var error = "Id inexistente.";
                return NotFound(error);
            }
            databaseCard.Titulo = card.Titulo;
            databaseCard.Conteudo = card.Conteudo;
            databaseCard.Lista = card.Lista;

            _ = await _repository.Update(id, databaseCard);

            databaseCard = await _repository.GetByKey(id);

            return Ok(databaseCard);
        }

        // DELETE <CardsController>/5
        [HttpDelete("{id}"), Authorize]
        [ProducesResponseType(typeof(List<Cards>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var databaseCard = await _repository.GetByKey(id);

            if (databaseCard == null)
            {
                var error = "Id inexistente.";
                return NotFound(error);
            }

            _ = await _repository.Delete(id);

            var cards = await _repository.Get();
            return Ok(cards);
        }
    }
}
