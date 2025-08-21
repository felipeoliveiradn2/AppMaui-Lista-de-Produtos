using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

	private async void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{ //instanciação de um novo produto
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text,
				//convert para transformar texto em numeros double
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text)
			};
			//usar o await para aguardar o retorno do usuario
			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Invalido", "OK");
		}
		catch (Exception ex) // catch para tratar erros
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
	}
}