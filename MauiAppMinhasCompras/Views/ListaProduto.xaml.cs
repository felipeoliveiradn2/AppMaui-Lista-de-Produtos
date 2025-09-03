using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
	//ObservableCollection tem melhor integração com a interface gráfica
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();
		//Linkando ObservableCollection com lista de produtos
		lst_produtos.ItemsSource = lista;
	}

	protected async override void OnAppearing()
	{
		try
		{
			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			{
				await DisplayAlert("Ops", ex.Message, "OK");
			}
		}
	}
	private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		//try catch para tratar os erros do novo produto que aparece ao clicar no Adicionar
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
	}

	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{

		try
		{
			string q = e.NewTextValue;

			lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			{
				await DisplayAlert("Ops", ex.Message, "OK");
			}
		}
	}

	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");
	}

	private async Task MenuItem_Clicked(object sender, EventArgs e)
	{
		/* pega o BindingContext do MenuItem (que é o objeto associado àquela linha da lista)
		var produto = (sender as MenuItem)?.BindingContext as Produto;
		if (produto != null)
		{
			lista.firstordefault procura o primeiro item da lista, verifica se o Id é igual ao id do produto
			var item = lista.FirstOrDefault(p => p.Id == produto.Id);
			if (item != null)
				lista.Remove(item);
		*/

		try
		{
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			// Criado bool fo
			bool confirm = await DisplayAlert(
				"Tem Certeza?", "Remover Produto", "Sim", "Não");

			if (confirm)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);
			}
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
	}

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
		catch (Exception ex) 
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}