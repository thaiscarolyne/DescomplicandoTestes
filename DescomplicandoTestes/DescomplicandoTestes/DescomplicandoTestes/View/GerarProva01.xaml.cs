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
	public partial class GerarProva01 : ContentPage
	{
		public GerarProva01 ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            if(EntryValProv.Text == "0")
            {
                EntryValProv.Text = null;
            }
        }

        private void FiltrarConteudos(object sender, EventArgs args)
        {
            Picker pik = (Picker)sender;

            if (pik.SelectedIndex > -1)
            {
                Model.Disciplina disciplina = (Model.Disciplina)(pik.ItemsSource[pik.SelectedIndex]);

                ViewModel.ViewModelLocator.DisciplinasVM.FiltrarConteudosAction(disciplina);
            }
        }

        private void ConteudoProvaSelecionado(object sender, EventArgs args)
        {
            Picker pik = (Picker)sender;

            if(pik.SelectedIndex > -1)
            {
                Model.Conteudo conteudo = (Model.Conteudo)(pik.ItemsSource[pik.SelectedIndex]);

                ViewModel.ViewModelLocator.DisciplinasVM.ConteudoProvaSelecionado(conteudo);
            }           
        }


        private void FocarPikDisciplina(object sender, ClickedEventArgs args)
        {
            PikDisciplina.Focus();
        }


        private void FocarPikConteudo(object sender, ClickedEventArgs args)
        {
            PikConteudo.Focus();
        }


    }
}