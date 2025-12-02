using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace ShopTARgv24.Hubs
{
    // Наследуемся от базового класса Hub
    public class ChatHub : Hub
    {
        // Метод, который будут вызывать клиенты
        public async Task SendMessage(string user, string message)
        {
            var timestamp = DateTime.Now.ToString("ddMMyy HH:mm");
            // Отправляем сообщение всем подключенным клиентам
            // "ReceiveMessage" — это имя метода, который мы будем слушать в JavaScript
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}