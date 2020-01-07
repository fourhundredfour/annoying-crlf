open System.IO

let lines path = File.ReadAllText path

let replace (line: string) =
    line.Replace("\r\n", "\n")

let modifyText (file: string) =
    replace <| lines file

let writeToFile (file: string) =
    async {
        let fileName = Path.GetFileName file
        let writeInTempFile =
            use sw = new StreamWriter(Path.Join("/tmp/converted_files/", fileName))
            sw.Write (modifyText file)
            sw.Flush |> ignore
            sw.Close |> ignore
        writeInTempFile
        File.Move (Path.Join("/tmp/converted_files/", fileName), file, true)
    }

[<EntryPoint>]
let main argv =
    Array.map (fun x -> Directory.GetFiles("/data", x) |> Array.map (Path.GetFullPath >> writeToFile) |> Async.Parallel |> Async.RunSynchronously) argv |> ignore
    0