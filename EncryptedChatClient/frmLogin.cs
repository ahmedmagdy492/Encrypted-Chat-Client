using EncChatCommonLib.Services;
using EncChatCommonLib.ViewModels;
using EncryptedChatClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedChatClient
{
    public partial class frmLogin : Form
    {
        private readonly IHashingService _hashingService;
        private readonly IUserService _userService;
        public frmLogin()
        {
            InitializeComponent();
            _hashingService = new HashingService();
            _userService = new UserService();
        }

        private string IsFormValid()
        {
            if(string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return "please enter your email";
            }
            if(!HelperService.IsValidEmail(txtEmail.Text))
            {
                return "please enter a valid email";
            }
            if(string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                return "please enter your password";
            }

            return null;
        }

        private void lblCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSignup frmSignup = new frmSignup();
            frmSignup.ShowDialog();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string msg;
            if((msg = IsFormValid()) == null)
            {
                var btn = (Button)sender;
                var loginViewModel = new LoginViewModel
                {
                    Email = txtEmail.Text,
                    PasswordHash = _hashingService.Hash(txtPassword.Text)
                };
                btn.Enabled = false;

                var response = await _userService.Login(loginViewModel);
                
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    this.Hide();
                    Form1 form = new Form1(loginViewModel.Email);
                    form.ShowDialog();
                    btn.Enabled = true;
                }
                else
                {
                    MessageBox.Show(response.Result);
                    btn.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
