using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade;
        double _preco;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set;}
        public string Descricao {get => Descricao;
                set {
                    if (value == string.Empty)
                    {
                        throw new Exception("Por favor, preencha a descrição");
                    }
                    _descricao = value;
                }
            }        
        public double Quantidade { get => Quantidade;
            set {
                if (value == 0)
                {
                    throw new Exception("Por favor, preencha a quantidade");
                }            
                _quantidade = value;
            } 
        }

        public double Preco { get => Preco;
            set {
                if (value == 0)
                {
                    throw new Exception("Por favor, preencha o preço");
                }
                _preco = value;
            }              
        }
        public double Total { get => Quantidade * Preco; }
    }
}
