type FsTree = Node of (string * FsTree) list

module FileSystem =

    let rec show (fs:FsTree) : string list list = failwith "not implemented"

    let rec createDir (p : string list) (fs : FsTree) : FsTree = failwith "not implemented"     
    
    let rec delete (p : string list) (fs : FsTree) = failwith "not implemented"