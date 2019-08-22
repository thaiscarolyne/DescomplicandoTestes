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
	public partial class Disciplinas : ContentPage
	{
		public Disciplinas ()
		{
			InitializeComponent ();

            BindingContext = new ViewModel.DisciplinasViewModel();

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = (double)(mainDisplayInfo.Height / mainDisplayInfo.Density);

            if (height < 550)
            {
                LayoutDisciplinasBotoes.Spacing = 50;

                LabelCadastrarDisciplina.FontSize = 15;
                LabelPesquisarDisciplinas.FontSize = 14;
            }

            if (height > 550 && height < 650)
            {
                LayoutDisciplinasBotoes.Spacing = 75;

                LabelCadastrarDisciplina.FontSize = 15;
                LabelPesquisarDisciplinas.FontSize = 14;
            }

            if (height > 650)
            {
                LayoutDisciplinasBotoes.Spacing = 120;

                LabelCadastrarDisciplina.FontSize = 19;
                LabelPesquisarDisciplinas.FontSize = 18;
            }
        }
	}
}