namespace BeerBuilder
    module Malts = 
        open FSharp.Data

        type MaltProvider = CsvProvider<".\Malts.csv">

//        let GetMalts() = 
//            let malts = MaltProvider.Load(".\Malts.csv")
//            let arrRows = [for row in malts.Rows -> row.Name]
//            malts.Rows |>
//                Seq.map(fun x -> printfn "%A %A %A %A %A" malts.Name malts.Type malts.PP malts.LowerL malts.UpperL)

