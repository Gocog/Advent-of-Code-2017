package day_6;

import java.io.BufferedReader;
import java.io.IOException;
import java.util.Arrays;
import java.util.HashMap;

public class MemoryReallocation {
	private int[] memory;
	public CycleInfoStruct cycleinfo;
	
	public MemoryReallocation(String path) {
		memory = getMemoryFromFile(path);
		cycleinfo = getCycleInfo();
	}
	
	public MemoryReallocation(int[] _memory) {
		memory = _memory;
		cycleinfo = getCycleInfo();
	}
	
	/** Calculates the number of redistribution cycles required before a repeat configuration appears. */
	private CycleInfoStruct getCycleInfo() {
		// Don't overwrite the original memory
		int[] memoryCopy = memory.clone();
		
		HashMap<String,Integer> occurrences = new HashMap<String, Integer>();
		int cycles = 0;
		do {
			
			// Save the current state to the occurences map.
			occurrences.put(getHash(memoryCopy), cycles);
			
			// Reallocate memory.
			reallocate(memoryCopy);
			cycles++;
			
			// Repeat only if the current memory configuration has not occurred before.
		} while(occurrences.getOrDefault(getHash(memoryCopy), 0) == 0);
		
		int loopSize = cycles - (occurrences.get(getHash(memoryCopy))); 
		
		return new CycleInfoStruct(cycles,loopSize);
	}

	/** Gets a hash for mapping memory configurations. Is equal for arrays where the indexes
	 	contain the same value between two arrays.*/
	private static String getHash(int[] memory) {
		return Arrays.toString(memory);
	}
	
	/** Redistributes the blocks from the largest memory bank between all banks.
	 * @param memory	The integer array to modify.
	 * */
	private static void reallocate(int[] memory) {
		int largestIndex = indexOfLargest(memory);
		int blocks = memory[largestIndex];
		memory[largestIndex] = 0;
		
		for (int i = 1; i <= blocks; i++) {
			int indexToChange = (largestIndex + i) % memory.length;
			memory[indexToChange] += 1;
		}
	}
	
	/** Returns the index of the largest element in the array. If multiple elements are equally large
	 	it returns the first of them. 
	 	@param array The array to search.
	 	@return The index of the largest number. The first index if tied. */
	private static int indexOfLargest(int[] array) {
		int largest = 0;
		for (int current = 0; current < array.length; current++) {
			if (array[current] > array[largest]) {
				largest = current;
			}
		}
		return largest;
	}
	
	/** Returns an integer array representing the memory stored in the file at the specified path. 
	 	@param path The path of the file to read from.
	 	@return The int array representing the memory. */
	private static int[] getMemoryFromFile(String path) {
		BufferedReader reader = new BufferedReader(file.FileUtil.getFileReader(path));
		String delimiter = "	";
		// Read all lines from file. Should only be one.
		String[] lines = reader.lines().toArray(String[]::new);
		// Should there be more lines, join them by same delimiter as the rest, and split the entire string.
		String[] bankStrings = String.join(delimiter,lines).split(delimiter);
		int[] result = Arrays.stream(bankStrings).mapToInt((s) -> Integer.parseInt(s)).toArray();
		
		return result;
	}
	
	public static void main(String[] args) {
		String puzzlepath = "src/puzzleinput.txt";
		
		System.out.println("Memory Reallocation puzzle");
		MemoryReallocation mr = new MemoryReallocation(puzzlepath);
		int redistributionCycles = mr.cycleinfo.getCycles();
		int loopSize = mr.cycleinfo.getLoopSize();
		
		System.out.println("First task: "+redistributionCycles);
		System.out.println("Second task: "+loopSize);
		
		try {
			System.in.read();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
