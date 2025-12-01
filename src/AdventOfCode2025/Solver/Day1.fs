module AdventOfCode2025.Solver.Day1

let solver1 (lines: string array) =
    lines
    |> Array.fold
        (fun (pos, count) el ->
            let direction = if el.StartsWith "R" then 1 else -1
            let steps = el.Substring 1 |> int
            let newPosition = pos + direction * steps

            if newPosition % 100 = 0 then
                (newPosition, count + 1)
            else
                (newPosition, count))
        (50, 0)
    |> snd

let solver2 (lines: string array) =
    lines
    |> Array.fold
        (fun (pos, count) el ->
            let direction = if el.StartsWith "R" then 1 else -1
            let steps = el.Substring 1 |> int
            let newPosition = pos + direction * steps

            let sequence =
                if direction = 1 then
                    seq { for i in (pos + (1))..newPosition -> i }
                else
                    seq { for i in newPosition..(pos - (1))-> i }

            let zeroes =
                sequence
                |> Seq.filter (fun el -> el % 100 = 0)
                |> Seq.length

            (newPosition, count + zeroes)
        )
        (50, 0)
    |> snd
