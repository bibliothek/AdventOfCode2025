module AdventOfCode2025.Tests.Day1Tests

open AdventOfCode2025.Solver
open Xunit

type Day1Test() =
    let demoData =
        [|
            "L68"
            "L30"
            "R48"
            "L5"
            "R60"
            "L55"
            "L1"
            "L99"
            "R14"
            "L82"
        |]

    [<Fact>]
    let ``Day 1 part 1`` () =
        let solution = Day1.solver1 demoData
        Assert.Equal(3, solution)

    [<Fact>]
    let ``Day 1 part 2`` () =
        let solution = Day1.solver2 demoData
        Assert.Equal(6, solution)


