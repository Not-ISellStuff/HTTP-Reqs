using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

// HTTP POST and GET Reqs In C#

class Code
{
    public void GetReq(HttpClient client)
    {
        var r = client.GetAsync("https://api.ipify.org?format=json").Result;
        var rawdata = r.Content.ReadAsStringAsync().Result;
        var data = JsonDocument.Parse(rawdata);

        // Response looks something like this --> {"ip": "127.0.0.1"}
        // Parse Json Data | Value of "ip"

        var ip = data.RootElement.GetProperty("ip").GetString();
        Console.WriteLine(ip);
    }

    public void PostReq(HttpClient client)
    {
        var webhook = "https://discord.com/api/webhooks/1311872810351722589/v6lZjIucMp6jHIQtKGcUEbLiksnnWZs71AnnPmeaJitYVvXG3rBA8wK6dnBLxSUuaaaw";
      
        // Setup the json payload

        var rawdata = new
        {
            content = "hi nigger"
        };
        var data = JsonConvert.SerializeObject(rawdata);
        var content = new StringContent(data, Encoding.UTF8, "application/json");

        // The Req

        var r = client.PostAsync(webhook, content).Result;

        // fyi you can't compare the status code with an int
        // so you can do if (r.StatusCode == 200) or shit like that

        if (r.StatusCode == System.Net.HttpStatusCode.NoContent) // btw NoContent = Code 204
        {
            Console.WriteLine("[+] Sent Request");
        } else
        {
            Console.WriteLine("[!] Failed To Send Request");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        var client = new HttpClient();
        var req = new Code();

        req.PostReq(client);
        req.GetReq(client);
    }
}
