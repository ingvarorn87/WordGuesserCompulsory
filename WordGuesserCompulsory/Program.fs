// Learn more about F# at http://fsharp.org


open System
open Configuration



//Replaces the random word with a '*' for each character
let toPartialWord (word:string) (used:char seq) =
   word |> String.map (fun c -> 
      if Seq.exists ((=) c) used then c else '*'
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
//if the word(full word) is equal to the word(partial) the 
let rec play word used tally =
   let word' = toPartialWord word used
   Console.WriteLine(word')
   if word = word' then 
    Console.WriteLine("Correct")
   
   else
      let guess = getGuess used
      let used = guess::used
      if word |> String.exists ((=) guess)
      then play word used tally
      else play word used (tally+1)
      let totalGuesses = used.Length
      Console.WriteLine(totalGuesses)

    


let mutable listOfWords = Configuration.WORDS
let word = listOfWords.[Random().Next(listOfWords.Length)]
do play word [] 0