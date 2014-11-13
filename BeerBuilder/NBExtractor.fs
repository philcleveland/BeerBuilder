namespace BeerBuilder

    module NBExtractor =
        open FSharp.Data

        let urlBase = "http://www.northernbrewer.com/shop/brewing/brewing-ingredients/maillard-malts-malted-grain?limit=all"

        let fetchProdTblAsync (node:HtmlNode) = async {
            let url = node.AttributeValue("href")
            let! doc = HtmlDocument.AsyncLoad(url)
            let sb = new System.Text.StringBuilder()

            doc.Descendants["title"] 
                |> Seq.head
                |> (fun x -> x.InnerText())
                |> sb.Append
                |> ignore

            let prodTbl = doc.Descendants["table"] 
                            |> Seq.find(fun x -> x.HasAttribute("id", "product-attribute-specs-table"))
            
            Seq.zip (prodTbl.Descendants["th"]) (prodTbl.Descendants["td"])
                |> Seq.iter (fun x-> 
                                let lbl,data = x
                                sb.Append(",") |> ignore
                                sb.Append(lbl.InnerText()) |> ignore
                                sb.Append(",") |> ignore
                                sb.Append(data.InnerText()) |> ignore)

            return sb.ToString()
        }

        let results = HtmlDocument.Load(urlBase)

        let links = results.Descendants["a"]
                    |> Seq.filter (fun x-> x.HasAttribute("class", "product-image"))

        let fetchMaltAttr() = links 
                                |> Seq.map fetchProdTblAsync 
                                |> Async.Parallel
                                |> Async.RunSynchronously