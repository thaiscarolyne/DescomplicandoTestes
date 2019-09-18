using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DescomplicandoTestes.Model;

namespace DescomplicandoTestes.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisualizarDisciplina : ContentPage
	{
		public VisualizarDisciplina ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = (double)(mainDisplayInfo.Height / mainDisplayInfo.Density);

            if (height < 550)
            {
                LabelAdicionarConteudo.FontSize = 17;                
            }

            if (height > 550 && height < 650)
            {
                LabelAdicionarConteudo.FontSize = 18;
            }

            if (height > 650)
            {
                LabelAdicionarConteudo.FontSize = 20;
            }
        }


        public void VisualizarConteudo(object sender, ItemTappedEventArgs args)
        {
            App.Current.MainPage.Navigation.PushAsync(new VisualizarConteudo());
        }
    }
}