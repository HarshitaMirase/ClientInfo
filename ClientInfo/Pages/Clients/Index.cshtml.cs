using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ClientInfo.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfos> listClients = new List<ClientInfos>();
        public void OnGet()
        {
            try
            {
                String connectionstring = "Data Source=DESKTOP-T38PAMD;Initial Catalog=ClientInfo;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    String sql = " SELECT * FROM ClientInfo";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                ClientInfos clientInfo= new ClientInfos();
                                clientInfo.Id = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.PhoneNo = reader.GetString(3);
                                clientInfo.Address = reader.GetString(4);

                                listClients.Add(clientInfo);
                            }
                            
                        }
                    }
                }
            }
            catch(Exception ex)
            { 
                Console.WriteLine("Exception"+ex.ToString());
            }
        }
    }
    public class ClientInfos
    {
        public string Id;
        public string Name;
        public string Email;
        public string PhoneNo;
        public string Address;
    }
}
