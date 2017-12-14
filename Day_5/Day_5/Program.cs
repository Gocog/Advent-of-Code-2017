using System;
using System.IO;
using System.Linq;

namespace Day_5
{
    class TrampolineMaze
    {
		private int[] jumpList;
		public delegate int ListUpdateFunctionDelegate(int stepSize);

		public TrampolineMaze(string path) {
			jumpList = GetJumpListFromFile(path);
		}

		/// <summary>
		/// Gets the number of steps required to get outside of the list. Each step
		///	moves the position by the value in the list at that position. After moving,
		///	the position you moved from has its value set according to the provided update function.
		///	</summary>
		public int GetSteps(ListUpdateFunctionDelegate updateFunc) {
			// Make a copy of the list so we don't change the original.
			int[] jumpListCopy = new int[jumpList.Length];
			jumpList.CopyTo(jumpListCopy,0);

			// Initialise position to the start (0), and step counter to 0.
			int position = 0;
			int steps = 0;

			// Update our position until it is no longer inside the list.
			while (position < jumpListCopy.Length && position >= 0) {
				int newposition = position + jumpListCopy[position];
				jumpListCopy[position] = updateFunc(jumpListCopy[position]);
				position = newposition;
				steps++;
			}
			// Return the number of steps it took to reach the outside.
			return steps;
		}

		/// <summary>
		/// Adds 1 to the value.
		/// </summary>
		public static int AddOne(int value) {
			return value + 1;
		}

		/// <summary>
		/// Adds 1 to the value, unless value is 3 or more, then subtracts 1.
		/// </summary>
		public static int AddUnder3ElseRemove(int value) {
			return value + (value < 3 ? 1 : -1);
		}

		/// <summary>
		/// Generates an integer array based on a file with line-separated integers, one per line. */
		/// </summary>
		private int[] GetJumpListFromFile(string path) {
			string[] lines = File.ReadAllLines(path);
			int[] numbers = lines.Select(s => Convert.ToInt32(s)).ToArray();
			return numbers;
		}

		static void Main(string[] args) {
			string puzzlepath = @"puzzleinput.txt";
			TrampolineMaze tm = new TrampolineMaze(puzzlepath);

			Console.WriteLine("A Maze of Twisty Trampolines, All Alike");
			// Gets the steps required when adding one to each position after moving.
			Console.WriteLine("First task: " + tm.GetSteps(AddOne));
			// Gets the steps required when adding one for values under 3 and -1 otherwise after moving.
			Console.WriteLine("Second task: " + tm.GetSteps(AddUnder3ElseRemove));

			Console.Read();
		}
	}
}