type FsTree = Node of (string * FsTree) list

module FileSystem =

    let rec prepend (name:string) (path:string list list) : string list list =
        match path with
        | [] -> [[name]]
        | head :: tail -> (name :: head) :: prepend name tail

    let rec show (fs:FsTree) : string list list = 
        match fs with
        | Node [] -> []
        | Node ((name, tree) :: rest) -> prepend name (show tree) @ show (Node rest)

    let rec createDir (p : string list) (fs : FsTree) : FsTree = failwith "not implemented"     
    
    let rec delete (p : string list) (fs : FsTree) = failwith "not implemented"