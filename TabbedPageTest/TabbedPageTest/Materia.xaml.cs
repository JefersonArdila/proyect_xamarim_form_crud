using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedPageTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Materia : ContentPage
    {
        public Materia()
        {
            InitializeComponent();

            List<string> materias = new List<string> { "Elemento 1", "Elemento 2", "Elemento 3" };
        }
    }

    


}