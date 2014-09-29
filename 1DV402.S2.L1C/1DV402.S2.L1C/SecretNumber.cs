using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L1C
{
	public enum Outcome		// possible status of a guess
	{
		Indefinite,
		Low,
		High,
		Right,
		NoMoreGuesses,
		OldGuess
	}
	public class SecretNumber
	{
		public const int MaxNumberOfGuesses = 7;
		private int _count;		// number of made guesses
		private GuessedNumber[] _guessedNumbers;  // collection of made guesses
		private int? _number;		// The secret number
		private int? _guess = null;
		Random generator;

		public SecretNumber()
		{
			_guessedNumbers = new GuessedNumber[MaxNumberOfGuesses];
			generator = new Random();
			Initialize();
		}

		public void Initialize() 
		{
			Guess = null;
			Outcome = Outcome.Indefinite;
			Count = 0;
			Number = 0;
			Array.Clear(_guessedNumbers, 0, _guessedNumbers.Length);
			Number = generator.Next(1, 100);
		}

		public Outcome MakeGuess(int guess)
		{
			String order = "";
							

			switch (Count)
			{
				case 0: 
					order = Strings.First;
					break;
				case 1:
					order = Strings.Second;
					break;
				case 2:
					order = Strings.Third;
					break;
				case 3:
					order = Strings.Fourth;
					break;
				case 4:
					order = Strings.Fifth;
					break;
				case 5:
					order = Strings.Sixth;
					break;
				case 6:
					order = Strings.Seventh;
					break;
				default:
					Outcome = Outcome.NoMoreGuesses;
					break;

			}
			if (Outcome != Outcome.NoMoreGuesses)
			{
				Console.Write(String.Format(Strings.Guess_Number, order));
				Guess = int.Parse(Console.ReadLine());

				if ((Guess < 1) || (Guess > 100))
				{
					throw new ArgumentOutOfRangeException();
				}
				else if (Guess == Number)
					Outcome = Outcome.Right;
				else if (Guess < Number)
					Outcome = Outcome.Low;
				else if (Guess > Number)
					Outcome = Outcome.High;
				else if (Array.IndexOf(GuessedNumbers, Guess) != -1)
					Outcome = Outcome.OldGuess;

				GuessedNumbers[Count].Number = (int)Guess;
				GuessedNumbers[Count].Outcome = Outcome;

				Count++;
			}
			return Outcome;
		}

		/**************/
		/* Properties */
		/**************/

		public int Count     
		{
			get { return _count; }
			private set {  _count = value; }
		}

		public bool CanMakeGuess 
		{ 
			get { return (Count < MaxNumberOfGuesses); } 
		}

		public int? Guess { get; private set; }

		public GuessedNumber[] GuessedNumbers
		{
			get { return _guessedNumbers; }  // byt mot  referens
		}

		public int? Number
		{
			get 
			{ 
				if (CanMakeGuess)
						return null;
					else
						return _number;
			}
			private set { _number = value; }
		}
		public Outcome Outcome { get; private set; }

	}
}
