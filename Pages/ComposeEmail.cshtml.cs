using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;



namespace FinalProject.Pages
{
    public class ComposeEmailModel : PageModel
    {
        public EmailInfo emailInfo = new EmailInfo();
        public string errorMessage = "";
        public string successMessage = "";
        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Get values from the form
            emailInfo.EmailReceiver = Request.Form["emailreceiver"];
            emailInfo.EmailSubject = Request.Form["emailsubject"];
            emailInfo.EmailMessage = Request.Form["emailmessage"];
            emailInfo.EmailIsRead = "0";

            

            if (emailInfo.EmailReceiver.Length == 0 || emailInfo.EmailSubject.Length == 0 || 
                emailInfo.EmailMessage.Length == 0)
            {
                errorMessage = "All the fields are required"; 
                return;
            }
            try
            {
                string currentUserName = User.Identity.Name;


                if (emailInfo.EmailReceiver.Equals(currentUserName, StringComparison.OrdinalIgnoreCase))
                {
                    errorMessage = "You cannot send emails to yourself.";
                    return;
                }


                String connectionString = "Server=tcp:adis123.database.windows.net,1433;Initial Catalog=adis;Persist Security Info=False;User ID=adis123;Password=123456sS*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO emails " +
                              "(emailreceiver, emailsubject, emailmessage, emailisread, emailsender) VALUES " +
                              "(@EmailReceiver, @EmailSubject, @EmailMessage, @EmailIsRead, @EmailSender);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                       
                        command.Parameters.AddWithValue("@EmailReceiver", emailInfo.EmailReceiver);
                        command.Parameters.AddWithValue("@EmailSubject", emailInfo.EmailSubject);
                        command.Parameters.AddWithValue("@EmailMessage", emailInfo.EmailMessage);
                        command.Parameters.AddWithValue("@EmailIsRead", emailInfo.EmailIsRead);
                        command.Parameters.AddWithValue("@EmailSender", currentUserName);

                        command.ExecuteNonQuery();
					}

                }
				successMessage = " Correctly";

				Response.Redirect("/index");
			}
            catch (Exception ex) 
            {
				errorMessage = ex.Message;
                errorMessage = "An error occurred while sending the email. Please log in before using.";
                return;
			}
            emailInfo.EmailReceiver = "";
            emailInfo.EmailSubject = "";
            emailInfo.EmailMessage = "";

           
			

		}

    }
}