namespace BeerBuilder

    module Functions =
    
        //ounce
        [<Measure>] type oz
        //minute
        [<Measure>] type min
        //gallon
        [<Measure>] type gal
        //lovibond
        [<Measure>] type L
        //pounds
        [<Measure>] type lbs

        //gc = grain color
        //weight = grain weight in lbs
        //vol = batch volume in gal
        //(gc:float) (weight:float)
        let mcu (arr:(float<L>*float<lbs>)[]) (vol:float<gal>) =
            let total =
                arr |> 
                    Seq.map(fun x-> 
                                let gc, weight = x
                                gc*weight) |>
                                Seq.sum
            total / vol

        
        let srm_color mcu:float<L lbs/gal> =
            1.4922 * (mcu**0.6859)

        //L = degrees lovibond
        let srm L = 
            1.35 * L - 0.6

        //european brewing convention
        let ebc srm =
            1.97 * srm

        let lovibond srm = 
            (srm + 0.6) / 1.35

        //og = original gravity
        //fg = final gravity
        let abv og fg = 
            (76.08 * (og - fg) / (1.775 - og)) * (fg / 0.794)

        //bt = boil time in minutes
    //    let ragerUtilization bt =
    //        match bt with
    //        | bt when bt <= 5 -> 5.0
    //        | bt when bt > 5 && bt <= 10 -> 6.0
    //        | bt when bt > 10 && bt <= 15 -> 8.0
    //        | bt when bt > 15 && bt <= 20 -> 10.1
    //        | bt when bt > 20 && bt <= 25 -> 12.1
    //        | bt when bt > 25 && bt <= 30 -> 15.3
    //        | bt when bt > 30 && bt <= 35 -> 18.8
    //        | bt when bt > 35 && bt <= 40 -> 22.8
    //        | bt when bt > 40 && bt <= 45 -> 26.9
    //        | _-> 0.0
    
    

        ///wh = weight of hops (oz)
        //aa = alpha acid percentage
        //uaa = quatity of alpha used during boiling process
        //vw = volume of wort in gallons
        //1.34 = adjustment for U.S customary units
        let ibu (wh:float) aa uaa vw:float =
            (wh * aa * uaa) / (vw * 1.34)

        ///wh = weight of hops (oz)
        //aa = alpha acid percentage
        let aau wh aa =
            wh * aa