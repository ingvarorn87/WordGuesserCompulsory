﻿// Learn more about F# at http://fsharp.org


open System
open Configuration



//Fetches the Hidden char from the Configuration module
let mutable hiddenCharacter = Configuration.HIDDEN

let mutable case_sensitive = Configuration.CASE_SENSITIVE

let mutable help = Configuration.HELP

let mutable allow_Blanks = Configuration.ALLOW_BLANKS

let mutable multiple = Configuration.MULTIPLE

//Gets the list of words from the configuration module
let mutable listOfWords = Configuration.WORDS




//Replaces the random word with a '*' for each character
let toPartialWord (word:string) (used:char seq) =
   word |> String.map (fun c -> 
      if Seq.exists ((=) c) used then c else hiddenCharacter
   )

//Checks if the guess is valid, as is only takes in chars, does not take numbers or spaces
let isGuessValid (used:char seq) (guess:char) =
   Seq.exists ((=) guess) ['A'..'Z'] &&
   not (used |> Seq.exists ((=) guess))

//Reads the guess given by the user.
let rec readGuess used =
   let guess = Console.ReadKey(true).KeyChar |> Char.ToUpper
   if isGuessValid used guess then guess
   else readGuess used
let getGuess used =
   Console.Write("Guess: ")
   let guess = readGuess used
   Console.WriteLine(guess)
   guess
   

//checks the guess to the Partialword and the randomword. 
//if the guess is correct the word' is changed and written
//if the word(full word) is equal to the word(partial) the if statement closes
//else it adds the guess to the "used list" and add 1 to the tally of incorrect guesses
let rec play word used nrOfGuesses =
   let word' = toPartialWord word used
   Console.WriteLine(word')
   if word = word' then 
    Console.WriteLine("Correct, you guessed the word in :")
   
   else
      let guess = getGuess used
      
      //let numberofguesses = nrOfGuesses+1
      //printfn "Number of wrong guesses %A" numberofguesses
      let used = guess::used
      printfn "Guesses tried %A" used
      if word |> String.exists ((=) guess)
      then play word used nrOfGuesses
      else play word used (nrOfGuesses+1)
      printfn "%A" used.Length 
      printf " guesses"
      

  

    



let word = listOfWords.[Random().Next(listOfWords.Length)]
printfn "Welcome to Word Guesser"
printfn "The length of the word is %A" word.Length
do play word [] 0