using Ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Ecommerce.Middleware
{
    public class FirstMiddleware : IMiddleware
       
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(context.Request.Path != "/api/User/GetAll")
            {

                await next(context);

            }
            else
            {
                var originBody = context.Response.Body;
                var memStream = new MemoryStream();
                context.Response.Body = memStream;
                await next(context);
                memStream.Position = 0;
                var response = new StreamReader(memStream).ReadToEnd();
                
                
                var json = JsonConvert.DeserializeObject<List<SignUpModel>>(response);
                if (json != null)
                {
                    foreach (var item in json)
                    {
                        if (item != null)
                        {
                            if (item.FirstName == "Asad")
                            {
                                item.FirstName = "zaheer";
                            }
                        }
                    }

         
                }
                var jsons = JsonConvert.SerializeObject(json);
                var memoryStreamModified = new MemoryStream();
                var sw = new StreamWriter(memoryStreamModified);
                sw.Write(jsons);
                sw.Flush();
                memoryStreamModified.Position = 0;

                await memoryStreamModified.CopyToAsync(originBody).ConfigureAwait(false);



                context.Response.Body = originBody;

            }
                }
                    }
                }

            
            
        
    

