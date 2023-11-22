using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Humanizer.In;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinalProject.Pages
{
    public class ReadEmailModel : PageModel
    {
		public List<EmailInfo> listEmails = new List<EmailInfo>();

		private readonly ILogger<ReadEmailModel> _logger;

		public ReadEmailModel(ILogger<ReadEmailModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
        {
			String emailid = Request.Query["emailid"];
			try
			{
				String connectionString = "Server=tcp:adis.database.windows.net,1433;Initial Catalog=project_2;Persist Security Info=False;User ID=adis123;Password=123456sS*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM emails WHERE emailid=@emailid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@emailid", emailid);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							
							while (reader.Read())
							{
								EmailInfo emailInfo = new EmailInfo();
								emailInfo.EmailID = "" + reader.GetInt32(0);
								emailInfo.EmailSubject = reader.GetString(1);
								emailInfo.EmailMessage = reader.GetString(2);
								emailInfo.EmailDate = reader.GetDateTime(3).ToString();
								emailInfo.EmailIsRead = reader.GetString(4);
								emailInfo.EmailSender = reader.GetString(5);
								emailInfo.EmailReceiver = reader.GetString(6);

								Console.WriteLine($"Read email: ID = {emailInfo.EmailID}, Sender = {emailInfo.EmailSender}, Subject = {emailInfo.EmailSubject}, Message = {emailInfo.EmailMessage}");
								listEmails.Add(emailInfo);
							}
						}
					}
					String sql2 = "UPDATE emails SET emailisread = 1 WHERE emailid = @emailid";
					using (SqlCommand command2 = new SqlCommand(sql2, connection))
					{
						command2.Parameters.AddWithValue("@emailid", emailid);
						command2.ExecuteNonQuery();
					}

                };
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

		}

    }

}
