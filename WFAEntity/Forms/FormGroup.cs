using System;
using System.Data.Entity;
using System.Windows.Forms;
using WFAEntity.API;
using System.Linq;

namespace WFAEntity
{
    public partial class FormGroup : Form
    {
       // BindingSource objectBindingSource = new BindingSource();
        public FormGroup()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 2;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].HeaderText = "Id"; // header text
            dataGridView1.Columns[0].DataPropertyName = "Id"; // field name
            dataGridView1.Columns[1].HeaderText = "Имя"; // header text
            dataGridView1.Columns[1].DataPropertyName = "Name"; // field name
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            this.ShowAll();
        }

        private void ShowAll()
        {
            try
            {
                using (MyDBContext objectMyDBContext = new MyDBContext())
                {
                    dataGridView1.DataSource = DatabaseRequest.GetGroups(objectMyDBContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                Group objectGroup = new Group();
                objectGroup.Name = textBox1.Text;
                try
                {
                    using (MyDBContext objectMyDBContext = new MyDBContext())
                    {
                        objectMyDBContext.Groups.Add(objectGroup);
                        objectMyDBContext.SaveChanges();
                    }
                    MessageBox.Show("Группа добавлена");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.ShowAll();
            }
            else
                MessageBox.Show("Заполните все поля!", "Ошибка!");
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
                            Group ots = objectMyDBContext.Groups.Find(id);
                            objectMyDBContext.Groups.Remove(ots);
                            objectMyDBContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show("Группа удалена");
                }
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            try
            {
                using (MyDBContext objectMyDBContext = new MyDBContext())
                {
                    objectMyDBContext.Groups.Load();
                    var tmp = from s in objectMyDBContext.Groups
                              where s.Name == textBox2.Text
                              select s;
                    dataGridView1.DataSource = tmp.ToList();//*/objectMyDBContext.Students.Local.ToBindingList();
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            this.ShowAll();
        }
    }
}
