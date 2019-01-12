// Define a function
//
// elem : int -> 'a list -> 'a
//
// so that 'elem i xs' evaluates to the i-th element in the list xs.
//
// The function should throw an exception when i is less than zero or greater than
// the number of elements in xs. You should use 'failwith' to throw an exception.
//
// The function must be implemented via explicit recursion.
//
// Keep in mind that a list can either be empty ([]) or non-empty (x :: xs)
// and you need to define what is the i-th element of the list in both
// cases.
//
// When you have completed this definition then think about the time complexity
// of your implementation.

let elem (i:int) (xs:'a list) : 'a =
    if i < 0 then failwith "index cannot be less than 0"
    else
        let rec findElem (i:int) (xs:'a list) : 'a = 
            match i, xs with
            | 0, _  -> List.head xs
            | _, [] -> failwith "index out of bound"
            | _, _  -> findElem (i-1) (List.tail xs)
        findElem i xs

// Here is a type synonym for a tuple for representing information about artists.

type Artist = (string * int * bool * string)

let name ((x,_,_,_) : Artist) = x
let isAlive ((_,x,_,_) : Artist) = x
let year ((_,x,_,_) : Artist) = x
let country ((_,_,_,x) : Artist) = x


// The components of the tuple are:
// * name of the artist
// * year of birth
// * is the artist alive
// * country of birth


// Define a list
//
// artists : Artist list
//
// which contains information about your favourite artists.
// The list should contain at least five unique artists.
//
// You can use this list in the following exercises to test your code.

let artists = [("Eminem", 1972, true, "USA"); 
          ("Freddie Mercury", 1946, false, "Tanzania");
          ("The Weeknd", 1990, true, "Canada");
          ("50 Cent", 1975, true, "USA");
          ("Drake", 1986, true, "Canada")] : Artist list

// Define a function
//
// bornLaterThan : int -> Artist list -> Artist list
//
// so that 'bornLaterThan y xs' evaluates to
// all the artists x in xs satisfying the condition (birth year of x is greater than y)
// and nothing else.
//
// The function must preserve the relative ordering of elements in xs.
//
// The function must preserve duplicates.

let rec bornLaterThan (y:int) (xs:Artist list) : Artist list =
    match xs with
    | []                                -> []
    | head :: tail when (year head) > y -> head :: bornLaterThan y tail
    | _ :: tail                         -> bornLaterThan y tail

// Define a function
//                        
// artistsFrom : string -> Artist list -> Artist list
//                        
// so that 'artistsFrom c xs' evaluates to
// all the artists in xs who were born in c
// and nothing else.
//
// The function must preserve the relative ordering of elements in xs.
//
// The function must preserve duplicates.

let rec artistsFrom (c:string) (xs:Artist list) : Artist list =
    match xs with
    | []                                   -> []
    | head :: tail when (country head) = c -> head :: artistsFrom c tail
    | _::tail                              -> artistsFrom c tail

// Define a function
//                          
// names : Artist list -> string list
//                          
// so that 'names xs' evaluates to the names of the artists in xs.
//
// 'xs' and 'names xs' must have the same number of elements.
//
// The element at index i in 'names xs' must be the name of the
// artist at index i in 'xs'.

let rec names (xs:Artist list) : string list =
    match xs with
    | []           -> []
    | head :: tail -> name head :: names tail

// Using your solutions to previous exercises define a function
//    
// areFromAndBornLaterThan : string -> int -> Artist list -> string list
//    
// so that 'areFromAndBornLaterThan c y xs' evaluates to the names
// of the artists in xs who are from c and are born later than y.
//
// This should be a one-line definition.

let areFromAndBornLaterThan (c:string) (y:int) (xs:Artist list) : string list = 
    xs |> artistsFrom c |> bornLaterThan y |> names