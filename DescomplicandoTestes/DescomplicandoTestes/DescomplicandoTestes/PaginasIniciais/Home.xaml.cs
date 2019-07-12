using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DescomplicandoTestes.PaginasIniciais
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
            
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var height = (double)(mainDisplayInfo.Height / mainDisplayInfo.Density);
                                   
            if (height < 550)
            {
                LayoutHomeBotoes.Spacing = 20;

                LabelAlunos.FontSize = 22;
                LabelDisciplinas.FontSize = 19;
                LabelTurmas.FontSize = 22;
                LabelProvas.FontSize = 22;
            }

            if (height > 550 && height < 650)
            {
                LayoutHomeBotoes.Spacing = 25;

                LabelAlunos.FontSize = 25;
                LabelDisciplinas.FontSize = 21;
                LabelTurmas.FontSize = 25;
                LabelProvas.FontSize = 25;
            }

            if (height > 650)
            {
                LayoutHomeBotoes.Spacing = 50;

                LabelAlunos.FontSize = 30;
                LabelDisciplinas.FontSize = 26;
                LabelTurmas.FontSize = 30;
                LabelProvas.FontSize = 30;
            }

        }
    }
}