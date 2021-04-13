using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBConnection
{
    public partial class Form1 : Form
    {
        string cnnStr = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void getDB()
        {
            string query = "Select count(*) from  LopSH";
            SqlConnection conn = new SqlConnection(cnnStr);
            SqlCommand cmd = new SqlCommand(query,conn);
            conn.Open();
            int n = (int)cmd.ExecuteScalar();
            conn.Close();
            MessageBox.Show(n.ToString());

        }
        private void datasetPhuong()
        {
            string query = "Select * from sv";
            SqlConnection conn = new SqlConnection(cnnStr);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }
        private void excuteFromTxt()
        {
            string query = "select * from sv where MSSV = @MSSV";
            SqlConnection conn = new SqlConnection(cnnStr);
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@MSSV", SqlDbType.NVarChar);
                cmd.Parameters["@MSSV"].Value = txt_query.Text;
                conn.Open();
                SqlDataReader r = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("MSSV",typeof(string)),
                new DataColumn("NameSV",typeof(string)),
                new DataColumn("ID_Lop",typeof(string))

                });

                while (r.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["MSSV"] = r["MSSV"];
                    dr["NameSV"] = r["NameSV"];
                    dr["ID_Lop"] = r["ID_Lop"];
                    dt.Rows.Add(dr);

                }
                conn.Close();
                dataGridView1.DataSource = dt;
            }
            catch(SqlException ex)
            {
                conn.Close();
                MessageBox.Show("Nhap  lai");
                txt_query.Clear();
            }
        }
        private void sqlDataReader()
        {
            string query = "Select * from  SV";
            SqlConnection conn = new SqlConnection(cnnStr);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader r = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV",typeof(string)),
                new DataColumn("NameSV",typeof(string)),
                new DataColumn("ID_Lop",typeof(string))

            });
            
            while (r.Read())
            {
                DataRow dr = dt.NewRow();
                dr["MSSV"] = r["MSSV"];
                dr["NameSV"] = r["NameSV"];
                dr["ID_Lop"] = r["ID_Lop"];
                dt.Rows.Add(dr);
                
            }
            conn.Close();
            dataGridView1.DataSource = dt;

        }
        private void button1_Click(object sender, EventArgs e)
        {

            /*SqlConnection conn = new SqlConnection(cnnStr);
            conn.Open();
            if(conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Hello world");
            }
            conn.Close();
            Console.WriteLine(conn.State.ToString()) */
            //getDB();
            datasetPhuong();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cnnStr = @"Data Source=LAPTOP-FMB4HJAD\SQLEXPRESS;Initial Catalog=DOtnet;Integrated Security=True";
        }
    }
}
