(*
  Write a function
  satisfiesPInList : (float -> float -> bool) -> (float * float) list -> float * float
  that returns the first element in list passed as the second argument of the
  function that satsifies the predicate passed as the first argument. The first
  element in the pair is passed as the first argument and the second as the second.
  Make sure your implementation uses explicit tail recursion.
*)

let rec satisfiesPInList xs x = 
  match x with
  | (a, b)::_ when xs a b -> a, b
  | _::tl -> satisfiesPInList xs tl 
  | [] -> failwith "no match"

(*
  Write a function
  createPairsOfList : 'a -> 'a list -> ('a * 'a) list
  that takes a list of 'a-s and returns a list of pairs of 'a-s that are taken
  sequentially from the list passed as the second argument to the function.
  In case the list has odd number of elements make the first argument of the
  function be the second element in the pair. 
  Make sure your implementation uses explicit tail recursion.
*)

let createPairsOfList (x:'a) (y:'a list) : ('a * 'a) list =
  let rec dummy list pairList = 
    match list with
    | [] -> pairList
    | [hd] -> dummy [] ((hd, x)::pairList)
    | hd::hd2::tl -> dummy tl ((hd, hd2)::pairList)
  (dummy y []) |> List.rev

(*
  Write a function
  createPairsOfListFold : 'a -> 'a list -> ('a * 'a) list
  that takes a list of 'a-s and returns a list of pairs of 'a-s that are taken
  sequentially from the list passed as the second argument to the function. 
  In case the list has odd number of elements make the first argument of the
  function be the second element in the pair. 
  Make sure your implementation uses List.fold or List.foldBack appropriately.
  Test yourself if this implementation appears to be tail recursive.
*)

let createPairsOfListFold x y =
  let i = List.length y - 1
  let a, b = List.fold(fun ((a, k), acc) elem -> 
    match k with
    | Some t -> ((a+2, None), (t, elem)::acc)
    | None when i = a -> ((a+2, None), (elem, x)::acc)
    | None -> ((a, Some elem), acc) ) ((0, None), []) y
  b |> List.rev
  
(*
  Below you find the definition of a type Tree of leaf-labeled trees. Write a
  function medianInTree : float Tree -> float that returns the median label in the
  given tree, i.e. the difference in counts of elements to the right and left is
  either 0 or the count of elements to the right is exactly 1 greater than the
  count of elements to the left.
  Use continuation-passing style in your implementation.
*)

type 'a Tree =
  | Leaf   of 'a
  | Branch of 'a Tree * 'a Tree

let medianInTree x = 
  let rec inOrder x c = 
    match x with
    | Leaf a -> c [a]
    | Branch (tl, tr) -> inOrder tl (fun vl -> vl@(inOrder tr (fun vr -> c vr))) 

  let midList list = 
    let mid = int (floor (float (List.length list - 1) / 2.))
    list.[mid]

  midList(inOrder x id)
