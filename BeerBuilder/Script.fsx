// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
#load "NBExtractor.fs"

open BeerBuilder
open Functions
open NBExtractor

// Define your library scripting code here
let calcVal = srm 2.4
let lovi = lovibond 2.64

let abvol = abv 1.085 1.01

let ibuCalc = ibu 1.0 10. 0.2697 7.0

let extractedData = extract()
