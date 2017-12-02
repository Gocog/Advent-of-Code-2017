using System;
using System.IO;
using System.Linq;

namespace Day_1 {
	class InverseCaptcha {
		private string puzzlepath = @"puzzleinput.txt";

		public InverseCaptcha() {
			int[] sequence = GetSequenceFromFile(puzzlepath);
			Console.WriteLine("First task: " + DecodeSequence(sequence,1));
			Console.WriteLine("Second task: " + DecodeSequence(sequence, sequence.Length / 2));

			Console.Read();
		}

		/// <summary>
		/// Iterates through the array, comparing the number at index i to the number
		///	at index i + offset. If the numbers are equal, it adds the number once to the sum.
		///	The method returns this sum.
		///	</summary>
		private int DecodeSequence(int[] numbers, int offset) {
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
		private int[] GetSequenceFromFile(string path) {
			FileStream inputStream = File.Open(path, FileMode.Open);
			StreamReader reader = new StreamReader(inputStream);

			int[] numbers = reader.ReadToEnd().Select(c => Convert.ToInt32(c.ToString())).ToArray();
			return numbers;
		}

        static void Main(string[] args) {
            new InverseCaptcha();
        }
    }
}