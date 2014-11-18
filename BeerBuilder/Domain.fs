namespace BeerBuilder
    module Domain =
        ///Weight: in lbs
        ///Color: in Lovibond
        type Fermentable = {
            Weight : float
            Color : float
        }

        ///Name: Name of the ingrediant
        ///Type: Type of ingrediant. e.g. Grain or Adjunct
        ///PPG: Sugar points per gallon
        ///ColorLower: Lower end of ingrediant color in degrees Lovibond
        ///ColorUpper: Upper end of ingrediant color in degrees Lovibond
        type Ingredient = {
            Name : string
            Type : string
            PPG : float
            ColorLower : float
            ColorUpper : float
        }