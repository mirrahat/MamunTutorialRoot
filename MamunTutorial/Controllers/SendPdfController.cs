using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using MamunTutorial.Data;
using MamunTutorial.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Cors;


[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class SendPdfController : ControllerBase
{
    private readonly ApplicationDBContext _dbcontext;

    public SendPdfController(ApplicationDBContext configuration)
    {
        _dbcontext = configuration;
    }


    /*[HttpPost("send-pdf")]
    public async Task<IActionResult> SendPdfToEmail(IFormFile pdf, string email)
    {
        if (pdf == null || string.IsNullOrEmpty(email))
        {
            return BadRequest("Invalid request.");
        }

        try
        {
            // Set up email settings from configuration
            var smtpServer = _configuration["SmtpServer"];
            var smtpPort = int.Parse(_configuration["SmtpPort"]);
            var smtpUser = _configuration["SmtpUser"];
            var smtpPass = _configuration["SmtpPass"];
            var senderEmail = _configuration["SenderEmail"];

            // Convert PDF to byte array
            byte[] pdfBytes;
            using (var memoryStream = new MemoryStream())
            {
                await pdf.CopyToAsync(memoryStream);
                pdfBytes = memoryStream.ToArray();
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Bill Summary",
                Body = "Please find the attached bill summary.",
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            var attachment = new Attachment(new MemoryStream(pdfBytes), "bill-summary.pdf", "application/pdf");
            mailMessage.Attachments.Add(attachment);

            using (var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            })
            {
                await smtpClient.SendMailAsync(mailMessage);
            }

            return Ok(new { message = "PDF sent successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Failed to send email", error = ex.Message });
        }
    }*/
    /*[AllowAnonymous]
    [HttpPost]
    [Route("send-pdf")]
    // Make sure the endpoint allows unauthenticated access
    public IActionResult SendPDF(int x)
    {
       *//* if (pdf == null || pdf.Length == 0)
            return BadRequest("PDF file is required.");

      *//*
        // Process the PDF and email here

        return Ok(new { message = "PDF received successfully!" });
    }*/


    [AllowAnonymous]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> PostSendPDF([FromForm] IFormFile pdf, [FromForm] string email)
    {
        try
        {
            Console.WriteLine("Starting email sending process...");

            // Step 1: Save the received PDF to a temporary location
            var tempFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), pdf.FileName);
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await pdf.CopyToAsync(stream);
            }

            Console.WriteLine($"PDF saved successfully at: {tempFilePath}");

            // Step 2: Create the email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App Name", "your-email@example.com")); // Replace with your email
            message.To.Add(new MailboxAddress("Target User", email)); // Replace with recipient email
            message.Subject = "Your Bill - Test Email";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = "Dear User,\n\nPlease find your bill attached as a PDF for testing purposes.\n\nRegards,\nYour App Team"
            };

            // Step 3: Attach the PDF to the email
            if (System.IO.File.Exists(tempFilePath))
            {
                bodyBuilder.Attachments.Add(tempFilePath);
                Console.WriteLine("PDF attached successfully.");
            }
            else
            {
                Console.WriteLine("Error: PDF file not found.");
                return StatusCode(500, new { message = "PDF file not found." });
            }

            message.Body = bodyBuilder.ToMessageBody();

            // Step 4: Send the email using MailKit
            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                Console.WriteLine("Connecting to SMTP server...");
                smtpClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                // Replace with your SMTP server and port

                Console.WriteLine("Authenticating...");
                smtpClient.Authenticate("mir.udemy2024@gmail.com", "kfyd euct svko cyeb"); // Replace with your credentials

                Console.WriteLine("Sending email...");
                await smtpClient.SendAsync(message);

                smtpClient.Disconnect(true);
                Console.WriteLine("Email sent successfully!");
            }

            return Ok(new { message = "PDF sent successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during email sending: {ex.Message}");
            return StatusCode(500, new { message = "Error sending email", error = ex.Message });
        }

    }






}