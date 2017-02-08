using System;

namespace Kamina.Contracts.Objects
{
    public class HangmanGame
    {
        public string TargetWord { get; set; } = String.Empty;

        public string AlreadyHadLetters { get; set; } = String.Empty;

        public int Mistakes { get; set; } = 0;

        public string CorrectGuessedLetters { get; set; } = String.Empty;
    }
}
