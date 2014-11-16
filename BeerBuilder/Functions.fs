namespace BeerBuilder

    module Functions =
        open Domain
            
        let srmColorLookup =
            [|
                (1,0xffe699);(2,0xffd878);(3,0xffca5a);(4,0xffbf42);(5,0xfbb123);(6,0xf8a600);(7,0xf39300);(8,0xea8f00);(9,0xe58500);(10,0xde7c00);
                (11,0xd77200);(12,0xcf6900);(13,0xcb6200);(14,0xc35900);(15,0xbb5100);(16,0xb54c00);(17,0xb04500);(18,0xa63e00);(19,0xa13700);(20,0x9b3200);
                (21,0x952d00);(22,0x8e2900);(23,0x882300);(24,0x821e00);(25,0x7b1a00);(26,0x771900);(27,0x701400);(28,0x6a0e00);(29,0x660d00);(30,0x5e0b00);
                (31,0x5a0a02);(32,0x600903);(33,0x520907);(34,0x4c0505);(35,0x470606);(36,0x440607);(37,0x3f0708);(38,0x3b0607);(39,0x3a070b);(40,0x36080a)
            |] |> Map.ofSeq

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
        let mcu (arr:Fermentable[]) (vol:float) =
            let total =
                arr |> 
                    Seq.map(fun x-> 
                                let gc, weight = x.Color, x.Weight
                                gc*weight) |>
                                Seq.sum
            total / vol

        
        let srm_color mcu:float =
            1.4922 * (mcu**0.6859)

        let GetSRMDisplayColor (arr:Fermentable[]) (vol:float) =
            mcu arr vol |>
                srm_color |>
                    int |>
                        srmColorLookup.TryFind

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

                        