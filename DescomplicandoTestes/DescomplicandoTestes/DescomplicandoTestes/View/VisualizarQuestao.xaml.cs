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

            if(LabelEnunciado.Text.Length <= 100)
            {
                ScrollEnunciado.HeightRequest = 70;
                Box.HeightRequest = 100;
            }
            else
            {
                ScrollEnunciado.HeightRequest = 170;
                Box.HeightRequest = 200;
            }
        }
	}
}