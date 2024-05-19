using System;
using System.Data.Common;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private MySqlConnection conn;

        public Form1()
        {
            InitializeComponent();
            conn = DBUtils.GetDBConnection(); 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void QueryEmployee(MySqlConnection conn)
        {
            listBox1.Items.Clear();
            string name, code, manufacturer, price_per_unit, min_order_quantity, shelf_life;
            string sql = "SELECT material_name, materials_code, manufacturer, price_per_unit, min_order_quantity, shelf_life FROM materials";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader["material_name"].ToString();
                        code = reader["materials_code"].ToString();
                        manufacturer = reader["manufacturer"].ToString();
                        price_per_unit = reader["price_per_unit"].ToString();
                        min_order_quantity = reader["min_order_quantity"].ToString();
                        shelf_life = reader["shelf_life"].ToString();
                        listBox1.Items.Add("Назва: " + name + ", Код: " + code + ", Виробник: " + manufacturer + 
                        ", Ціна за одиницю: " + price_per_unit + ", Мінімальна кількість замовлення: " + min_order_quantity + ", Термін придатності: " + shelf_life);
                    }
                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                QueryEmployee(conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
