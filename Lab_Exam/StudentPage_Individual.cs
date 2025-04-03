using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Lab_Exam
{
    public partial class StudentPage_Individual : Form
    {
        private int studentId;
        private string connectionString = "server=127.0.0.1; port=3307; user=root; database=studentinfodb; password=";

        public StudentPage_Individual(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
            // Set the font to a more appealing design
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private void StudentPage_Individual_Load(object sender, EventArgs e)
        {
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"
                    SELECT sr.*, c.courseName 
                    FROM studentrecordtb sr
                    JOIN coursetb c ON sr.courseId = c.courseId
                    WHERE sr.studentId = @studentId";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    labelStudentId.Text = "Student ID: " + reader["studentId"].ToString();
                    labelFullName.Text = "Full Name: " + reader["firstName"].ToString() + " " + reader["middleName"].ToString() + " " + reader["lastName"].ToString();
                    labelCourseYear.Text = "Course and Year: " + reader["courseName"].ToString() + " - Year " + reader["yearId"].ToString();
                    labelAddress.Text = "Address: " + reader["houseNo"].ToString() + ", " + reader["brgyName"].ToString() + ", " + reader["municipality"].ToString() + ", " + reader["province"].ToString();
                    labelBirthdate.Text = "Birthdate: " + Convert.ToDateTime(reader["birthdate"]).ToString("yyyy-MM-dd");
                    labelAge.Text = "Age: " + reader["age"].ToString();
                    labelContact.Text = "Contact Info: " + reader["studContactNo"].ToString();
                    labelEmail.Text = "Email Address: " + reader["emailAddress"].ToString();
                    labelGuardian.Text = "Guardian Info: " + reader["guardianFirstName"].ToString() + " " + reader["guardianLastName"].ToString();
                    labelHobbies.Text = "Hobbies: " + reader["hobbies"].ToString();
                    labelNickname.Text = "Nickname: " + reader["nickname"].ToString();
                }
            }
        }
    }
}