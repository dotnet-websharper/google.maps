/// Helper notation for defining interfaces.
module IntelliFactory.WebSharper.Google.Maps.Notation

open IntelliFactory.WebSharper.Dom
open IntelliFactory.WebSharper.InterfaceGenerator

let Node = T<Node>
let Document = T<Document>
let Element = T<Element>

/// Defines a method type given the return type and parameter specifications.
let Fun (ret: Type.IType) (ps: seq<Type.IParameter>) : Type.IType =
    let ps =
        (Type.Parameters.Empty, ps)
        ||> Seq.fold (fun x y -> x * y)
    (ps ^-> ret) :> _

/// Defines a constructor given the parameter specifications.
let Ctor (ps: seq<Type.IParameter>) : CodeModel.Constructor =
    let ps =
        (Type.Parameters.Empty, ps)
        ||> Seq.fold (fun x y -> x * y)
    Constructor ps

