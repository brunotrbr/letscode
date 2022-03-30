namespace kanban_api.Models
{
    public class CardsPost
    {
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Lista { get; set; }

        public CardsPost(string titulo, string conteudo, string lista)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            Lista = lista;
        }
    }
}
