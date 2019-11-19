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
	public partial class GerarProva02 : ContentPage
	{
		public GerarProva02 ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;
        }

        private void QtdFaceisSelecionada(object sender, EventArgs args)
        {
            Picker pik = (Picker)sender;

            if (pik.SelectedIndex > -1)
            {
                int qtdFaceis = (int)(pik.ItemsSource[pik.SelectedIndex]);

                ViewModel.ViewModelLocator.DisciplinasVM.QtdFaceisSelecionada(qtdFaceis);
            }
        }

        private void QtdMediasSelecionada(object sender, EventArgs args)
        {
            Picker pik = (Picker)sender;

            if (pik.SelectedIndex > -1)
            {
                int qtdMedias = (int)(pik.ItemsSource[pik.SelectedIndex]);

                ViewModel.ViewModelLocator.DisciplinasVM.QtdMediasSelecionada(qtdMedias);
            }
        }

        private void QtdDificeisSelecionada(object sender, EventArgs args)
        {
            Picker pik = (Picker)sender;

            if (pik.SelectedIndex > -1)
            {
                int qtdDificeis = (int)(pik.ItemsSource[pik.SelectedIndex]);

                ViewModel.ViewModelLocator.DisciplinasVM.QtdDificeisSelecionada(qtdDificeis);
            }
        }

        private void FocarPikFacil(object sender, ClickedEventArgs args)
        {
            PikFacil.Focus();
        }

        private void FocarPikMedio(object sender, ClickedEventArgs args)
        {
            PikMedio.Focus();
        }

        private void FocarPikDificil(object sender, ClickedEventArgs args)
        {
            PikDificil.Focus();
        }
    }
}