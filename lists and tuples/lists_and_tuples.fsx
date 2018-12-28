// 1. Create a type BibliographyItem that has the following structure:
// string list * string * (int * int) * int
// The meaning of the tuple elements is as follows:
// * The first field represents the list of author names where each name is in the format
//   "Lastname, Firstname1 Firstname2" (i.e. listing all first names after comma)
// * The second field represents the title of the publication
// * The third field represents a pair containing the starting page number and ending page number of the paper.
// * The fourth field represents the year of publication

type BibliographyItem = string list * string * (int * int) * int

// 2. Create a value bibliographyData : BibliographyItem list that contains
// at least 7 different publications on your favourite topic from https://dblp.uni-trier.de/ 
// Please note that you need not read the papers, just pick 7 papers that sound interesting to you from the database.

let bibliographyData = [(["Peter, Tolstrup Aagesen"; "Clint, Heyer"],"Personality of Interaction: Expressing Brand Personalities Through Interaction Aesthetics",(3126, 3130),2016);
    (["	Harikrishna, Veldandi"; "Shaik, Rafi Aahmed"],"Design procedure for multifinger MOSFET two-stage OTA with shallow trench isolation effect",(513, 522),2018);
    (["Marie, Kirstejn Aakjær"; "Eva, Brandt"],"Social innovation within prison service",(101, 104),2012);
    (["Peter, Edward Aackermann"; "Peter, Juhler Dinesen Pedersen"; "Allan, Peter Engsig-Karup"; "Thomas, Clausen"; "Jesper, Grooss"], "Development of a GPU-Accelerated Mike 21 Solver for Water Wave Dynamics", (129, 130), 2012);
    (["IJsbrand, Jan Aalbersberg"; "Pilar, Cos Alvarez"; "Julien, Jomier"; "Charles, Marion"; "Elena, V. Zudilova-Seinstra"], "Bringing 3D visualization into the online research article", (27, 37), 2014);
    (["Maria, Stella Iacobucci"; "Fabio, Graziosia"; "Panfilo, Ventresca"], "Quality of Service Performances in Ad Hoc IEEE 802.11 Wireless LANs", (160, 165), 2004);
    (["Michal, Gaziel Yablowitz"; "David, G. Schwartz"], "A Review and Assessment Framework for Mobile-Based Emergency Intervention Apps", (1, 32), 2018)] : BibliographyItem list

// 3. Make a function compareLists : string list -> string list -> int that takes two string lists and
// returns 
// * Less than zero in case the first list precedes the second in the sort order;
// * Zero in case the first list and second list occur at the same position in the sort order;
// * Greater than zero in case the first list follows the second list in the sort order;
// You are encouraged to use String.Compare to compare individual strings. If the first authors are the same
// then the precedence should be determined by the next author.
// A missing author can be considered to be equivalent to an empty string.
// Please note that your implementation should be recursive over the input lists.

let rec compareLists (first:string list) (second:string list) =
    match first, second with
    | [], [] -> 0
    | [], _  -> -1
    | _, []  -> 1
    | hf::tf, hs::ts -> match compare hf hs with
        | t when t > 0 -> 1
        | 0 -> compareLists tf ts
        | _ -> -1
        
// 4. Make a function
// compareAuthors : BibliographyItem -> BibliographyItem -> int
// that takes two instances of bibliography items and compares them according to the authors.
// Use solution from task 3.

let author (a,_,_,_) = a

let compareAuthors (x:BibliographyItem) (y:BibliographyItem) = 
    compareLists (author x) (author y)

// 5. Make a function
// compareAuthorsYears : BibliographyItem -> BibliographyItem -> int
// that takes two instances of bibliography items and compares them according to the authors 
// and if the authors are the same then according to years.

let year (_,_,_,y) = y

let compareAuthorsYears (x:BibliographyItem) (y:BibliographyItem) = 
    let res = compareAuthors x y 
    if res = 0 then 
        if year x < year y then
            -1
        else 
            if year x > year y then
                1
            else 
            0
    else res

// 6. Make a function 
// sortBibliographyByYear : BibliographyItem list -> BibliographyItem list
// That returns a bibliography sorted according to the year in ascending order

let sortBibliographyByYear (x:BibliographyItem list) =
    x |> List.sortBy(fun e -> year e)

// 7. Make a function 
// sortBibliographyByAuthorYear : BibliographyItem list -> BibliographyItem list
// That returns a bibliography sorted according to the authors and year in ascending order

let sortBibliographyByAuthor (x:BibliographyItem list) =
    x |> List.sortBy(fun e -> author e)

let sortBibliographyByAuthorYear (x:BibliographyItem list) =
    x |> sortBibliographyByYear |> sortBibliographyByAuthor

// 8. Make a function
// groupByAuthor : BibliographyItem list -> (string * BibliographyItem list) list
// where the return list contains pairs where the first element is the name of a single
// author and the second element a list of bibliography items that the author has co-authored.

let rec uniqueAuthor (x:BibliographyItem list) = 
    let authors = match x with
    | [] -> []
    | hd::tl -> (author hd)@(uniqueAuthor tl)
    authors |> Seq.distinct |> List.ofSeq // this part from https://bit.ly/2zFQkE0

let rec getAuthorBibliography (x:BibliographyItem list) (y:string) =
    x |> List.filter(fun e -> List.contains y (author e) = true)

let rec zip (xs:string list) (x:BibliographyItem list) =
   match xs with
   | [] -> []
   | hd :: tl -> (hd, (getAuthorBibliography x hd)) :: zip tl x
    
let groupByAuthor (x:BibliographyItem list) =
    let authors = uniqueAuthor x
    zip authors x