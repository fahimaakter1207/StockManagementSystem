using StockManagementSystem.Models;
using StockManagementSystemApp.Gateway;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagementSystem.Repository
{
    class CategoryRepository
    {
        ConnectionClass connection;
        SqlCommand cmd;
        SqlDataReader reader;

        public int SaveCategory(Category category)
        {
            int row = 0;
            connection = new ConnectionClass();
            string query = "Insert Into Categories(Name,UserId,CreatedDate) Values(@name,@userId,@date)";
            try
            {
                cmd = new SqlCommand(query, connection.GetConnection());
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                cmd.Parameters.AddWithValue("@userId", category.UserId);
                cmd.Parameters.AddWithValue("@date", category.CreatedDate);
                row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return row;
            }
            catch(Exception exception)
            {
                row = 0;
            }
            finally
            {
                connection.GetClose();
            }
            return row;            
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            connection = new ConnectionClass();
            string query = "Select * From Categories";
            try
            {
                cmd = new SqlCommand(query,connection.GetConnection());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = (int)(reader["Id"]);
                    category.CategoryName = reader["Name"].ToString();
                    category.CreatedDate =Convert.ToDateTime(reader["CreatedDate"]);

                    categories.Add(category);
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.GetClose();
            }
            return categories;
        }
        public int UpdateCategory(Category category)
        {
            int row = 0;
            connection = new ConnectionClass();
            string query = "Update Categories SET Name=@name,UserId=@userId,CreatedDate=@date Where Id=@id";
            try
            {
                cmd = new SqlCommand(query, connection.GetConnection());
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", category.Id);
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                cmd.Parameters.AddWithValue("@userId", category.UserId);
                cmd.Parameters.AddWithValue("@date", category.CreatedDate);
                row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return row;
            }
            catch (Exception exception)
            {
                row = 0;
            }
            finally
            {
                connection.GetClose();
            }
            return row;
        }
        public bool IsExistCategory(Category category)
        {
            connection = new ConnectionClass();
            string query = "Select * From Categories Where Name=@name";
            try
            {
                cmd = new SqlCommand(query, connection.GetConnection());
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", category.CategoryName);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return true;
                }
            }
            catch(Exception exception)
            {
                return false;
            }
            finally
            {
                connection.GetClose();
            }
            return false;
        }
        public List<Category> GetCategoriesByCompany(Company company)
        {
            List<Category> categories = new List<Category>();
            connection = new ConnectionClass();
            string query = "Select DISTINCT c.Name,c.Id From Items AS i INNER JOIN Categories AS c ON i.CategoryId=c.Id Where CompanyId=@company";
            try
            {                
                cmd = new SqlCommand(query, connection.GetConnection());
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@company", company.Id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id =Convert.ToInt32(reader["Id"]);
                    category.CategoryName = reader["Name"].ToString();
                    categories.Add(category);
                }
            }
            catch(Exception ex)
            {
                categories = null;
            }
            finally
            {
                connection.GetClose();
            }
            return categories;
        }
    }
}
