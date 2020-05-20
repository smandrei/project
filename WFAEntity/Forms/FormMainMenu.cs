using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WFAEntity.API;
using WFAEntity.Forms;

namespace WFAEntity
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
        }

        private void buttonStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FormStudent fs = new FormStudent();
                fs.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА buttonStudent_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonGroup_Click(object sender, EventArgs e)
        {
            FormGroup fg = new FormGroup();
            fg.Show();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
        }
    }
}
