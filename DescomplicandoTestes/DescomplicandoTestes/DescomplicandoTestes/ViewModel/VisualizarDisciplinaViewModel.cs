using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DescomplicandoTestes.Model;

namespace DescomplicandoTestes.ViewModel
{   
    public class VisualizarDisciplinaViewModel
    {
        private List<Conteudo> _ListaConteudos;
        public List<Conteudo> ListaConteudos { get { return _ListaConteudos; } set { _ListaConteudos = value; OnPropertyChanged("ListaConteudos"); } }

        public VisualizarDisciplinaViewModel(Disciplina disciplina)
        {
            ListaConteudos = new List<Conteudo>();

            ListaConteudos.Add(new Conteudo("Trigger"));
            ListaConteudos.Add(new Conteudo("Álgebra Relacional"));
            ListaConteudos.Add(new Conteudo("Normalização"));
            ListaConteudos.Add(new Conteudo("Projeto de banco de dados"));
            ListaConteudos.Add(new Conteudo("Views"));
            ListaConteudos.Add(new Conteudo("Modelo físico"));
            ListaConteudos.Add(new Conteudo("Modelo lógico"));
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
