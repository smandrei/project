using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFAEntity.API;

namespace WFAEntity.Forms
{
    public partial class FormАuthorization : Form
    {
        public FormАuthorization()
        {
            InitializeComponent();


            FormCreateDataBase form = new FormCreateDataBase();
            form.Show();
            form.Update();
            form.ShowText("Проверка существования базы данных");
            form.Update();
            using (MyDBContext objectMyDBContext = new MyDBContext())
            {
                if (objectMyDBContext.Database.Exists() == false)
                {
                    form.ShowText("Создание базы данных");
                    form.Update();
                    objectMyDBContext.Database.Create();
                    form.ShowText("Добавление пользователя");
                    form.Update();
                    User objectUser = new User();
                    objectUser.Name = "user name";
                    objectUser.Login = "user";
                    objectUser.Password = "1111";
                    objectMyDBContext.Users.Add(objectUser);
                    objectMyDBContext.SaveChanges();
                }
            }
            form.Close();
        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            bool isEnter = true;
            using (MyDBContext objectMyDBContext = new MyDBContext())
            {
                if (DatabaseRequest.IsUser(objectMyDBContext,
                    textBoxLogin.Text,
                    textBoxPassword.Text) == false)
                {
                    isEnter = false;
                    MessageBox.Show("Ошибка ввода логина или пароля");
                }
            }
            if (isEnter == true)
            {
                this.Hide();
                FormMainMenu form = new FormMainMenu();
                if (form.ShowDialog() == DialogResult.Cancel)
                    this.Close();
            }
            else
                this.Close();
        }
    }
}
