using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DescomplicandoTestes.Model;
using Xamarin.Forms;
using DescomplicandoTestes.View;

namespace DescomplicandoTestes.ViewModel
{
    public class PesquisarDisciplinasViewModel :INotifyPropertyChanged
    {
        private List<Disciplina> _ListaDisciplinas;
        public List<Disciplina> ListaDisciplinas { get { return _ListaDisciplinas; } set { _ListaDisciplinas = value; OnPropertyChanged("ListaDisciplinas"); } }
        
        public PesquisarDisciplinasViewModel()
        {
            ListaDisciplinas = new List<Disciplina>();

            ListaDisciplinas.Add(new Disciplina("Banco de dados","BD"));
            ListaDisciplinas.Add(new Disciplina("Matemática", "MAT"));
            ListaDisciplinas.Add(new Disciplina("Engenharia de software", "ENGSOFT"));
            ListaDisciplinas.Add(new Disciplina("Processamento digital de imagens e teste teste", "PDI"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));
            ListaDisciplinas.Add(new Disciplina("Cálculo Numérico", "CALCNUM"));

        }     

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string NameProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NameProperty));
            }
        }

    }
}
