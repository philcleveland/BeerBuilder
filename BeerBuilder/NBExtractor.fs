namespace BeerBuilder

    module NBExtractor =
        open FSharp.Data

        let results = HtmlDocument.Load("http://www.northernbrewer.com/shop/brewing/brewing-ingredients/maillard-malts-malted-grain?limit=all")
        let links = results.Descendants["a"]
                    |> Seq.filter (fun x-> x.HasAttribute("class", "product-image"))

        links |> Seq.iter (fun x-> printfn "%A" (x.AttributeValue "href"))
        let count = Seq.length(links)
