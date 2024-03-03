
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ZoneFranche.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmailController : ControllerBase
    {
        [HttpGet("{targetEmail}/{type}")]
        public IActionResult sendEmail(string targetEmail, string type)
        {
            var email = new MimeMessage();
            Random random = new Random();
            email.From.Add(new MailboxAddress("Sender Name", "sender@email.com"));
            email.To.Add(new MailboxAddress("Receiver Name", targetEmail));

            email.Subject = "Réponse à la demande d'accès à la zone franche de Zarzis";

            if (type == "Refusée")
            {
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<b>Cher Mr/Madame,\r\n\r\nNous vous remercions d'avoir soumis votre demande d'accès au centre. Après avoir examiné attentivement votre demande, nous regrettons de vous informer que celle-ci n'a pas été approuvée.\r\n\r\nCordialement,\r\n\r\nL'équipe du parc d'activité économique de Zazris</b>"
                };
            }
            else if(type == "Acceptée")
            {
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = "<b>Cher Mr/Madame,\r\n\r\nNous sommes ravis de vous informer que votre demande d'accès au centre a été acceptée avec succès. Nous sommes impatients de vous accueillir dans nos locaux et de vous offrir l'expérience que vous méritez.\r\n\r\nVotre demande sera valable pour une période de 15 jours seulement,\r\n\r\n votre code secret est: "+ random.Next(100000, 1000000) +". Nous sommes impatients de vous voir bientôt.\r\n\r\nCordialement,\r\n\r\nL'équipe du parc d'activité économique de Zazris</b>"
                };

            }

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("meryamsouissi2003@gmail.com", "dnmz adbz foaj tjlw");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return Ok(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("emp/{targetEmail}/{password}")]
        public IActionResult sendEmployeeEmail(string targetEmail, string password)
        {
            var email = new MimeMessage();
            Random random = new Random();
            email.From.Add(new MailboxAddress("Sender Name", "sender@email.com"));
            email.To.Add(new MailboxAddress("Receiver Name", targetEmail));

            email.Subject = "Votre information de login comme etant un nouveau employee";

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Bienvenue cher employee, votre compte à éte ajouter avec success. \r votre email est :" + targetEmail + " et votre mot de passe est : " + password
            };

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    smtp.Authenticate("meryamsouissi2003@gmail.com", "dnmz adbz foaj tjlw");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return Ok(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
    
}

