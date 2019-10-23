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
	public partial class ModalConteudoCadastradoSucesso : ContentPage
	{
		public ModalConteudoCadastradoSucesso ()
		{
			InitializeComponent ();

            BindingContext = ViewModel.ViewModelLocator.DisciplinasVM;

            LabelCadastroSucesso.Text = "Conteúdo\n" + LabelCadastroSucesso.Text + "\ncadastrado com sucesso!";
        }
	}
}