#r "System.Net.Http"
#r "System.Net.Http.Formatting"
#r "System.Web.Http"
#r "Newtonsoft.Json"

#if !COMPILED
#r "../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"
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
