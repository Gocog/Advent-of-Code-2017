using System;
using System.Collections.Generic;
using System.Linq;
using Day_10;

namespace Day_14
{
    public class Defragmenter
    {
		/// <summary>
		/// Contains integers for row and column indices.
		/// </summary>
		public struct Coords {
			public Coords(int _row, int _col) {
				row = _row;
				col = _col;
			}
			public int row;
			public int col;
		}

		public bool[,] Grid;
		public List<List<Coords>> Regions;

		public Defragmenter(string input, int size) {
			InitialiseGrid(input, size);
			InitialiseRegions();
		}

		/// <summary>
		/// Returns a list of all populated coordinates in the Grid.
		/// </summary>
		/// <returns></returns>
		public List<Coords> UsedSquares() {
			List<Coords> usedsquares = new List<Coords>();
			for (int row = 0; row < Grid.GetLength(0); row++) {
				for (int col = 0; col < Grid.GetLength(1); col++) {
					if (Grid[row,col]) usedsquares.Add(new Coords(row, col));
				}
			}
			return usedsquares;
		}

		/// <summary>
		/// Populates the Grid 2D bool array based on the binary knot hash of the
		/// input value and the row number.
		/// </summary>
		/// <param name="input">The string to hash.</param>
		/// <param name="size">The size of the grid.</param>
		private void InitialiseGrid(string input, int size) {
			Grid = new bool[size, size];
			for (int row = 0; row < size; row++) {
				string binaryhash = new KnotHash(input + "-" + row, true, 64).AsBinaryHash();
				for (int col = 0; col < size; col++) {
					Grid[row, col] = binaryhash.ElementAt(col) == '1';
				}
			}
		}

		/// <summary>
		/// Finds all regions and populates the Region 2D List.
		/// </summary>
		private void InitialiseRegions() {
			Regions = new List<List<Coords>>();
			List<Coords> usedsuares = UsedSquares();
			Dictionary<Coords, int> regionmapping = new Dictionary<Coords, int>();

			int region = 0;
			foreach (Coords coords in usedsuares) {
				if (!regionmapping.ContainsKey(coords)) {
					SetNeighbours(coords, region, ref regionmapping);
					region++;
					Regions.Add(new List<Coords>());
				}
				Regions[regionmapping[coords]].Add(coords);	
			}
		}

		/// <summary>
		/// Sets this coordinate and all populated neighbours of this coordinate to the same region,
		/// then calls itself for each of those neigbhours. Does not count diagonals as neighbours.
		/// </summary>
		/// <param name="coords">The initial coordinates.</param>
		/// <param name="region">The region to assign the coordinates to.</param>
		/// <param name="regionmapping">The dictionary mapping coordinates to regions.</param>
		private void SetNeighbours(Coords coords, int region, ref Dictionary<Coords,int> regionmapping) {
			// Assign self to region.
			regionmapping[coords] = region;

			// Periodic function in steps of 1 between 1 and -1, starting at 0.
			int offset(int n) => (int)Math.Pow(-1, n / 2) * (n % 2);

			// Visit all non-diagonal neighbours.
			for (int i = 0; i < 4; i++) {

				// Find neighbour's coordinates.
				int neighbourrow = Math.Clamp(coords.row + offset(i), 0, Grid.GetLength(0) - 1);
				int neighbourcol = Math.Clamp(coords.col + offset(i+1), 0, Grid.GetLength(1) - 1);
				Coords neighbour = new Coords(neighbourrow, neighbourcol);

				// Call this method on neighbour if neighbour is populated and not yet registered in a region.
				if (Grid[neighbour.row, neighbour.col] && !regionmapping.ContainsKey(neighbour)) {
					SetNeighbours(neighbour, region, ref regionmapping);
				}
			}
		}

        static void Main(string[] args)
        {
			Console.WriteLine("Disk Defragmentation puzzle");

			string input = "hwlqcszp";

			Defragmenter df = new Defragmenter(input,128);

			Console.WriteLine("First task: " + df.UsedSquares().Count);
			Console.WriteLine("Second task: " + df.Regions.Count);

			Console.Read();
			
        }
    }
}
