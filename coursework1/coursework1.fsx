// 1. Associate an identifier "myFirstList" with an empty list of booleans.

let myFirstList = [] : bool list

// 2. Define a function
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

let rec elem i xs =
    if i < 0 then failwith "index cannot be less than 0"
    else 
        match xs with
        | []           -> failwith "index out of bound"
        | head :: tail -> 
            if i = 0 then head
            else elem (i - 1) tail

// Here is a type synonym for a tuple for representing information about artists.

type Artist = (string * int * bool * string)

// The components of the tuple are:
// * name of the artist
// * year of birth
// * is the artist alive
// * country of birth


// 3. Define a list
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

// 4. Define a function
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

let year (_,year,_,_) = year

let rec bornLaterThan y (xs:Artist list) =
    match xs with
        | []           -> []
        | head :: tail -> 
            if (year head) > y then
                head :: bornLaterThan y tail
            else
                bornLaterThan y tail

// 5. Define a function
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










// 6. Define a function
//                          
// names : Artist list -> string list
//                          
// so that 'names xs' evaluates to the names of the artists in xs.
//
// 'xs' and 'names xs' must have the same number of elements.
//
// The element at index i in 'names xs' must be the name of the
// artist at index i in 'xs'.











// 7. Using your solutions to previous exercises define a function
//    
// areFromAndBornLaterThan : string -> int -> Artist list -> string list
//    
// so that 'areFromAndBornLaterThan c y xs' evaluates to the names
// of the artists in xs who are from c and are born later than y.
//
// This should be a one-line definition.

