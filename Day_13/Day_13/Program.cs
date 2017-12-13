using System;
using System.Collections.Generic;
using System.IO;

namespace Day_13
{
    class PacketScanners
    {
		static void Main(string[] args) {
			string puzzlepath = "puzzleinput.txt";
			Console.WriteLine("Packet Scanners puzzle");

			int[] ranges = GetInputListFromFile(puzzlepath);
			int severity = GetSeverity(ranges);
			Console.WriteLine("First task: " + severity);

			int delay = BruteForceGetSafeTime(ranges);
			Console.WriteLine("Second task: " + delay);

			Console.Read();
		}

		/// <summary>
		/// Starting with a delay of 0, checks for the first delay
		/// resulting in not getting caught.
		/// </summary>
		/// <param name="ranges">The scanner ranges indexed by depth.</param>
		/// <returns>The delay in picoseconds required to not get caught.</returns>
		public static int BruteForceGetSafeTime(int[] ranges) {
			int delay = 0;
			bool caught = false;
			do {
				delay++;
				caught = GetCaught(ranges, delay);
			} while (caught);
			return delay;
		}

		/// <summary>
		/// Checks if you are caught when running through the firewall
		/// after the waiting the given delay.
		/// </summary>
		/// <param name="ranges">The scanner ranges indexed by depth.</param>
		/// <param name="delay">The number of picoseconds to wait before starting.</param>
		/// <returns>True if caught, false otherwise.</returns>
		public static bool GetCaught(int[] ranges, int delay) {
			int time;
			int range;
			int steps;

			for (int i = 0; i < ranges.Length; i++) {
				time = i + delay;
				range = ranges[i];
				if (range != 0) {
					steps = (range - 1) * 2;
					if (time % steps == 0) {
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Gets the total severity of running through the firewall.
		/// </summary>
		/// <param name="ranges">The scanner ranges indexed by depth.</param>
		/// <returns>Sum of severities from being discovered.</returns>
		public static int GetSeverity(int[] ranges) {
			int severity = 0;
			int time;
			int range;
			int steps;

			for (int i = 0; i < ranges.Length; i++) {
				time = i;
				range = ranges[i];
				if (range != 0) {
					steps = (range - 1) * 2;
					if (time % steps == 0) {
						severity += range * i;
					}
				}
			}
			return severity;
		}

		/// <summary>
		/// Gets the list of indexed integers in a file. The largest index found
		/// sets the length of the array, missing indices are set to 0.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <returns>List of integers found in file.</returns>
		private static int[] GetInputListFromFile(string path) {
			string[] lines = File.ReadAllLines(path);
			string[] segments;
			Dictionary<int, int> input = new Dictionary<int, int>();
			int idx = 0;
			int val = 0;
			foreach (string line in lines) {
				segments = line.Split(' ');
				idx = Convert.ToInt32(segments[0].Replace(":",""));
				val = Convert.ToInt32(segments[1]);
				input[idx] = val;
			}

			int[] inputlist = new int[idx+1];
			for (int i = 0; i < inputlist.Length; i++) {
				input.TryGetValue(i, out inputlist[i]);
			}
			return inputlist;
		}
    }
}
