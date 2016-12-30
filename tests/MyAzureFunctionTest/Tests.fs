module Tests

open Expecto
open MyAzureFunction

open System.Net
open System.Net.Http
open System.Web.Http
open System.Web.Http.Hosting
open Newtonsoft.Json



[<Tests>]
let tests =
  testList "MyAzureFunction" [
    testCase "Run - good message" <| fun _ ->
      let body = Newtonsoft.Json.JsonConvert.SerializeObject( {name = "Chris"})

      use ctn = new StringContent(body)
      use req = new HttpRequestMessage()
      req.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
      req.Content <- ctn

      let res = Run(req)
      let resBody = res.Content.ReadAsStringAsync().Result

      Expect.equal resBody "\"Hello Chris\"" ""



    testCase "Run - bad message" <| fun _ ->
      let result =
        try
          use ctn = new StringContent("")
          use req = new HttpRequestMessage()
          req.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
          req.Content <- ctn

          let res = Run(req)
          let resBody = res.Content.ReadAsStringAsync().Result
          true
        with
        | _ -> false

      Expect.equal result false ""
  ]