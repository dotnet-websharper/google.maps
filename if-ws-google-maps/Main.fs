namespace IntelliFactory.WebSharper.Google

module Main =
    open IntelliFactory.WebSharper.InterfaceGenerator
    do Compiler.Compile stdout MapsSpecification.Assembly

