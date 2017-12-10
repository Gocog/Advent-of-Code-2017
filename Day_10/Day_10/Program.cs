using System;
using System.IO;
using System.Linq;

namespace Day_10
{
    class KnotHash
    {
		private const int SPARSELENGTH = 256;
		private const int DENSEFACTOR = 16;
		private readonly int[] tailstring = { 17, 31, 73, 47, 23 };
		private string puzzlepath = @"puzzleinput.txt";

		public KnotHash() {
			Console.WriteLine("Knot Hash puzzle");

			Console.Write("First task: ");
			int[] input = GetInputListFromFile(puzzlepath,false);
			int[] knothashlist = GenerateSparseHash(input, 1);
			int result = knothashlist[0] * knothashlist[1];
			Console.WriteLine(result);

			Console.Write("Second task: ");
			input = GetInputListFromFileAsByte(puzzlepath,true);
			string hash = GenerateKnotHash(GenerateSparseHash(input, 64));
			Console.WriteLine(hash);

			Console.Read();
		}

		/// <summary>
		/// Given an int array representing the sparse hash, generates a string
		/// representing the dense hash.
		/// </summary>
		/// <param name="sparsehash">The sparse hash int array.</param>
		/// <returns>String representing the final hash.</returns>
		public string GenerateKnotHash(int[] sparsehash) {
			int factor = DENSEFACTOR;
			string hashstring = "";

			for (int i = 0; i < (sparsehash.Length / factor); i++) {
				int num = 0;
				for (int j = 0; j < factor; j++) {
					num ^= sparsehash[i * factor + j];
				}
				hashstring += num.ToString("X2");
			}

			return hashstring;
		}

		/// <summary>
		/// Generates the sparse hash based on the integer input and number
		/// of rounds.
		/// </summary>
		/// <param name="input">The array of integers used as parameter for each operation.</param>
		/// <param name="rounds">The number of times to run through the input list.</param>
		/// <returns></returns>
		public int[] GenerateSparseHash(int[] input, int rounds) {
			int length = SPARSELENGTH;
			int[] sparse = Enumerable.Range(0, length).ToArray();
			int position = 0;
			int skipsize = 0;

			for (int round = 0; round < rounds; round++) {
				foreach (int i in input) {
					int[] reversed = new int[i];
					for (int j = 0; j < i; j++) {
						int readpos = (position + j) % length;
						reversed[j] = sparse[readpos];
					}
					Array.Reverse(reversed);
					for (int j = 0; j < i; j++) {
						int readpos = (position + j) % length;
						sparse[readpos] = reversed[j];
					}
					position += i + skipsize;
					skipsize++;
				}
			}

			return sparse;
		}

		/// <summary>
		/// Gets the list of comma-separated integers from the given file.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <param name="addsuffix">Whether to append the standard suffix to the array.</param>
		/// <returns>List of integers found in file.</returns>
		private int[] GetInputListFromFile(string path, bool addsuffix) {
			string input = File.ReadAllText(path);
			int[] inputlist = input.Split(',').Select(s => Convert.ToInt32(s)).ToArray();

			if (addsuffix) {
				return AddSuffix(inputlist);
			}
			return inputlist;
		}

		/// <summary>
		/// Gets an array of integers representing the numerical value of
		/// each character in the file.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <param name="addsuffix">Whether to append the standard suffix to the array.</param>
		/// <returns>List of integers representing the file's contents.</returns>
		public int[] GetInputListFromFileAsByte(string path, bool addsuffix) {
			string input = File.ReadAllText(path);
			int[] inputlist = input.Select(s => Convert.ToInt32(s)).ToArray();

			if (addsuffix) {
				return AddSuffix(inputlist);
			}
			return inputlist;
		}

		/// <summary>
		/// Generates a new integer array by appending the standard suffix.
		/// </summary>
		/// <param name="list">The list to append to.</param>
		/// <returns>The new array, combining the parameter with the standard suffix.</returns>
		private int[] AddSuffix(int[] list) {
			int[] newList = new int[list.Length + tailstring.Length];
			list.CopyTo(newList, 0);
			tailstring.CopyTo(newList, list.Length);

			return newList;
		}

		static void Main(string[] args) {
			new KnotHash();
		}
	}
}
