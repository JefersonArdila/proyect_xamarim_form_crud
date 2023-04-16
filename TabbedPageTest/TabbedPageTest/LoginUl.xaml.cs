using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedPageTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUl : ContentPage
    {
        

        public LoginUl()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(txtUsername.Text=="admin" && txtPassword.Text=="123")
            {
                
                this.Navigation.PushModalAsync(new Welcome());

                


            }
            else if (txtUsername.Text == "jefer" && txtPassword.Text == "123")
            {

                this.Navigation.PushModalAsync(new Welcome());




            }
            else if (txtUsername.Text == "manu" && txtPassword.Text == "123")
            {

                this.Navigation.PushModalAsync(new Welcome());




            }
            else
            {
                DisplayAlert("ops..", "Username or Password is incorrect", "ok");
            }

            
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new Register());
        }


    }
}