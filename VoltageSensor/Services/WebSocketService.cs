using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VoltageSensor.Models.VoltageSensor;

namespace VoltageSensor.Services
{

    public class WebSocketService
    {
        private readonly SensorService _sensor;

        public WebSocketService(SensorService sensor)
        {
            _sensor = sensor;
        }

        private async Task WebSocketLoop(HttpContext context, WebSocket webSocket)
        {
            //Int32 bufferSize = Marshal.SizeOf<RawSensor>();

            using IMemoryOwner<byte> buffer = MemoryPool<byte>.Shared.Rent(1024 * 4);
            
            ValueWebSocketReceiveResult request = await webSocket.ReceiveAsync(buffer.Memory, CancellationToken.None);

            while (webSocket.State == WebSocketState.Open) {
                try
                {
                    switch (request.MessageType)
                    {
                        case WebSocketMessageType.Text:

                            string inComeString = System.Text.Encoding.UTF8.GetString(buffer.Memory.Span);
                            RawSensor inComeDocument = JsonConvert.DeserializeObject<RawSensor>(inComeString);

                            
                            _sensor.Create(new Sensor(inComeDocument));
                            break;
                        default:
                            break;
                    }

                } catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"{e.Message}\r\n{e.StackTrace}");
                }
                request = await webSocket.ReceiveAsync(buffer.Memory, CancellationToken.None);
            }

            //while (!result.CloseStatus.HasValue)
            //{
            //    try
            //    {
            //        string string_buffer = System.Text.Encoding.UTF8.GetString(buffer);
            //        Sensor s = System.Text.Json.JsonSerializer.Deserialize<Sensor>(string_buffer);
            //        _sensor.Create(s);
            //    }
            //    catch (Exception e)
            //    {
            //        System.Diagnostics.Debug.WriteLine(e.Message);
            //    }


            //    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //}

            //await webSocket.CloseAsync(request.CloseStatus.Value, request.CloseStatusDescription, CancellationToken.None);
        }

        public async Task ProcessWebsocketSession(HttpContext context, Func<Task> next)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                    System.Diagnostics.Debug.WriteLine("Client connected");

                    await WebSocketLoop(context, webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            }
            else
            {
                await next();
            }

        }
    }
}
