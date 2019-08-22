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
	public partial class VisualizarDisciplina : ContentPage
	{
		public VisualizarDisciplina ()
		{
			InitializeComponent ();

            BindingContext = new ViewModel.VisualizarDisciplinaViewModel();
        }
	}
}