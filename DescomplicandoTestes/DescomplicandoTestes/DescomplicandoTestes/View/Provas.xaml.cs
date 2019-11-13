using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DescomplicandoTestes.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Provas : ContentPage
	{
		public Provas ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = (double)(mainDisplayInfo.Height / mainDisplayInfo.Density);

            if (height < 550)
            {
                LayoutProvasBotoes.Spacing = 50;

                LabelGerarProva.FontSize = 20;
                LabelCorrigirGabaritos.FontSize = 18;
                LabelPesquisarProvas.FontSize = 18;
            }

            if (height > 550 && height < 650)
            {
                LayoutProvasBotoes.Spacing = 75;

                LabelGerarProva.FontSize = 22;
                LabelCorrigirGabaritos.FontSize = 19;
                LabelPesquisarProvas.FontSize = 19;
            }

            if (height > 650)
            {
                LayoutProvasBotoes.Spacing = 90;

                LabelGerarProva.FontSize = 24;
                LabelCorrigirGabaritos.FontSize = 20;
                LabelPesquisarProvas.FontSize = 20;
            }
        }
	}
}