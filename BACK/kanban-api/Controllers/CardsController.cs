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
        public async Task<ActionResult> Post([FromBody] CardsPost cardPost)
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE <CardsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
