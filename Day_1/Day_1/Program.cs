using System;
using System.IO;
using System.Linq;

namespace Day_1 {
	class InverseCaptcha {
		/// <summary>
		/// Iterates through the array, comparing the number at index i to the number
		///	at index i + offset. If the numbers are equal, it adds the number once to the sum.
		///	The method returns this sum.
		///	</summary>
		///	<param name="numbers">The array to check.</param>
		///	<param name="offset">The offset between the two numbers to compare.</param>
		///	<returns>The sum of all numbers that match the number at their index+offset.</returns>
		public static int DecodeSequence(int[] numbers, int offset) {
			if (numbers.Length == 0)
				throw new ArgumentException("DecodeSequence called on empty array!");

			int sum = 0;
			for (int i = 0; i < numbers.Length; i++) {
				if (numbers[i] == numbers[(i + offset) % numbers.Length]) {
					sum += numbers[i];
				}
			}

			return sum;
		}

		/// <summary>
		/// Returns the array of integers representing the contents of the file at the specified path.
		/// </summary>
		private static int[] GetSequenceFromFile(string path) {
			int[] numbers = File.ReadAllText(path).Select(c => Convert.ToInt32(c.ToString())).ToArray();
			return numbers;
		}

        static void Main(string[] args) {
			string puzzlepath = @"puzzleinput.txt";
			int[] sequence = GetSequenceFromFile(puzzlepath);

			Console.WriteLine("First task: " + DecodeSequence(sequence, 1));
			Console.WriteLine("Second task: " + DecodeSequence(sequence, sequence.Length / 2));

			Console.Read();
		}
    }
}