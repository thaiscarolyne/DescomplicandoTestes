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
	public partial class PesquisarDisciplinas : ContentPage
	{
		public PesquisarDisciplinas ()
		{
			InitializeComponent ();

            BindingContext = new ViewModel.PesquisarDisciplinasViewModel();
        }

        private void MudarPaginaVisualizarDisciplina(object sender, ItemTappedEventArgs args)
        {
            App.Current.MainPage.Navigation.PushAsync(new VisualizarDisciplina());
        }
    }
}