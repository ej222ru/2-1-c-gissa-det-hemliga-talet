using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L1C
{
	// possible status of a guess
	public enum Outcome		
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
		// Declare and initialize local variables
		int _count;							// number of made guesses
		GuessedNumber[] _guessedNumbers;	// collection of made guesses
		int? _number;						// The secret number
		Random generator;

		/// <summary>
		/// Constructor för class SecretNumber. 
		/// </summary>
		public SecretNumber()
		{
			_guessedNumbers = new GuessedNumber[MaxNumberOfGuesses];
			generator = new Random();
			Initialize();
		}

		/******************/
		/* Member Methods */
		/******************/

		public void Initialize() 
		{
			// Initialize global properties
			Guess = null;
			Outcome = Outcome.Indefinite;
			Count = 0;
			for (int i = 0; i < MaxNumberOfGuesses; i++)
			{
				_guessedNumbers[i].Number = default(int?);
				_guessedNumbers[i].Outcome = default(Outcome);
			}
			// Generate a secret number in closed range 1-100
			this.Number = generator.Next(1, 101);
		} 

		public Outcome MakeGuess(int guess)
		{
			String order = "";
			Guess = guess;				

			switch (Count)
			{
				case 0: 
					order = Strings.Count_1;
					break;
				case 1:
					order = Strings.Count_2;
					break;
				case 2:
					order = Strings.Count_3;
					break;
				case 3:
					order = Strings.Count_4;
					break;
				case 4:
					order = Strings.Count_5;
					break;
				case 5:
					order = Strings.Count_6;
					break;
				case 6:
					order = Strings.Count_7;
					break;
				default:
					Outcome = Outcome.NoMoreGuesses;
					break;

			}
			// Determine status outcome of a guess
			if (Outcome != Outcome.NoMoreGuesses)
			{
				if ((Guess < 1) || (Guess > 100))
				{
					throw new ArgumentOutOfRangeException();
				}

				else if (Guess == _number)
					Outcome = Outcome.Right;
				else if (Guess < _number)
					Outcome = Outcome.Low;
				else if (Guess > _number)
					Outcome = Outcome.High;
				int i = 0;
				while (i < Count)
				{
					if (GuessedNumbers[i].Number == Guess)
					{
						Outcome = Outcome.OldGuess;
					}
					i++;
				}	

				_guessedNumbers[Count].Number = (int)Guess;
				_guessedNumbers[Count].Outcome = Outcome;
				// Increment the counter for made guesses
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
		/// <summary>
		/// Determine if a new guess is possible
		/// Test on number of already made guesses and that a correct guess has not been made
		/// </summary>
		public bool CanMakeGuess 
		{ 
			get { 
				return ((Count == 0) || (Count < MaxNumberOfGuesses) && (GuessedNumbers[Count-1].Outcome != Outcome.Right));
			} 
		}

		public int? Guess { get; private set; }
		/// <summary>
		/// Return a reference to made guesses. Retain integrity of source
		/// </summary>
		public GuessedNumber[] GuessedNumbers
		{
			get 
			{
				GuessedNumber[] guessedNumbers = new GuessedNumber[_guessedNumbers.Length]; 
				Array.Copy(_guessedNumbers, guessedNumbers, _guessedNumbers.Length);
				return guessedNumbers;
			} 
		}
		/// <summary>
		/// Return null as long as a new guess is possible, otherwise the secret number
		/// </summary>
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
