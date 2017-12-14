using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Day_10
{
    public class KnotHash
    {
		private const int SPARSELENGTH = 256;
		private const int DENSEFACTOR = 16;
		private readonly int[] tailstring = { 17, 31, 73, 47, 23 };

		private int[] m_sparsehash;
		public ReadOnlyCollection<int> SparseHash { get { return Array.AsReadOnly(m_sparsehash); } }
		private string m_hash;
		public string Hash { get { return m_hash; } private set { m_hash = value; } }

		public KnotHash(string input, bool asByte, int rounds) {
			int[] inputlist = asByte ? GetInputListFromStringAsByte(input,true) : GetInputListFromString(input, false);
			m_sparsehash = GenerateSparseHash(inputlist, rounds);
			Hash = GenerateKnotHash();
		}

		/// <summary>
		/// Returns the value of this KnotHash represented as a string of 4-bit binary numbers.
		/// </summary>
		/// <returns>String of joined 4-bit binary numbers representing the hash.</returns>
		public string AsBinaryHash() {
			// Convert each character to int from hex, then convert the int to 4-bit binary. Join together all numbers to one string.
			string binaryhash = String.Join("",Hash.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4,'0')));

			return binaryhash;
		}

		/// <summary>
		/// Given an int array representing the sparse hash, generates a string
		/// representing the dense hash.
		/// </summary>
		/// <param name="sparsehash">The sparse hash int array.</param>
		/// <returns>String representing the final hash.</returns>
		public string GenerateKnotHash() {
			int factor = DENSEFACTOR;
			string hashstring = "";

			for (int i = 0; i < (SparseHash.Count / factor); i++) {
				int num = 0;
				for (int j = 0; j < factor; j++) {
					num ^= SparseHash[i * factor + j];
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
		private int[] GenerateSparseHash(int[] input, int rounds) {
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
		/// Gets the list of comma-separated integers from the given string.
		/// </summary>
		/// <param name="input">The string.</param>
		/// <param name="addsuffix">Whether to append the standard suffix to the array.</param>
		/// <returns>List of integers found in string.</returns>
		private int[] GetInputListFromString(string input, bool addsuffix) {
			int[] inputlist = input.Split(',').Select(s => Convert.ToInt32(s)).ToArray();

			if (addsuffix) {
				return AddSuffix(inputlist);
			}
			return inputlist;
		}

		/// <summary>
		/// Gets an array of integers representing the numerical value of
		/// each character in the string.
		/// </summary>
		/// <param name="input">The string.</param>
		/// <param name="addsuffix">Whether to append the standard suffix to the array.</param>
		/// <returns>List of integers representing the string's contents.</returns>
		private int[] GetInputListFromStringAsByte(string input, bool addsuffix) {
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
			string puzzlepath = @"puzzleinput.txt";
			string input = File.ReadAllText(puzzlepath);

			Console.WriteLine("Knot Hash puzzle");

			Console.Write("First task: ");
			KnotHash kh = new KnotHash(input, false, 1);
			int result = kh.SparseHash[0] * kh.SparseHash[1];
			Console.WriteLine(result);

			Console.Write("Second task: ");
			kh = new KnotHash(input, true, 64);
			Console.WriteLine(kh.Hash);

			Console.Read();
		}
	}
}
