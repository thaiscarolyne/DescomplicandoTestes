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

            BindingContext = new ViewModel.PesquisarDisciplinasViewModel();
        }

        private void MudarPaginaVisualizarDisciplina(object sender, ItemTappedEventArgs args)
        {
            ListView lista = (ListView)sender;

            Disciplina disciplina = (Disciplina)args.Item;

            App.Current.MainPage.Navigation.PushAsync(new VisualizarDisciplina(disciplina));
        }

    }
}