using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Packing.Mudblazor.Server.Data;
using Packing.Mudblazor.Shared;
using WebPush;

namespace Packing.Mudblazor.Server.Helpers
{
    public class NotificacionesService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        
        public NotificacionesService(IConfiguration configuration, ApplicationDbContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }

        public async Task EnviarNotificacionNuevoPedido(Pedido pedido)
        {
            var notificaciones = await _context.Notificaciones.ToListAsync();
            var llavePublica = _configuration.GetValue<string>("Notificaciones:publicKey");
            var llavePrivada = _configuration.GetValue<string>("Notificaciones:privateKey");
            var email = _configuration.GetValue<string>("Notificaciones:email");
            var vapidDetails = new VapidDetails(email,llavePublica,llavePrivada);
            
            foreach (var notificacion in notificaciones)
            {
                var pushSubscription = new PushSubscription(notificacion.URL, notificacion.P256dh,
                    notificacion.Auth);
                var webPushClient = new WebPushClient();

                try
                {
                    var payload = JsonSerializer.Serialize(new
                    {
                        
                    });
                    await webPushClient.SendNotificationAsync(pushSubscription,payload,vapidDetails);
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }
            }
        }
    }
}
