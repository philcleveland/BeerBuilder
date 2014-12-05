namespace BeerBuilder

    module Functions =
        open Domain
        open System
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

        //Mash pH
        //alk = CaCO3 ppm
        //ca = Ca ppm
        //mg = Mg ppm
        //note ppm = mg/L
        //recommended range 5.8 - 5.2
        let pH alk ca mg = 
            5.8 + (0.028 * ((alk * 0.056) - (ca * 0.04) - (mg * 0.033)))

        //the weight of water lbs/gal at 60F
        let weightOfWater = 8.3378

        //boilTime = in hours
        let evaporation boilTime =
            1.0 - (boilTime * 0.05) //5% evaporation/hour

        //amount of water lost in the spent grains
        let grainWaterRetention grainInWeight =
            let grainOut = grainInWeight * 0.4
            let waterWeight = 4.0 * grainOut //mult by 4 since slurry is est at 80% H20 20% spent grains
            waterWeight / weightOfWater

        let requiredWater batchSize trubLoss grainWeight waterMashRatio boilTime equipLoss =
            let finalBoil = batchSize + trubLoss
            let shrinkage = 1.0 - 0.04 //4% shrinkage during cooling
            let evap = evaporation boilTime
            let grainWaterLoss = grainWaterRetention grainWeight

            printfn "FB: %A SH: %A EV: %A EqL: %A GrL: %A" finalBoil shrinkage evap equipLoss grainWaterLoss

            finalBoil / shrinkage / evap + equipLoss + grainWaterLoss

        let convertQuartsToGal q =
            q / 4.0

        //convert specific gravity to gravity units
        let gravUnitConv sg =
            (sg - 1.0) * 1000.0

        //Gravity Units to Specific Gravity
        let GUtoSG gu = 
            (gu / 1000.0) + 1.0

        //Total Gravity
        //volume in gallons
        //gu = gravity units
        let totalGravity (gu:float) (volume:float) = 
            gu * volume
        
        //Gravity Units End
        //guBeg =gravity units beginning
        //volBeg= volume beginning
        //volEnd = volume ending
        let GUend guBeg volBeg volEnd =
            (guBeg * volBeg) / volEnd

        //Determines the amount of extract to add to hit target total gravity
        //tgTarget = Total gravity target in GU
        //tgMash = Measured Total gravity of Mash
        //extractGU = the gravity units contributed by the malt extract
        let Extractlbs (tgTarget:float)  (tgMash:float) (extractGU:float) =
            (tgTarget - tgMash) / extractGU

        //Calculates the final volume of wort given the mash gravity
        //and the target gravity units
        //tgMash = Measured Total gravity of Mash
        //targetGU = target Gravity Units desired
        let VolumeFinal (tgMash:float) (targetGU:float) = 
            tgMash / targetGU

        //Alpha Acid Units (AAU)
        //weight = in oz
        //alpha acid = percentage
        let AAU (weight:float) (alphaAcid:float) = 
            weight * alphaAcid

        //http://www.howtobrew.com/section1/chapter5-5.html
        //The numbers 1.65 and 0.00125 in f(G) were empirically derived to fit 
        //the boil gravity (Gb) analysis data. In the f(T) equation, the number 
        //-0.04 controls the shape of the utilization vs. time curve. The factor 
        //4.15 controls the maximum utilization value. This number may be 
        //adjusted to customize the curves to your own system. If you feel that 
        //you are having a very vigorous boil or generally get more utilization 
        //out of a given boil time for whatever reason, you can reduce the number 
        //a small amount to 4 or 3.9. Likewise if you think that you are getting 
        //less, then you can increase it by 1 or 2 tenths.
        //(boilGravity:float) Specific gravity of the wort 
        //(time:float) Length of time(minutes) hops are boiled
        let HopUtilization (boilGravity:float) (time:float) =
            let fGrav = 1.65 * 0.000125 ** (boilGravity - 1.0)
            let fTime = (1.0 - System.Math.E**(-0.04 * time)) /  4.15
            fGrav * fTime

        let IBU (aau:float) (utilization:float) (volume:float)=
            aau * utilization * 75. / volume

        