// A list can contain items of the same type.
// We use a Cell to collect things that can be slightly different.

type Cell = Empty | Value of int | Pair of (int * int)

// 1. Define a function
//
// noEmptyCells : Cell list -> Cell list
//
// that discards all cells that are Empty.
//
// Use List.filter


let noEmptyCells x = 
    x |> List.filter(fun e -> e <> Empty)

// 2. Define a function
//
// filterGreaterThan : int -> Cell list -> Cell list
//
// that discards all cells with value less than or equal to n.
// This means that all Empty cells should be discarded,
// Value v should be discarded when v is less than or equal to n,
// Pair (x, y) should be discarded when both x and y are less than or equal to n.
//
// Use List.filter


let filterGreaterThan n y = 
    y |> noEmptyCells |> List.filter(fun e ->
     match e with 
    | Value v when v > n              -> true
    | Pair (x, y) when x > n || y > n -> true
    | _                               -> false)

// 3. Define a function
//
// increaseCells : int -> Cell list -> Cell list
//
// that increases the values in the cells by the given amount.
// Empty should stay Empty and for Pairs you should increase
// both of the values in the pair.
//
// Use List.map

let increaseCells n y = 
    y |> List.map(fun e -> 
    match e with 
    | Value v     -> Value (v + n)
    | Pair (x, z) -> Pair (x + n, z + n)
    | Empty       -> Empty)

// 4. Define a function
//
// transformPairs : (int -> int -> Cell) -> Cell list -> Cell list
//
// that replaces the Pair cells in the list
// with the result of applying the given operation to the two integer values in the pair.
//
// 'transformPairs f xs' should replace a 'Pair (x, y)' with 'f x y'.
//
// Use List.map

let transformPairs f y =
    y |> List.map(fun e -> 
    match e with
    | Pair (x, y) -> f x y
    | x           -> x) 

// 5. Define a function
//
// pairsToEmpty : Cell list -> Cell list
//
// that replaces all Pairs with Empty cells.


let pairsToEmpty y = 
    y |> List.map(fun e -> 
    match e with
    | Pair _ -> Empty
    | x      -> x) 

// 6. Define a function
//
// replicateCells : Cell list -> Cell list
//
// that replicates each cell in the list n times, where n is
// 0             for Empty
// max 0 v       for Value v
// max 0 (x + y) for Pair (x, y)
//
// Use List.collect

let replicateCells y =
    y |> List.collect(fun e -> 
    match e with
    | Empty       -> []
    | Value v     -> [for _ in 1 .. (max 0 v) -> Value v]
    | Pair (x, y) -> [for _ in 1 .. (max 0 (x+y)) -> Pair(x, y)])

// 7. Define a function
//
// flattenPairs : Cell list -> Cell list
//
// that replaces a Pair (x, y) with Value x followed by Value y.
//
// Use List.collect.
    
let flattenPairs y =
    y |> List.collect(fun e ->
    match e with
    | Pair(x, y) -> [Value x; Value y]
    | k          -> [k])

// 8. Define a function
//
// countCells : Cell list -> int * int * int
//
// which counts the number of Empty, Value, and Pair cells in the list.
//
// If 'countCells xs' is (1, 2, 3) then
// 1 is the number of Empty cells
// 2 is the number of Value cells
// 3 is the number of Pair  cells
//
// Use List.fold
    

// 9. Define a function
//
// cellsToString : Cell list -> string
//
// that constructs a string representation of the cells.
//
// Empty       is represented as "."
// Value v     is represented as "v"
// Pair (x, y) is represented as "x,y"
//
// Use "|" as the separator symbol.
// The result must start and end with the separator.
// You can convert an int to a string by the function 'string'.
//
// Use List.fold
