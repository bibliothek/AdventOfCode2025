module AdventOfCodeHelpers.Array2DHelper

open System
open System.Text

type Direction =
    | Up
    | Down
    | Left
    | Right
    | UpLeft
    | UpRight
    | DownLeft
    | DownRight

type Pos = { x: int; y: int }

let buildFromLinesAsInt (lines: string array) =
    Array2D.init lines.[0].Length lines.Length (fun x y -> Char.GetNumericValue lines.[y].[x] |> int)

let buildFromLines (lines: string array) =
    Array2D.init lines.[0].Length lines.Length (fun x y -> lines.[y].[x])

let toSeq<'A> (array: 'A[,]) =
    seq {
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                yield array.[x, y]
    }

let toSeqi<'A> (array: 'A[,]) =
    seq {
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                yield ((x, y), array.[x, y])
    }

let toSeqWithPoint<'A> (array: 'A[,]) =
    seq {
        for x in 0 .. Array2D.length1 array - 1 do
            for y in 0 .. Array2D.length2 array - 1 do
                yield ({ x = x; y = y }, array.[x, y])
    }

let getPrintableOverview map =
    let sb = StringBuilder()

    for y in 0 .. (map |> Array2D.length2) - 1 do
        for x in 0 .. (map |> Array2D.length1) - 1 do
            sb.Append(map.[x, y] |> string) |> ignore

        sb.Append Environment.NewLine |> ignore

    sb.ToString()

let getHorizontalAndVerticalNeighbours map pos =
    let x, y = pos

    [| (x + 1, y); (x - 1, y); (x, y + 1); (x, y - 1) |]
    |> Array.filter (fun (x, y) -> x >= 0 && x < Array2D.length1 map && y >= 0 && y < Array2D.length2 map)

let isInBounds map pos =
    let x = pos.x
    let y = pos.y

    x >= 0 && x < Array2D.length1 map && y >= 0 && y < Array2D.length2 map

let tryGetWithDirection map pos direction =
    let x = pos.x
    let y = pos.y

    match direction with
    | Up ->
        if y - 1 < 0 then
            None
        else
            Some(Array2D.get map (x) (y - 1), { x = x; y = y - 1 })
    | Down ->
        if y + 1 >= Array2D.length2 map then
            None
        else
            Some(Array2D.get map (x) (y + 1), { x = x; y = y + 1 })
    | Left ->
        if x - 1 < 0 then
            None
        else
            Some(Array2D.get map (x - 1) y, { x = x - 1; y = y })
    | Right ->
        if x + 1 >= Array2D.length1 map then
            None
        else
            Some(Array2D.get map (x + 1) y, { x = x + 1; y = y })
    | UpLeft ->
        if y - 1 < 0 || x - 1 < 0 then
            None
        else
            Some(Array2D.get map (x - 1) (y - 1), { x = x - 1; y = y - 1 })
    | UpRight ->
        if y - 1 < 0 || x + 1 >= Array2D.length1 map then
            None
        else
            Some(Array2D.get map (x + 1) (y - 1), { x = x + 1; y = y - 1 })
    | DownLeft ->
        if y + 1 >= Array2D.length2 map || x - 1 < 0 then
            None
        else
            Some(Array2D.get map (x - 1) (y + 1), { x = x - 1; y = y + 1 })
    | DownRight ->
        if y + 1 >= Array2D.length2 map || x + 1 >= Array2D.length1 map then
            None
        else
            Some(Array2D.get map (x + 1) (y + 1), { x = x + 1; y = y + 1 })

let tryGetLeft map pos = tryGetWithDirection map pos Left

let tryGetRight map pos = tryGetWithDirection map pos Right

let tryGetUp map pos = tryGetWithDirection map pos Up

let tryGetDown map pos = tryGetWithDirection map pos Down

let getNeighbours map pos =
    [ (tryGetLeft map pos)
      (tryGetRight map pos)
      (tryGetUp map pos)
      (tryGetDown map pos) ]
    |> List.filter Option.isSome
    |> List.map Option.get

let getDirection pos1 pos2 =
    if pos1.x > pos2.x then
        if pos1.y = pos2.y then Left
        elif pos1.y > pos2.y then UpLeft
        else DownLeft
    elif pos1.x < pos2.x then
        if pos1.y = pos2.y then Right
        elif pos1.y > pos2.y then UpRight
        else DownRight
    else if pos1.y > pos2.y then
        Up
    else
        Down

let getNeighboursIncludingDiagonal map pos =
    let up = (tryGetUp map pos)
    let down = (tryGetDown map pos)

    [ (tryGetLeft map pos)
      (tryGetRight map pos)
      up |> Option.bind (snd >> (tryGetLeft map))
      up |> Option.bind (snd >> (tryGetRight map))
      down |> Option.bind (snd >> (tryGetLeft map))
      down |> Option.bind (snd >> (tryGetRight map))
      up
      down ]
    |> List.filter Option.isSome
    |> List.map Option.get
