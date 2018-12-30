type Permission = Read | Write | Execute | Traverse

type FileSystem = Element list
and Element = | File of string * Permission Set
              | Dir of string * Permission Set * FileSystem

// helper functions
let compare (x:string) (y:string) : bool =
  match compare x y with
  | 0 -> true
  | _ -> false

let rec consistDir (fs:FileSystem) (dirName:string) : bool = 
  match fs with
  | Dir(dir, _, _)::_ when compare dir dirName -> true
  | _::tl -> consistDir tl dirName
  | [] -> false

let rec consistFile (fs:FileSystem) (fileName:string) : bool = 
  match fs with
  | File(file, _)::_ when compare file fileName -> true
  | _::tl -> consistFile tl fileName
  | [] -> false 

// 1. Define a function
// createEmptyFilesystem: unit -> FileSystem
// that will be a function with 0 arguments that will return an
// empty filesystem of the type that you defined.  
// (Permissions are initially assumed to be Read and Write and Traverse for
// the root directory, check task 5)  
// We assume that your file system is defined in a type called FileSystem.
// Please note that you will later be asked to extend the type 

let createEmptyFilesystem () : FileSystem = []

// 2. Define a function 
// createDirectory : string list -> FileSystem -> FileSystem
// that will return a new file system containing the directory
// specified by the string list into the file system passed as the
// second argument.
// For example, evaluating
// createDirectory ["Dir1"; "Dir2"] (createEmptyFileSystem ())
// should evaluate to a file system containing "Dir1" and in "Dir1" there should 
// be "Dir2".
// Please note that createDirectory is expected to create all directories in the path
// if they do not exist beforehand.
// (Permissions are initially assumed to be Read, Write and Traverse, check task 5) 

let rec createDirectory (path : string list) (fs : FileSystem) : FileSystem =
  match path with
    | [name] when not (consistDir fs name) ->
      Dir(name, set [Read; Write; Traverse], createEmptyFilesystem())::fs
    | main :: rest when (consistDir fs main) -> 
      match fs with
      | Dir(dirName, perm, fileSystem)::otherFs when compare dirName main ->
        if perm.Contains Traverse then
          if rest.Length = 1 then
            if perm.Contains Write then 
              Dir(dirName, perm, createDirectory rest fileSystem)::otherFs
            else
              failwith ("Directory " + dirName + " does not have Write permission")
          else 
            Dir(dirName, perm, createDirectory rest fileSystem)::otherFs
        else
          failwith ("Directory " + dirName + " does not have Traverse permission")
      | hd::otherFs -> hd::createDirectory path otherFs
      | [] -> [Dir(main, set [Read; Write; Traverse], createDirectory rest (createEmptyFilesystem()))]
    | main :: rest -> Dir(main, set [Read; Write; Traverse], createDirectory rest (createEmptyFilesystem()))::fs
    | []          -> fs

// createFile : string list -> FileSystem -> FileSystem
// that will create a file with the path given as the first argument in terms
// of a string list.
// createFile is expected to fail with exception (failwith) if the directory
// where the file is to be created does not exist.
// (Permissions are initially assumed to be Read and Write, check task 5) 

let rec createFile (path : string list) (fs : FileSystem) : FileSystem =
    match path with
    | [fileName] when not (consistFile fs fileName) ->
      File(fileName, set [Read; Write])::fs
    | main :: rest when (consistDir fs main) -> 
      match fs with
      | Dir(dirName, perm, fileSystem)::otherFs when compare dirName main ->
        if perm.Contains Traverse then
          if rest.Length = 1 then
            if perm.Contains Write then
              Dir(dirName, perm, createFile rest fileSystem)::otherFs
            else
              failwith ("Directory " + dirName + " does not have Write permission")
          else
            Dir(dirName, perm, createFile rest fileSystem)::otherFs
        else
          failwith ("Directory " + dirName + " does not have Traverse permission")
      | hd::otherFs -> hd::createFile path otherFs
      | [] -> failwith ("Could not find given " + path.ToString() + " in file system")
    | _ -> failwith ("Could not find given " + path.ToString() + " in file system")

// 3. Define a function
// countFiles : FileSystem -> int
// that will recursively count the number of files in the current filesystem.
// (countFiles should not honour permissions).

// 4. Define a function
// show : FileSystem -> string list list
// That will return a list of files and directories where
// each file and directory is represented as a string list.
// Please note that the grading of several further functions
// depends on the correctness of the show function.
// The show function is expected to output a path of each file and directory
// in the file system regardless of the permissions.

// 5. Define a function
// changePermissions : Permission set -> string list -> FileSystem -> FileSystem
// that will apply the specified set of permissions to the file or directory
// represented by a string list. For example, list ["Dir1";"Dir2";"File1"]
// represents a structure where "Dir1" is in the root directory, "Dir2" is
// in "Dir1" and "File1" is in "Dir2".
// changePermissions is assumed to work at super user level, i.e. it should be
// possible to change the permissions of every file and directory regardless of their
// previous permissions.

// 6. Modify the implementations of createDirectory and createFile to honor the
// permissions of the current file system level.
// Files can be created in a directory with Traverse and Write permissions. All of the
// above directories must at least have the Traverse permission.
// Directories can be created in a directory with Traverse and Write permissions. All of the
// above directories must at least have the Traverse permission.
// If the permissions do not allow creation of the item, the appropriate function should fail
// with an exception and an appropriate message (that you should formulate yourself).
// Hint: use the built-in failwith function.

// 7. Implement the function
// locate : string -> FileSystem -> string list list
// that will locate all files and directories with name matching the first argument
// of the function. The return value should be a list of paths to the files and
// directories. Each path is a list of strings indicating the parent directory
// structure.
// Note that the locate should honor the permissions, i.e. the files from
// directories without the Read permission should not be returned and
// directories without the Traverse permission should not be traversed further.

// 8. Implement the function:
// delete : string list -> FileSystem ->FileSystem
// that will delete a file or directory given as the first argument from a file
// system specified as the second argument.
// In case the item to be deleted is a directory, it needs to honor permissions
// and recursively only delete files with write permissions from directories with 
// write permissions (in addition to Traverse permissions).
// Subdirectories which will become empty need to be deleted as well.
//
// Note that the directory listing of a read only directory cannot be changed, i.e.
// if there is ["Dir1";"Dir2";"File1"] and Dir1 has only Read and Traverse
// permissions while Dir2 and File1 have all permissions, and one tries to delete
// Dir1, only File1 should be deleted, because deleting Dir2 would alter the 
// directory listing of a read only directory Dir1.

