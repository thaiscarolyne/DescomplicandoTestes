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
	public partial class VisualizarQuestao : ContentPage
	{
		public VisualizarQuestao ()
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

            if (LabelEnunciado.Text.Length <= 100)
            {
                ScrollEnunciado.HeightRequest = 70;
                Box.HeightRequest = 80;
            }
            else
            {
                if (LabelEnunciado.Text.Length > 100 && LabelEnunciado.Text.Length < 210)
                {
                    ScrollEnunciado.HeightRequest = 160;
                    Box.HeightRequest = 170;
                }
                else
                {
                    ScrollEnunciado.HeightRequest = 185;
                    Box.HeightRequest = 200;
                }
            }
            
        }

        public void VisualizarAlternativa(object sender, ItemTappedEventArgs args)
        {
            App.Current.MainPage.Navigation.PushAsync(new VisualizarAlternativa());
        }
    }
}