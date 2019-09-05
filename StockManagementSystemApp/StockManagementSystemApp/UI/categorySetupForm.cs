using StockManagementSystem.Manager;
using StockManagementSystem.Models;
using StockManagementSystemApp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagementSystem.UI
{
    public partial class categorySetupForm : Form
    {
        public categorySetupForm()
        {
            InitializeComponent();
        }
        Category category = new Category();
        CategoryManager _categoryManager = new CategoryManager();
        UserManager _userManager = new UserManager();
        List<Category> categories = new List<Category>();
        private object categoryManager;

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormValid())
                {
                    if (SaveButton.Text == "&Save")
                    {
                        category.CategoryName = categoryNameTextBox.Text;
                        category.UserId = _userManager.GetUserId(LoginForm.UserName, LoginForm.Password);
                        category.CreatedDate = DateTime.Now;

                        if (_categoryManager.IsExistCategory(category))
                        {
                            MessageBox.Show("Category name '" + categoryNameTextBox.Text + "' already exist!", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            categoryNameTextBox.Focus();
                            return;
                        }
                        string message = _categoryManager.SaveCategory(category);
                        if (message == "Category Saved Successful.")
                        {
                            MessageBox.Show(message, "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            categories = categoryManager.GetCategories();
                            BindCategoriesListGridView(categories);
                            categoryNameTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }  
                }
                if (SaveButton.Text == "Update")
                {
                    if (IsFormValid())
                    {
                        category.Id =Convert.ToInt32(idLabel.Text);
                        category.CategoryName = categoryNameTextBox.Text;
                        category.UserId = _userManager.GetUserId(LoginForm.UserName, LoginForm.Password);
                        category.CreatedDate = DateTime.Now;

                        if (_categoryManager.IsExistCategory(category))
                        {
                            MessageBox.Show("Category name '" + categoryNameTextBox.Text + "' already exist!", "Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            categoryNameTextBox.Focus();
                            return;
                        }
                        string message = _categoryManager.UpdateCategory(category);
                        if(message== "Category Update Successful.")
                        {
                            MessageBox.Show(message, "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            categories = _categoryManager.GetCategories();
                            BindCategoriesListGridView(categories);
                            categoryNameTextBox.Clear();
                            SaveButton.Text = "&Save";
                            SaveButton.BackColor = Color.Indigo;
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsFormValid()
        {
            if (categoryNameTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter category name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                categoryNameTextBox.Focus();
                categoryNameTextBox.Clear();
                return false;
            }
            return true;
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                categories = _categoryManager.GetCategories();
                BindCategoriesListGridView(categories);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindCategoriesListGridView(List<Category> categories)
        {
            int serial = 0;
            categoriesListGirdView.Rows.Clear();
            foreach(var category in categories)
            {
                serial++;
                categoriesListGirdView.Rows.Add(serial, category.CategoryName,category.Id);
            }
        }

        private void categoriesListGirdView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = categoriesListGirdView.SelectedRows[0];
                categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                idLabel.Text = row.Cells[2].Value.ToString();
                SaveButton.Text = "Update";
                SaveButton.BackColor = Color.Magenta;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
