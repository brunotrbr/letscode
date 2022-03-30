using kanban_api.Interfaces;
using kanban_api.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace kanban_api.Utils
{
    public class LogFilter : IActionFilter, IResultFilter
    {
        private const string PUTMETHOD = "PUT";
        private const string DELETEMETHOD = "DELETE";

        private readonly List<int> _successStatusCodes;
        private readonly IBaseRepository<Cards> _repository;
        private Dictionary<Guid, Object> _contextDict;

        public LogFilter(IBaseRepository<Cards> repository)
        {
            _repository = repository;
            _contextDict = new Dictionary<Guid, Object>();
            _successStatusCodes = new List<int>();
            _successStatusCodes.Add(StatusCodes.Status200OK);
            _successStatusCodes.Add(StatusCodes.Status201Created);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.Equals(context.ActionDescriptor.RouteValues["controller"], "cards", StringComparison.InvariantCultureIgnoreCase))
            {
                if (context.HttpContext.Request.Method == DELETEMETHOD)
                {
                    Guid id = new Guid(context.ActionArguments["id"].ToString());
                    var card = _repository.GetByKey(id).Result;
                    if (card != null)
                    {
                        _contextDict.Add(id, card);
                    }
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Request.Path.Value.StartsWith("/Cards", StringComparison.InvariantCultureIgnoreCase))
            {
                var statusCode = context.HttpContext.Response.StatusCode;

                if (_successStatusCodes.Contains(statusCode))
                {

                    if (context.HttpContext.Request.Method == PUTMETHOD)
                    {
                        string id = context.HttpContext.Request.Path.ToString().Split("/")[2];
                        Guid guidId = new Guid(id);
                        var card = _repository.GetByKey(guidId).Result;
                        if (card != null)
                        {
                            SaveLog(card.Id, card.Titulo, PUTMETHOD);
                        }

                    }
                    else if (context.HttpContext.Request.Method == DELETEMETHOD)
                    {
                        string id = context.HttpContext.Request.Path.ToString().Split("/")[2];
                        Guid guidId = new Guid(id);
                        Object card;
                        if (_contextDict.TryGetValue(guidId, out card))
                        {
                            Cards castedCard = (Cards)card;
                            SaveLog(castedCard.Id, castedCard.Titulo, DELETEMETHOD);
                            _contextDict.Clear();
                        }
                    }
                }
            }
        }

        private void SaveLog(Guid id, string title, string method)
        {
            var now = DateTime.Now.ToString("G");

            if (method == PUTMETHOD)
            {
                Console.WriteLine($"{now} - Card {id} - {title} - Atualizado");

            }
            else if (method == DELETEMETHOD)
            {
                Console.WriteLine($"{now} - Card {id} - {title} - Removido");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
    }
}
