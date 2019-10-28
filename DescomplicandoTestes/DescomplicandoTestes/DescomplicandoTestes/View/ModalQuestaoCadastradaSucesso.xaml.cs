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
	public partial class ModalQuestaoCadastradaSucesso : ContentPage
	{
		public ModalQuestaoCadastradaSucesso ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            LabelCadastroSucesso.Text = "Questao\n" + LabelCadastroSucesso.Text + "\ncadastrada com sucesso!";
        }
	}
}