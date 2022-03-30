using System.Text.Json.Serialization;

namespace kanban_api.Models
{
    public class Cards
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set;}
        public string Lista { get; set; }

        public Cards(string titulo, string conteudo, string lista)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Conteudo = conteudo;
            Lista = lista;
        }
    }
}
