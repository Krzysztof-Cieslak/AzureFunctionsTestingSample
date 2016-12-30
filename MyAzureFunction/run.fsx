#if !TEST
#r "System.Net.Http"
#r "System.Net.Http.Formatting"
#r "System.Web.Http"
#r "Newtonsoft.Json"

#if !COMPILED
#r "../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
#r "../packages/Microsoft.AspNet.WebApi.Client/lib/net45/System.Net.Http.Formatting.dll"
#r "../packages/Microsoft.AspNet.WebApi.Core/lib/net45/System.Web.Http.dll"
#endif

#else
module MyAzureFunction
#endif

open System.Net
open System.Net.Http
open Newtonsoft.Json

type Message = {
    name : string
}

let Run(req: HttpRequestMessage) =
    async {
        let! ctn = req.Content.ReadAsStringAsync() |> Async.AwaitTask
        let msg = JsonConvert.DeserializeObject<Message> ctn
        let output = sprintf "Hello %s" msg.name
        return req.CreateResponse(HttpStatusCode.OK, output);

    } |> Async.RunSynchronously
