using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L1C
{
	/// <summary>
	/// A struct that can keep the number of a guess and the status outcome of it in
	/// relation to the secret number other guesses
	/// </summary>
	public struct GuessedNumber
	{
		public int? Number;
		public Outcome Outcome;
	}
}
