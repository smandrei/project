using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using WFAEntity.API;

namespace WFAEntity
{
    public partial class FormStudent : Form
    {

        BindingSource objectBindingSource = new BindingSource();
        public FormStudent()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 5;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].HeaderText = "Id"; // header text
            dataGridView1.Columns[0].DataPropertyName = "NId"; // field name
            dataGridView1.Columns[1].HeaderText = "Имя"; // header text
            dataGridView1.Columns[1].DataPropertyName = "NName"; // field name
            dataGridView1.Columns[2].HeaderText = "Фамилия"; // header text
            dataGridView1.Columns[2].DataPropertyName = "NSurname"; // field name

            dataGridView1.Columns[3].HeaderText = "Id Группа"; // header text
            dataGridView1.Columns[3].DataPropertyName = "NIdGroup"; // field name 

            dataGridView1.Columns[4].HeaderText = "Группа"; // header text
            dataGridView1.Columns[4].DataPropertyName = "NGroup"; // field name  
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            comboBoxGroup.ValueMember = "Id";
            comboBoxGroup.DisplayMember = "Name";

            textBoxName.DataBindings.Add("Text",  objectBindingSource, "NName");
            textBoxSurname.DataBindings.Add("Text", objectBindingSource, "NSurname");
            dataGridView1.DataSource = objectBindingSource;
            //comboBoxGroup.DataSource = objectBindingSource;

            //comboBoxGroup.DataBindings.Add("Id", objectBindingSource, "NIdGroup");
            //DataBindings.Add("DisplayMember", objectBindingSource, "Name");
            this.ShowAll();
        }

        private void ShowAll()
        {
            try
            {
                using (MyDBContext objectMyDBContext = new MyDBContext())
                {
                    objectBindingSource.DataSource = DatabaseRequest.GetStudentsWithGroups(objectMyDBContext);
                    
                    //dataGridView1.DataSource = DatabaseRequest.GetStudentsWithGroups(objectMyDBContext);
                    comboBoxGroup.DataSource = DatabaseRequest.GetGroups(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != string.Empty && textBoxSurname.Text != string.Empty)
            {
                Student objectStudent = new Student();
                objectStudent.Group = (Group)comboBoxGroup.SelectedItem;
                objectStudent.Name = textBoxName.Text;
                objectStudent.Surname = textBoxSurname.Text;
                try
                {
                    using (MyDBContext objectMyDBContext = new MyDBContext())
                    {
                        objectMyDBContext.Students.Add(objectStudent);
                        objectMyDBContext.SaveChanges();
                    }
                    this.ShowAll();
                    MessageBox.Show("Студент добавлен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Заполните все поля!", "Ошибка");
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить запись?",
                "Удаление", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Cancel)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    try
                    {
                        using (MyDBContext objectMyDBContext = new MyDBContext())
                        {
                            Student ots = objectMyDBContext.Students.Find(id);
                            objectMyDBContext.Students.Remove(ots);
                            objectMyDBContext.SaveChanges();
                        }
                        MessageBox.Show("Студент удалён");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
