open System.Reflection

let getSolver (day, part) =
    let ``module`` = Assembly.GetExecutingAssembly().GetType($"AdventOfCode2025.Solver.Day{day}")
    ``module``.GetMethod($"solver{part}")

let getLines day = 
    System.IO.File.ReadAllLines $"../../../../../input/Day{day}.txt"

[<EntryPoint>]
let main args =
    let day = args.[0] |> int
    let part = args.[1] |> int
    printfn $"Solving for day %i{day} part %i{part}\n"
    let solver = getSolver (day, part)
    let result = solver.Invoke(null, [|(getLines day)|])
    printfn $"{result}"
    0
