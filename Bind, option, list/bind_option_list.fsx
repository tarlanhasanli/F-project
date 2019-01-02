(*
   Question 1

   By now you should be familiar with map and bind functions of both
   Option and List.

   Option.map : ('a -> 'b) -> 'a option -> 'b option
   List.map   : ('a -> 'b) -> 'a list   -> 'b list

   Option.bind  : ('a -> 'b option) -> 'a option -> 'b option
   List.collect : ('a -> 'b list)   -> 'a list   -> 'b list

   Option.map can be used to apply an ordinary function ('a -> 'b) to
   an optional value to get an optional result.

   Option.bind can be used to apply a partial function ('a -> 'b option)
   to an optional value to get an optional result.

   Similar idea applies to List.map and List.collect.

   Your first task is to implement two functions that allow to apply a
   "funny" function to a "funny" value to get a "funny" result.
   More precisely, you must implement

   applyO : ('a -> 'b) option -> 'a option -> 'b option
   applyL : ('a -> 'b) list   -> 'a list   -> 'b list

   You should use the corresponding bind functions to implement these two.

   The result of applyO (Some id) (Some 3) must be Some 3.

   The result of applyL [f1;...;fm] [v1;...;vn] must be
     [f1 v1;...;f1 vn;
      ...
      fm v1;...;fm vn]
*)

let applyO (mf : ('a -> 'b) option) (ma : 'a option) : 'b option = 
  mf |> Option.bind (fun x -> ma |> Option.map x)

let applyL (mf : ('a -> 'b) list) (ma : 'a list ) : 'b list= 
  mf |> List.collect (fun x -> ma |> List.map x)

(*
   Whenever we have a value of type 'a we can view it as an optional
   value that "is there" or a non-deterministic value which happens to
   have exactly one possibility.
*)

let pureO a = Some a

let pureL a = [a]

// Here is a more compact infix notation for the two apply operations
let (<?>) mf ma = applyO mf ma
let (<*>) mf ma = applyL mf ma

(*
   Question 2

   Implement the function

   sequenceO : 'a option list -> 'a list option

   so that given a list xs of optional values it evaluates to
   * Some xs' when all of the optional values in xs were Some
     and xs' is a list of the values inside
   * None     when there was at least one None in xs.

   sequenceO [Some 1; Some 2] should evaluate to Some [1; 2]

   sequenceO [Some 1; None; Some 2] should evaluate to None

   You should implement this as a List.foldBack over the input list.
   (No need for rec or pattern matching.)
   
   Use <?> and pureO to keep your code compact.
*)

(*
   Question 3

   Implement the function

   sequenceL : 'a list list -> 'a list list

   which is an analogue of sequenceO (option has been replaced
   with list in the type signature).

   Try to do this by just copying your implementation of sequenceO and
   replacing pureO with pureL and <?> with <*>.

   If you implemented sequenceO in a good way then the result should
   typecheck and work as expected.

   sequenceL xs computes all possibilities to pick a single element
   from each of the lists in xs.

   sequenceL [[1; 2]; [3; 4]]

   should evaluate to

   [[1; 3]; [1; 4]; [2; 3]; [2; 4]]   
*)

(*
   Question 4

   As an example of a "non-deterministic" function we are going to
   consider rolling a die. Define a function 

   roll : int -> int list

   such that given an integer n it gives all the possible outcomes
   (in increasing order) of rolling an n-sided die. 

   roll 6 should evaluate to [1;2;3;4;5;6]
*)

(*
   Question 5

   Define a function

   rollTwo : int -> int -> (int * int) list

   such that given two integers m and n it gives all possible results
   of rolling an m-sided die and an n-sided die.

   rollTwo 2 3 should evaluate to
   [(1, 1); (1, 2); (1, 3);
    (2, 1); (2, 2); (2, 3)]

   The <*> operation may be useful here.
*)

(*
   Question 6

   Implement the function

   rollTwo' : int -> (int * int) list

   such that given an integer n you roll an n-sided die
   and based on the outcome x of the roll you then roll an x-sided die.

   rollTwo' 3 should evaluate to
   [(1, 1);
    (2, 1); (2, 2);
    (3, 1); (3, 2); (3, 3)]

   Note that here we have a situation where based on the outcome
   of the first roll we decide what to roll next. This is different
   from the previous question where the two rolls did not depend on
   each other.

   Remember that the type of List.collect is

   ('a -> 'b list) -> 'a list -> 'b list

   Here the function ('a -> 'b list) allows you to do something with
   each 'a in the input list and then collect the results.
*)

(*
   Question 7

   Implement the function

   rollN : int list -> int list list

   where the argument int list is a description of the dice you have
   and you must compute all possible outcomes of rolling the dice.
   More precisely,

   rollN [1;2;3]

   means that you are given a 1-sided die, a 2-sided die and a 3-sided die
   and you must compute the possible outcomes of rolling these.

   This is similar to rollTwo except that here we are more flexible in the
   number of dice. Hence the result is a list of lists and not tuples.

   The function sequenceL may be useful here.
*)

(*
   Question 8

   Implement the function

   conditional : ('a -> bool) -> ('a -> bool) -> 'a list -> (int * int)

   such that conditional f g xs evaluates to (m, n) so that thinking
   of (m, n) as a fraction m / n gives the ratio of elements
   satisfying the predicate f among those elements of xs that satisfy
   the predicate g.
*)

