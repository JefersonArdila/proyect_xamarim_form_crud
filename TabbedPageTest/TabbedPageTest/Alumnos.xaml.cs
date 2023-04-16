using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using TabbedPageTest.Models;

namespace TabbedPageTest
{
    public partial class Alumnos : ContentPage
    {
        public Alumnos()
        {
            InitializeComponent();
            llenarDatos();
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (await validarFormulario())
            {
                Alumno alum = new Alumno
                {
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Email = txtEmail.Text,
                };
                await App.SQLiteDB.SaveAlumnoAsync(alum);

                await DisplayAlert("Registro", "Se guardo de manera exitosa el alumno", "ok");
                LimpiarControles();
                llenarDatos();

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingrese todos los datos", "ok");
            }
        }
        public async void llenarDatos()
        {

            var alumnoList = await App.SQLiteDB.GetAlumnosAsync();
            if (alumnoList != null)
            {
                lstAlumnos.ItemsSource = alumnoList;
            }
        }

       
        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAlumno.Text))
            {
                Alumno alumno = new Alumno()
                {
                    IdAlumno = Convert.ToInt32(txtIdAlumno.Text),
                    Nombre = txtNombre.Text,
                    Apellidos = txtApellidos.Text,
                    Email = txtEmail.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                };

                await App.SQLiteDB.SaveAlumnoAsync(alumno);
                await DisplayAlert("Registro", "Se actualizo de manera exitosa el alumno", "ok");
                LimpiarControles();
                txtIdAlumno.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnRegistrar.IsVisible = true;
                llenarDatos();

            }
        }

        private async void lstAlumnos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Alumno)e.SelectedItem;
            btnRegistrar.IsVisible = false;
            txtIdAlumno.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.IdAlumno.ToString()))
            {
                var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(obj.IdAlumno);
                if (alumno != null)
                {
                    txtIdAlumno.Text = alumno.IdAlumno.ToString();
                    txtNombre.Text = alumno.Nombre;
                    txtApellidos.Text = alumno.Apellidos;
                    txtEmail.Text = alumno.Email;
                    txtEdad.Text = alumno.Edad.ToString();

                }
            }

        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var alumno = await App.SQLiteDB.GetAlumnoByIdAsync(Convert.ToInt32(txtIdAlumno.Text));

            if (alumno != null)
            {
                await App.SQLiteDB.DeleteAlumnoAsync(alumno);

                await DisplayAlert("Alumno", "Se elimino de manera exitos", "ok");
                LimpiarControles();
                llenarDatos();
                txtIdAlumno.IsVisible = false;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                btnRegistrar.IsVisible = true;
            }
        }

        public void LimpiarControles()
        {
            txtIdAlumno.Text = "";
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";
        }

         private async Task<bool> validarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio.", "OK");
                return false;
            }
            //Valida que solo se ingresen letras
            else if (!txtNombre.Text.ToCharArray().All(Char.IsLetter))
            {
                await this.DisplayAlert("Advertencia", "Tu información contiene números, favor de validar.", "OK");
                return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(txtNombre.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                    return false;
                }
            }

            if (String.IsNullOrWhiteSpace(txtApellidos.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del apellido es obligatorio.", "OK");
                return false;
            }
            //Valida que solo se ingresen letras
            else if (!txtApellidos.Text.ToCharArray().All(Char.IsLetter))
            {
                await this.DisplayAlert("Advertencia", "Tu información contiene números, favor de validar.", "OK");
                return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(txtApellidos.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                    return false;
                }
            }


            if (String.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo de edad es obligatorio.", "OK");
                return false;
            }
            
            else
            {
                //Valida que solo se ingresen numeros
                if (!txtEdad.Text.ToCharArray().All(Char.IsDigit))
                {
                    await this.DisplayAlert("Advertencia", "El formato de la edad es incorrecto, solo se aceptan numeros.", "OK");
                    return false;
                }
            }
            if (String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del correo electronico es obligatorio.", "OK");
                return false;
            }
            else
            {
                //Valida que el formato del correo sea valido
                bool isEmail = Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (!isEmail)
                {
                    await this.DisplayAlert("Advertencia", "El formato del correo electrónico es incorrecto, revíselo e intente de nuevo.", "OK");
                    return false;
                }
            }

            return true;
        }
        
    }






}
