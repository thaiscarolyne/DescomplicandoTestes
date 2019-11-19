using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DescomplicandoTestes.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PesquisarProvas : ContentPage
	{
		public PesquisarProvas ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

        }


        //Foi preciso colocar essa transição de tela aqui, pois colocando no
        //método GET da disciplina selecionada, tava dando alguns problemas
        //na navegação entre as páginas, tava chamando o método GET em 
        //algumas transições entre as páginas
        public void VisualizarProva(object sender, ItemTappedEventArgs args)
        {      
            App.Current.MainPage.Navigation.PushAsync(new VisualizarProva());
        }
    }
}