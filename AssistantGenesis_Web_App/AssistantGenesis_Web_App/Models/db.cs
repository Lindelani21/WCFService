using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AssistantGenesis_Web_App.Models;

namespace AssistantGenesis_Web_App.Models
{
    public class db
    {

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\mabai\\Desktop\\IFM3A\\Group project\\Code-Lindelani-Practice\\AG_RESTful\\App_Data\\Database.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True");
        public int LoginCheck(login aUser)
        {
            SqlCommand com = new SqlCommand("Sp_login", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserId", aUser.Id);
            com.Parameters.AddWithValue("@Password",aUser.Password);
            SqlParameter oblogin = new SqlParameter();
            oblogin.ParameterName = "IsValid";
            oblogin.SqlDbType = SqlDbType.Bit;
            oblogin.Direction = ParameterDirection.Output;
            com.Parameters.Add(oblogin);

           
            con.Open();
            com.ExecuteNonQuery();
            int res = Convert.ToInt32(oblogin.Value);

            con.Close();
            return res;
        }
    }
}
