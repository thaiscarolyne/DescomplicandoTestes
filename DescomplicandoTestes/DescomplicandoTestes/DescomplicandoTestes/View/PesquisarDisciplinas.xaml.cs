using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DescomplicandoTestes.Model;

namespace DescomplicandoTestes.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PesquisarDisciplinas : ContentPage
	{
		public PesquisarDisciplinas ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;
        }

        //Foi preciso colocar essa transição de tela aqui, pois colocando no
        //método GET da disciplina selecionada, tava dando alguns problemas
        //na navegação entre as páginas, tava chamando o método GET em 
        //algumas transições entre as páginas
        public void VisualizarDisciplina(object sender, ItemTappedEventArgs args)
        {
            App.Current.MainPage.Navigation.PushAsync(new VisualizarDisciplina());
        }


    }
}