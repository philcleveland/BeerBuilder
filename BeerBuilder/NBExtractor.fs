namespace BeerBuilder

    module NBExtractor =
        open FSharp.Data

        let urlBase = "http://www.northernbrewer.com/shop/brewing/brewing-ingredients/maillard-malts-malted-grain?limit=all"

        let fetchProdTblAsync (node:HtmlNode) = async {
            let url = node.AttributeValue("href")
            let! doc = HtmlDocument.AsyncLoad(url)
            let prodTbl = doc.Descendants["table"] 
                            |> Seq.find(fun x -> x.HasAttribute("id", "product-attribute-specs-table"))
            
            Seq.zip (prodTbl.Descendants["th"]) (prodTbl.Descendants["td"])
                |> Seq.iter (fun x-> 
                        let lbl,data = x
                        printfn "%s      | %s" (lbl.InnerText()) (data.InnerText()))
        }

        let results = HtmlDocument.Load(urlBase)

        let links = results.Descendants["a"]
                    |> Seq.filter (fun x-> x.HasAttribute("class", "product-image"))

        let maltAttrTbls() = links 
                                |> Seq.map fetchProdTblAsync 
                                |> Async.Parallel
                                |> Async.RunSynchronously