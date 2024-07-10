using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientInfo.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfos clientInfos = new ClientInfos();
        public String ErrorMessage = "";
        public String SuccessMessage = "";
        public void OnGet()
        {
            String Id = Request.Query["Id"];
            try
            {
                String connectionstring = "Data Source=DESKTOP-T38PAMD;Initial Catalog=ClientInfo;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = " SELECT * FROM ClientInfo WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfos.Id = "" + reader.GetInt32(0);
                                clientInfos.Name = reader.GetString(1);
                                clientInfos.Email = reader.GetString(2);
                                clientInfos.PhoneNo = reader.GetString(3);
                                clientInfos.Address = reader.GetString(4);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            clientInfos.Id = Request.Form["Id"];
            clientInfos.Name = Request.Form["Name"];
            clientInfos.Email = Request.Form["Email"];
            clientInfos.PhoneNo = Request.Form["PhoneNo"];
            clientInfos.Address = Request.Form["Address"];

            if (clientInfos.Id.Length == 0 || clientInfos.Name.Length == 0 || clientInfos.Email.Length == 0 ||
                clientInfos.PhoneNo.Length == 0 || clientInfos.Address.Length == 0)
            {
                ErrorMessage = "All the Fields are required";
                return;
            }
            try
            {
                String connectionstring = "Data Source=DESKTOP-T38PAMD;Initial Catalog=ClientInfo;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = " Update ClientInfo SET Name=@Name, Email=@Email, PhoneNo=@PhoneNo, Address=@Address WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", clientInfos.Name);
                        command.Parameters.AddWithValue("@Email", clientInfos.Email);
                        command.Parameters.AddWithValue("@PhoneNo", clientInfos.PhoneNo);
                        command.Parameters.AddWithValue("@Address", clientInfos.Address);
                        command.Parameters.AddWithValue("@Id", clientInfos.Id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;

            }
            Response.Redirect("/Clients/Index");
        }
    }
}

