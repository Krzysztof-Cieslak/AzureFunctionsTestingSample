#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing.Expecto

let buildDir  = "./build/"
let appReferences  = !! "/**/*.fsproj"
let testExecutables = !! "build/*Test.exe"

Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)

Target "Build" (fun _ ->
    MSBuildDebug buildDir "Build" appReferences
    |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    testExecutables
    |> Expecto (fun p ->
        { p with
            Debug = true
            Parallel = true
            // use only one of the following parameters

        })
)

"Clean"
  ==> "Build"
  ==> "Test"




RunTargetOrDefault "Test"
