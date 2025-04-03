using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Lab_Exam
{
    public partial class Student_Page : Form
    {
        private string connectionString = "server=127.0.0.1; port=3307; user=root; database=studentinfodb; password=";

        public Student_Page()
        {
            InitializeComponent();
            // Set the font to a more appealing design
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private void Student_Page_Load(object sender, EventArgs e)
        {
            LoadStudentRecords();
        }

        private void LoadStudentRecords()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT studentId, CONCAT(firstName, ' ', lastName) AS fullName FROM studentrecordtb";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
                viewButtonColumn.Name = "ViewButton";
                viewButtonColumn.HeaderText = "";
                viewButtonColumn.Text = "View";
                viewButtonColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(viewButtonColumn);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["ViewButton"].Index && e.RowIndex >= 0)
            {
                int studentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["studentId"].Value);
                StudentPage_Individual individualPage = new StudentPage_Individual(studentId);
                individualPage.Show();
            }
        }
    }
}