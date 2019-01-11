type FsTree = Node of (string * FsTree) list

module FileSystem =

    let rec prepend (name:string) (path:string list list) : string list list =
        match path with
        | [] -> [[name]]
        | head :: tail -> (name :: head) :: prepend name tail

    let rec contains (nodeName : string) (fs : FsTree) : bool =
        match fs with
        | Node [] -> false
        | Node ((name, tree) :: rest) when compare name nodeName = 0 -> true
        | Node (_ :: rest) -> contains nodeName (Node rest) 

    let treeToList (Node n) = n 

    let rec show (fs:FsTree) : string list list = 
        match fs with
        | Node [] -> []
        | Node ((name, tree) :: rest) -> prepend name (show tree) @ show (Node rest)

    let rec createDir (p : string list) (fs : FsTree) : FsTree = 
        match p, fs with
        | [], _ -> fs
        | [nodeName], _ when nodeName = null || nodeName = "" -> failwith "not Wellformed file name: empty or null" 
        | [nodeName], _ when contains nodeName fs -> fs
        | [nodeName], Node k -> Node (k @ [nodeName, Node []])
        | _, Node [] -> fs
        | hd::tl, Node ((name, tree) :: rest) when hd = name -> Node ((name, (createDir tl tree)) :: rest)
        | _, Node (k :: rest) -> Node (k :: treeToList (createDir p (Node rest)))
    
    let rec delete (p : string list) (fs : FsTree) = 
        match p, fs with
        | [], _ | _, Node [] -> fs
        | [nodeName], _ when contains nodeName fs = false -> fs
        | [nodeName], Node ((name, _)::rest) when nodeName = name -> Node rest
        | hd::tl, Node ((name, tree) :: rest) when hd = name -> Node ((name, (delete tl tree)) :: rest)
        | _, Node (k :: rest) -> Node (k :: treeToList (delete p (Node rest)))