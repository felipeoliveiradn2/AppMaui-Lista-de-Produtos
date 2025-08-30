using MauiAppMinhasCompras.Models;
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
        List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
    }
	private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		//try catch para tratar os erros do novo produto que aparece ao clicar no Adicionar
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		} catch (Exception ex) {
			DisplayAlert("Ops", ex.Message, "OK");
		}
	}

	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) 
	{
		string q = e.NewTextValue;

		lista.Clear();

		List<Produto> tmp = await App.Db.Search(q);

		tmp.ForEach(i => lista.Add(i));
	}

	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");
	}

    private void MenuItem_Clicked(object sender, EventArgs e) { 
		// pega o BindingContext do MenuItem (que é o objeto associado àquela linha da lista)
        var produto = (sender as MenuItem)?.BindingContext as Produto;
        if (produto != null)
        {
		// lista.firstordefault procura o primeiro item da lista, verifica se o Id é igual ao id do produto
            var item = lista.FirstOrDefault(p => p.Id == produto.Id);
            if (item != null)
                lista.Remove(item);
        }
    }

}