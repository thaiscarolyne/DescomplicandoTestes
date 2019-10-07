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
	public partial class ModalDisciplinaCadastradaSucesso : ContentPage
	{
		public ModalDisciplinaCadastradaSucesso ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            LabelCadastroSucesso.Text = "Disciplina\n" + LabelCadastroSucesso.Text + "\ncadastrada com sucesso!";

        }
	}
}