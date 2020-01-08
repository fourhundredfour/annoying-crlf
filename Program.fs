open System.IO

let lines path = seq { yield! File.ReadLines path }

let modifyText (file: string) =
    Seq.map (fun x -> x + "\n") (lines file)

let writeToFile (file: string) =
    async {
        let fileName = Path.GetFileName file
        let writeInTempFile () =
            use sw = new StreamWriter(Path.Join("/tmp/converted_files/", fileName))
            modifyText file |> Seq.iter sw.Write
        writeInTempFile()
        File.Move (Path.Join("/tmp/converted_files/", fileName), file, true)
    }

[<EntryPoint>]
let main argv =
    argv
    |> Array.map (fun x ->
        Directory.GetFiles("/data", x)
        |> Array.map (Path.GetFullPath >> writeToFile)
        |> Async.Parallel
        |> Async.RunSynchronously)
    |> ignore
    0