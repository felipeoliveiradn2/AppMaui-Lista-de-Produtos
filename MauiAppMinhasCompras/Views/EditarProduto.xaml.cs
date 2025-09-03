using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}
	private async void ToolbarItem_Clicked(object sender, EventArgs e) {
        try
        { //instanciação de um novo produto

            Produto produto_anexado = BindingContext as Produto;
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                //convert para transformar texto em numeros double
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };
            //usar o await para aguardar o retorno do usuario
            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Registro Invalido", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex) // catch para tratar erros
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
