namespace FSharp

open System

type Sex =
    | Male
    | Female

type Customer =
    {
        Name: string
        Born: DateTime
        Sex: Sex
    }

type Class1() =
    member this.X = "F#"

    member this.hello() =
        printf "Test"

    member this.prefix prefixStr baseStr =
        prefixStr + ", " + baseStr

    member this.test names =
        names
        |> Seq.map(this.prefix "Hello")

    member this.exclusionList =
        let __            _ = true
        let olderThan   x y = x < y
        let youngerThan x y = x > y
        [|
          "Not allowed for senior"      , olderThan   65, __
          "Not allowed for children"    , youngerThan 16, __
          "Not allowed for young males" , youngerThan 25, (=) Male
        |]

