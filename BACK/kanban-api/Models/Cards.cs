using System.ComponentModel.DataAnnotations;

namespace kanban_api.Models
{
    public class Cards
    {
        [Key]
        public Guid Id { get; }
        public string Titulo { get; set; }
        public string Conteudo { get; set;}
        public string Lista { get; set; }

        public Cards(string titulo, string conteudo, string lista)
        {
            Id = new Guid();
            Titulo = titulo;
            Conteudo = conteudo;
            Lista = lista;
        }
    }
}
