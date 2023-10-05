open System.IO

let modifyText (file: string) =
    Seq.map (fun x -> x + "\n") (File.ReadLines file)

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
    let options, patterns =
        if argv.Length > 0 && argv[0] = "-r" then
            EnumerationOptions(RecurseSubdirectories=true), argv[1..]
        else
            EnumerationOptions(), argv

    patterns
    |> Array.map (fun x ->
        Directory.GetFiles("/data", x, options)
        |> Array.map (Path.GetFullPath >> writeToFile)
        |> Async.Parallel
        |> Async.RunSynchronously)
    |> ignore
    0