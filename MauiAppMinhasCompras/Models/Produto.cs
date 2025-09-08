using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao = string.Empty;
        double _quantidade = 1;
        double _preco = 1;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao
        {
            get => _descricao;
            set => _descricao = value ?? string.Empty;
        }

        public double? Quantidade
        {
            get => _quantidade;
            set => _quantidade = value ?? 1;
        }

        public double? Preco
        {
            get => _preco;
            set => _preco = value ?? 1;
        }

        public double? Total => Quantidade * Preco;

        // Validação explícita
        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new Exception("Por favor, preencha a descrição");

            if (Quantidade <= 0)
                throw new Exception("Por favor, preencha uma quantidade válida");

            if (Preco <= 0)
                throw new Exception("Por favor, preencha um preço prático");
        }
    }
}
