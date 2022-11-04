using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WebSocketController : ControllerBase
    {
        private readonly IConfiguration? _configuration;
        public WebSocketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("/client")]
        public async Task Start()
        {
            await ClientSocket();
            
        }

        [HttpGet("/send")]
        public async Task Get()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await ServerSocket(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task ServerSocket(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            if (result != null)
            {
                while (!result.CloseStatus.HasValue)
                {
                    string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));
                    Console.WriteLine($"Client says: {msg}");

                    Console.Write("Server: ");
                    var msg1 = Console.ReadLine()?.ToString();
                    Console.WriteLine();

                    if (!string.IsNullOrEmpty(msg1))
                    {
                        ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg1));
                        await webSocket.SendAsync(bytes, result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
                    }

                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
                }
            }

            if (result != null && result.CloseStatus.HasValue)
            {
                await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, System.Threading.CancellationToken.None);
            }
        }

        private  async Task ClientSocket()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine()?.ToString();

            using (ClientWebSocket clientWebSocket = new ClientWebSocket())
            {
                string urival = "";
                if (_configuration != null)
                {
                    urival = _configuration["WebSocket:Path"];
                }
                Uri serviceUri = new Uri(urival);


                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(120));
                try
                {
                    await clientWebSocket.ConnectAsync(serviceUri, cts.Token);
                    while (clientWebSocket.State == WebSocketState.Open)
                    {
                        Console.Write("Client: ");
                        var msg = Console.ReadLine()?.ToString();
                        Console.WriteLine();
                        if (!string.IsNullOrEmpty(msg))
                        {
                            ArraySegment<byte> bytes = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                            await clientWebSocket.SendAsync(bytes, WebSocketMessageType.Text, true ,cts.Token);
                            var responseBuffer = new byte[1024];
                            var offset = 0;
                            var packetSize = 1024;  
                              ArraySegment<byte> recievedBytes = new ArraySegment<byte>(responseBuffer, offset, packetSize);
                                WebSocketReceiveResult response = await clientWebSocket.ReceiveAsync(recievedBytes, cts.Token);
                                var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                                Console.WriteLine("Server says: " + responseMessage);
                            if (response.EndOfMessage) ;
                              
                                    
                            
                        }
                    }
                }
                catch (WebSocketException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
          
        }
    }

}