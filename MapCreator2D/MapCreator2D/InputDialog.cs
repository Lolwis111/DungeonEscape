using System;
using System.Windows.Forms;

namespace MapCreator2D
{
    public partial class InputDialog : Form
    {
        public string EnteredText { get; set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            EnteredText = txtName.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
