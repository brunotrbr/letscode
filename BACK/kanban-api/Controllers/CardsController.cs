using kanban_api.BusinessLayer;
using kanban_api.Models;
using kanban_api.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kanban_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardsBL _cardsBL;
        private Dictionary<Guid, Cards> database = new Dictionary<Guid, Cards>();

        public CardsController(CardsBL cardsBL)
        {
            _cardsBL = cardsBL;
        }
        // GET: <CardsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET <CardsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST <CardsController>
        [HttpPost]
        [ProducesResponseType(typeof(Cards), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CardsPost cardPost)
        {
            // Criar uma validação para verificar se já existe o guid na tabela dentro do ValidateInsert
            _cardsBL.ValidateInsert(cardPost);

            Cards card = new Cards(cardPost.Titulo, cardPost.Conteudo, cardPost.Lista);

            await Task.Run(() => { database.Add(card.Id, card); });

            var newCard = await Task.Run(() => { return database[card.Id]; });

            return Created(String.Empty, newCard);
        }

        // PUT <CardsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Cards), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Cards card)
        {
            _cardsBL.ValidateUpdate(id, card);

            Cards databaseCard;
            if (database.TryGetValue(card.Id, out databaseCard) == false)
            {
                var error = "Id inexistente.";
                return NotFound(error);
                
            }

            databaseCard = database[card.Id];
            databaseCard.Titulo = card.Titulo;
            databaseCard.Conteudo = card.Conteudo;
            databaseCard.Lista = card.Lista;

            database[card.Id] = databaseCard;

            var newCard = database[card.Id];
            return Ok(newCard);
        }

        // DELETE <CardsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<Cards>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (database.TryGetValue(id, out _) == false)
            {
                var error = "Id inexistente.";
                return NotFound(error);
            }

            database.Remove(id);

            return Ok(database.Values);
        }
    }
}
