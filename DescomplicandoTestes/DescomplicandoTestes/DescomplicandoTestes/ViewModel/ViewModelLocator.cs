using System;
using System.Collections.Generic;
using System.Text;

namespace DescomplicandoTestes.ViewModel
{
    public static class ViewModelLocator
    {

        private static DisciplinasViewModel _disciplinasViewModel = new DisciplinasViewModel();
        public static DisciplinasViewModel DisciplinasVM
        {
            get
            {
                return _disciplinasViewModel;
            }
        }
        
    }
}
