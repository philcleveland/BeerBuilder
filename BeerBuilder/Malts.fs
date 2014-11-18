namespace BeerBuilder
    module Malts = 
        open FSharp.Data
        open Domain

//        type MaltProvider = CsvProvider<"Malts.csv">

        let GetMalts(stream:System.IO.Stream) = 
            let malts = CsvFile.Load(stream:System.IO.Stream)
            
//            let malts = MaltProvider.Load("Malts.csv")
            [for row in malts.Rows -> row] |>
                Seq.map(fun row -> { Name = row.["Name"]; Type = row.["Type"]; PPG=(float)row.["PP"]; ColorLower=(float)row.["LowerL"]; ColorUpper=(float)row.["UpperL"]})
//                Seq.map(fun row -> printfn "%A %A %A %A %A" row.Name row.Type row.PP row.LowerL row.UpperL)

