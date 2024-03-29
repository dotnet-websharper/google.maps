// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2024 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
// Helper notation for defining interfaces.
namespace WebSharper.Google.Maps.Definition

module Notation =

    open WebSharper.JavaScript
    open WebSharper.JavaScript.Dom
    open WebSharper.JavaScript.WebGL
    open WebSharper.InterfaceGenerator

    let Node = T<Node>
    let Document = T<Document>
    let Element = T<Element>
    let HTMLElement = T<HTMLElement>
    let SVGElement = T<SVGImageElement>
    let URL = T<URL>
    let Object = T<WebSharper.JavaScript.Object>
    let Error = T<Error>
    let Date = T<Date>
    let Promise = T<Promise<_>>
    let MouseEvent = T<MouseEvent>
    let KeyboardEvent = T<KeyboardEvent>
    let JsEvent = T<Event>
    let AddEventListenerOptions = T<AddEventListenerOptions>
    let EventListenerOptions = T<EventListenerOptions>
    // let EventListener = T<EventListener>
    // let EventListenerObject = T<EventListenerObject>
    let Float32Array = T<Float32Array>
    //TODO: confirm this one
    // let WebGLRenderingContext = T<WebGLRenderingContext>
    let WebGLRenderingContext = T<RenderingContext>

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

    /// Defines a configuration object.
    let Config name =
        Class name
        |+> Static [ Ctor [] |> WithInline "{}" ]
