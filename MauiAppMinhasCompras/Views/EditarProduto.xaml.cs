
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = double.Parse(txt_quantidade.Text),
                Preco = double.Parse(txt_preco.Text)
            };

            // Chama validação aqui, só para os valores do usuário
            p.Validar();

            // Salva no banco
            await App.Db.Update(p);

            await DisplayAlert("Sucesso", $"Produto {p.Descricao} salvo!\nTotal: {p.Total}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}
