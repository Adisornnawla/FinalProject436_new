using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinalProject.Pages
{
    public class ComposeEmailModel : PageModel
    {
        public EmailInfo emailInfo = new EmailInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            // Get values from the form
            string senderEmail = Request.Form["Sender"];
            string subject = Request.Form["Subject"];
            string emailMessage = Request.Form["EmailMessage"];

            // Now you can use these values as needed

            // For demonstration purposes, let's just print the values to the console
            Console.WriteLine($"Sender: {senderEmail}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {emailMessage}");

            // Rest of your logic here
            // ...

            if (ModelState.IsValid)
            {
                // Process the email sending logic here
                // For demonstration purposes, let's assume a successful sending
                successMessage = "Email sent successfully!";
            }

            // If there are validation errors or the email sending fails,
            // the model state will be automatically populated with errors,
            // and the user will be redirected back to the ComposeEmail page.
        }
    }
}