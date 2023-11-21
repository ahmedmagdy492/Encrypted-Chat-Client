using EncChatCommonLib.Models;
using EncChatCommonLib.Services;
using EncryptedChatClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedChatClient
{
    public partial class frmSignup : Form
    {
        private readonly IHashingService _hashingService;
        private readonly IUserService _userService;
        public frmSignup()
        {
            InitializeComponent();
            _hashingService = new HashingService();
            _userService = new UserService();
        }

        

        private string IsFormValid()
        {
            if(string.IsNullOrWhiteSpace(txtName.Text))
            {
                return "please enter your name";
            }
            if(string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                return "please enter your email";
            }
            if(!HelperService.IsValidEmail(txtEmail.Text))
            {
                return "invalid email";
            }
            if(string.IsNullOrEmpty(txtPassword.Text))
            {
                return "please enter your password";
            }

            return null;
        }

        private async void btnSignup_Click(object sender, EventArgs e)
        {
            string msg;
            if((msg = IsFormValid()) == null)
            {
                var user = new User
                {
                    Email = txtEmail.Text,
                    Name = txtName.Text,
                    PasswordHash = _hashingService.Hash(txtPassword.Text)
                };
                var btn = ((Button)sender);
                btn.Enabled = false;

                var result = await _userService.Signup(user);
                if(result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    MessageBox.Show("Your Account has been created successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Result);
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
