module AdventOfCodeHelpers.MathHelper

let rec gcd a b =
    match (a, b) with
    | (x, y) when x = y -> x
    | (x, y) when x > y -> gcd (x - y) y
    | (x, y) -> gcd x (y - x)

let lcm (a:int64) b = a * b / (gcd a b)