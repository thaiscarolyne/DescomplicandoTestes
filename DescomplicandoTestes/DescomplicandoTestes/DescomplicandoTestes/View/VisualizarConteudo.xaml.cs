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
	public partial class VisualizarConteudo : ContentPage
	{
        public VisualizarConteudo ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            Thickness margin = Cabecalho.Margin;

            if (Cabecalho.Text.Length <= 23)
            {
                margin.Left = (double)-40;
            }
            else
            {
                if (Cabecalho.Text.Length <= 25)
                {
                    margin.Left = (double)-20;
                }
            }

            Cabecalho.Margin = margin;
        }

        public void VisualizarQuestao(object sender, ItemTappedEventArgs args)
        {
           
             App.Current.MainPage.Navigation.PushAsync(new VisualizarQuestao());  
        }
    }
}