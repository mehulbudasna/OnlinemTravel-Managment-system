﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OTMS
{
    public partial class booking : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand command;
        
        String rid,id;
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid.Text = (String)Session["red"];
          

            conn = new SqlConnection(connectionString);
            conn.Open();

            String selectdata="select * from packagedetails where p_id='"+Session["red"]+"'";
            command=new SqlCommand(selectdata,conn);
            SqlDataReader rdr=command.ExecuteReader();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    pkname.Text = rdr["PackageName"].ToString();
                    ctg.Text = rdr["Category"].ToString();
                    des.Text = rdr["Description"].ToString();
                    type.Text = rdr["Types"].ToString();
                    day.Text = rdr["Days"].ToString();
                    amount.Text = rdr["Amount"].ToString();
                }

            }
            conn.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            String rand=randomnumber();
           
            
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            conn.Open();
            command.CommandText = "insert into booking (packageID,PackageName,Category,Description,Types,Days,Amount,Date,BookingID) values (@username,@gender,@full_name,@email,@country,@zip_code,@city,@Age,@c_id)";
            command.Parameters.AddWithValue("@username", pid.Text);
            command.Parameters.AddWithValue("@gender", pkname.Text);
            command.Parameters.AddWithValue("@full_name", ctg.Text);
            command.Parameters.AddWithValue("@email", des.Text);
            command.Parameters.AddWithValue("@country", type.Text);
            command.Parameters.AddWithValue("@zip_code", day.Text);
            
            command.Parameters.AddWithValue("@city", amount.Text);
            command.Parameters.AddWithValue("@Age", date.Text);
            command.Parameters.AddWithValue("@c_id", rand);
            command.ExecuteNonQuery();
            Label1.Text = "Booking Successful....";
            Response.Redirect("bookingstatus.aspx");

            conn.Close();
        }
        private String randomnumber()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            Random rnd = new Random();
            id = rnd.Next(1, 150).ToString();
            String select = "select BookingID from booking";
            SqlCommand command = new SqlCommand(select, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet data = new DataSet();
            adapter.Fill(data, "id");
            foreach (DataRow row in data.Tables["id"].Rows)
            {
                String did = "" + row["BookingID"];
                if (did != id)
                {
                    rid = id;
                    Session["bid"] = rid;
                    break;
                }
            }
            if (rid == null)
            {
                rid = id;
            }
            conn.Close();
            return rid;
        }
}
}