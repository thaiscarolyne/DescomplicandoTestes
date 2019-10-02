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
	public partial class VisualizarAlternativa : ContentPage
	{
		public VisualizarAlternativa ()
		{
			InitializeComponent ();            

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

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

            TituloPag.Text = "Letra " + TituloPag.Text;

        }
	}
}