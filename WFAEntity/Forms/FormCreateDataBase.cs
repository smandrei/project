using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WFAEntity.Forms
{
    public partial class FormCreateDataBase : Form
    {
        public FormCreateDataBase()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("", "");
        }
        public void ShowText(string text)
        {
            dataGridView1.Rows.Add(text);
            this.Width = dataGridView1.Width + 10;
        }
    }
}
