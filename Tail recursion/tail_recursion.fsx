(*
  Task 1:

  Write a function
  satisfiesPInList : (float -> float -> bool) -> (float * float) list -> float * float
  that returns the first element in list passed as the second argument of the
  function that satsifies the predicate passed as the first argument. The first
  element in the pair is passed as the first argument and the second as the second.
  Make sure your implementation uses explicit tail recursion.
*)

(*
  Task 2:

  Write a function
  createPairsOfList : 'a -> 'a list -> ('a * 'a) list
  that takes a list of 'a-s and returns a list of pairs of 'a-s that are taken
  sequentially from the list passed as the second argument to the function.
  In case the list has odd number of elements make the first argument of the
  function be the second element in the pair. 
  Make sure your implementation uses explicit tail recursion.
*)

(*
  Task 3:

  Write a function
  createPairsOfListFold : 'a -> 'a list -> ('a * 'a) list
  that takes a list of 'a-s and returns a list of pairs of 'a-s that are taken
  sequentially from the list passed as the second argument to the function. 
  In case the list has odd number of elements make the first argument of the
  function be the second element in the pair. 
  Make sure your implementation uses List.fold or List.foldBack appropriately.
  Test yourself if this implementation appears to be tail recursive.
*)
  
(*
  Task 4:

  Below you find the definition of a type Tree of leaf-labeled trees. Write a
  function medianInTree : float Tree -> float that returns the median label in the
  given tree, i.e. the difference in counts of elements to the right and left is
  either 0 or the count of elements to the right is exactly 1 greater than the
  count of elements to the left.
  Use continuation-passing style in your implementation.
*)

