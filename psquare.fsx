#load "Reference.fsx"
#time "on"
open System
open System.Collections.Generic
open Akka
open Akka.Actor
open Akka.FSharp
open Akka.Configuration
open Akka.TestKit
open System.Diagnostics

let isSquare(x : int64) = 
    let m = sqrt(float(x))
    let a = ceil m
   // printfn "ceil m = %f, m = %f" a m
    ceil m = m

let sum_Square(x : int64, y :int64)  = 
    let mutable sum : int64 = 0L
    for i in 0L..y-1L do 
        sum <- (x + i) * (x + i) + sum
        //printfn"sum %i"x
    //printfn "sum %i"sum
    isSquare(sum)

let system = System.create "system" (Configuration.defaultConfig())
type Command = 
    | NUMBER of int64 * int64
    | RNUMBER of int64
    | START
    | GIVE_NUMBER 
    | NO_MORE 
    | WORK_DONE
    
let worker1 (mailbox: Actor<_>) =
    let rec loop() = actor {
        let! msg = mailbox.Receive()
        let sender = mailbox.Sender()
        match msg with
        | START -> sender <! GIVE_NUMBER
        | NUMBER (x , y) ->
          //  printfn "worker received x = %i ,y = %i" x y
            if sum_Square(x, y) then
                printf "%i, " x
               // sender <! RNUMBER x
            sender <! GIVE_NUMBER
        | NO_MORE ->
            sender <! WORK_DONE
        | _ -> ()
        return! loop()
    }
    loop()

let worker2 (mailbox: Actor<_>) =
    let rec loop() = actor {
        let! msg = mailbox.Receive()
        let sender = mailbox.Sender()
        match msg with
        | START -> sender <! GIVE_NUMBER
        | NUMBER (x , y) ->
          //  printfn "worker received x = %i ,y = %i" x y
            if sum_Square(x, y) then
                printf "%i, " x
               // sender <! RNUMBER x
            sender <! GIVE_NUMBER
        | NO_MORE ->
            sender <! WORK_DONE
        | _ -> ()
        return! loop()
    }
    loop()

let worker3 (mailbox: Actor<_>) =
    let rec loop() = actor {
        let! msg = mailbox.Receive()
        let sender = mailbox.Sender()
        match msg with
        | START -> sender <! GIVE_NUMBER
        | NUMBER (x , y) ->
          //  printfn "worker received x = %i ,y = %i" x y
            if sum_Square(x, y) then
                printf "%i, " x
               // sender <! RNUMBER x
            sender <! GIVE_NUMBER
        | NO_MORE ->
            sender <! WORK_DONE
        | _ -> ()
        return! loop()
    }
    loop()

let worker4 (mailbox: Actor<_>) =
    let rec loop() = actor {
        let! msg = mailbox.Receive()
        let sender = mailbox.Sender()
        match msg with
        | START -> sender <! GIVE_NUMBER
        | NUMBER (x , y) ->
          //  printfn "worker received x = %i ,y = %i" x y
            if sum_Square(x, y) then
                printf "%i, " x
               // sender <! RNUMBER x
            sender <! GIVE_NUMBER
        | NO_MORE ->
            sender <! WORK_DONE
        | _ -> ()
        return! loop()
    }
    loop()

let worker5 (mailbox: Actor<_>) =
    let rec loop() = actor {
        let! msg = mailbox.Receive()
        let sender = mailbox.Sender()
        match msg with
        | START -> sender <! GIVE_NUMBER
        | NUMBER (x , y) ->
          //  printfn "worker received x = %i ,y = %i" x y
            if sum_Square(x, y) then
                printf "%i, " x
               // sender <! RNUMBER x
            sender <! GIVE_NUMBER
        | NO_MORE ->
            sender <! WORK_DONE
        | _ -> ()
        return! loop()
    }
    loop()

let boss = spawn system "boss" <| fun mailbox ->
    let worker1 = spawn system "worker1" worker1
    let worker2 = spawn system "worker2" worker2
    let worker3 = spawn system "worker3" worker3
    let worker4 = spawn system "worker4" worker4
    let worker5 = spawn system "worker5" worker5
    //let worker6 = spawn system "worker6" worker6
    //let worker7 = spawn system "worker7" worker7
    //let worker8 = spawn system "worker8" worker8
        
    worker1 <! START
    worker2 <! START
    worker3 <! START
    worker4 <! START
    worker5 <! START
    //worker6 <! START
    //worker7 <! START
    //worker8 <! START
    let mutable currentNum = 1L
    let mutable workernum = 5
    let mutable n0 = 0L
    let mutable k0 = 0L
    let rec loop() = actor {
        let! msg = mailbox.Receive()
        let sender = mailbox.Sender()
       
        match msg with
        //| NUMBER x -> printfn "Hello, %i" x
        | NUMBER (x , y) ->
            n0 <- x
            k0 <- y 
            printf "["     
        | GIVE_NUMBER -> 
            if(currentNum <= n0) then
              //  printfn "current Num is %i" currentNum
                sender <! NUMBER (currentNum , k0)
                currentNum <- currentNum + 1L
            else
                sender <! NO_MORE
        //| RNUMBER x -> printfn "boss print %i" x
        | WORK_DONE ->
            workernum <- workernum - 1
            if(workernum = 0 ) then
                printfn "]"
                printf "Please press enter to get time..."
        | _ -> ()                   
        return! loop()
    }
    loop()

let n = fsi.CommandLineArgs.[1] |> int64
let k = fsi.CommandLineArgs.[2] |> int64

boss <! NUMBER(n , k)

System.Console.ReadLine() |> ignore