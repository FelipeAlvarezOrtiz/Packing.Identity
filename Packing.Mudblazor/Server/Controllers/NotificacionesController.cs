using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Packing.Mudblazor.Server.Data;
using Packing.Mudblazor.Shared;

namespace Packing.Mudblazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private MailjetClient _mailCliente;

        public NotificacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("suscribir"), Authorize]
        public async Task<ActionResult> Suscribir(Notificacion notificacion)
        {
            await _context.AddAsync(notificacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("desuscribir"), Authorize]
        public async Task<ActionResult> Desuscribir(Notificacion notificacion)
        {
            var notificacionDb =
                _context.Notificaciones.FirstOrDefaultAsync(x =>
                    x.Auth == notificacion.Auth && x.P256dh == notificacion.P256dh);
            if (notificacionDb is null) return NotFound();
            _context.Remove(notificacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("enviarCorreo/nuevoPedido"),AllowAnonymous]
        public async Task<ActionResult> EnviarCorreoPedido()
        {
            _mailCliente = new MailjetClient("f01ec3e7df5cd34adbfc9c5aab252378", "5e951f5e900a4ac02868aa91ec70d251");
            var request = new MailjetRequest()
            {
                Resource = Send.Resource,
            }.Property(Send.Messages,new JArray
            {
                new JObject
                {
                    { "From", new JObject
                    {
                        {"Email", "me@felipealvarez.dev"},
                        {"Name", "Notificaciones"}
                    } },
                    { "To", new JArray
                        {
                            new JObject
                            {
                                {"Email","falvarezortiz@hotmail.com" },
                                {"Name", "Maddalena Bertolla" }
                            }
                        }
                    },
                    { "Subject", "Pruebas de notificaciones" },
                    { "TextPart","Este es un mensaje de prueba" },
                    { "HTMLPart", "<h3> Este es un mensaje de prueba con HTML</h3>" }
                }
            });
            var response = await _mailCliente.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                return BadRequest();
            }
        }
    }
}
