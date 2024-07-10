using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientInfo.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfos clientInfos = new ClientInfos();
        public String ErrorMessage = "";
        public String SuccessMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            clientInfos.Name = Request.Form["Name"];
            clientInfos.Email = Request.Form["Email"];
            clientInfos.PhoneNo = Request.Form["PhoneNo"];
            clientInfos.Address = Request.Form["Address"];

            if (clientInfos.Name.Length == 0 || clientInfos.Email.Length == 0 ||
                clientInfos.PhoneNo.Length == 0 || clientInfos.Address.Length == 0)
            {
                ErrorMessage = "All the Fields are required";
                return;
            }

            //Save The data into the data base

            try
            {
                String connectionstring = "Data Source=DESKTOP-T38PAMD;Initial Catalog=ClientInfo;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = " INSERT INTO ClientInfo" +
                        "(Name, Email, PhoneNo, Address) VALUES" +
                        "(@Name, @Email, @PhoneNo, @Address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", clientInfos.Name);
                        command.Parameters.AddWithValue("@Email", clientInfos.Email);
                        command.Parameters.AddWithValue("@PhoneNo", clientInfos.PhoneNo);
                        command.Parameters.AddWithValue("@Address", clientInfos.Address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception" + ex.ToString());
            }


            clientInfos.Name = ""; clientInfos.Email = ""; clientInfos.PhoneNo = ""; clientInfos.Address = "";

            SuccessMessage = "New Client Added Corectly";

        }
    }
}
