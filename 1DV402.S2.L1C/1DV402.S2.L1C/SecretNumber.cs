﻿using System;
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

			for (int i = 0; i < MaxNumberOfGuesses; i++)
			{
				GuessedNumbers[i].Number = default(int?);
				GuessedNumbers[i].Outcome = default(Outcome);
			}
			this.Number = generator.Next(1, 100);
		} 

		public Outcome MakeGuess(int guess)
		{
			String order = "";
			Guess = guess;				

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
			
			get { 
	//			Console.WriteLine(String.Format("Count:{0}   GuessedNumbers[Count].Outcome: {1}", Count, GuessedNumbers[Count].Outcome));
				return ((Count == 0) || (Count < MaxNumberOfGuesses) && (GuessedNumbers[Count-1].Outcome != Outcome.Right));
			} 
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
