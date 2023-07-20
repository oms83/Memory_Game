using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game
{
    public partial class frmPlayerInfo : Form
    {
        public string FullName { get; private set; }

        public frmPlayerInfo()
        {
            InitializeComponent();
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtName, "Name should be have a value");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtName, "");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            FullName = txtName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
