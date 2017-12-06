package day_6;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.Arrays;
import java.util.HashMap;

public class MemoryReallocation {
	private String puzzlepath = "src/puzzleinput.txt";

	public MemoryReallocation() {
		int[] memory = getMemoryFromFile(puzzlepath);
		
		System.out.println("Memory Reallocation puzzle");
		
		CycleInfoStruct lis = getCycleInfo(memory);
		int redistributionCycles = lis.cycles;
		int loopSize = lis.loopSize;
		
		System.out.println("First task: "+redistributionCycles);
		System.out.println("Second task: "+loopSize);
		
		try {
			System.in.read();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	/** Calculates the number of redistribution cycles required before a repeat configuration appears.
	 * @param _memory	An integer array representing the initial memory configuration. Is not overwritten.
	 * */
	private static CycleInfoStruct getCycleInfo(int[] _memory) {
		// Don't overwrite the original memory
		int[] memory = _memory.clone();
		
		HashMap<String,Integer> occurrences = new HashMap<String, Integer>();
		int cycles = 0;
		do {
			
			// Save the current state to the occurences map.
			occurrences.put(getHash(memory), cycles);
			
			// Reallocate memory.
			reallocate(memory);
			cycles++;
			
			// Repeat only if the current memory configuration has not occurred before.
		} while(occurrences.getOrDefault(getHash(memory), 0) == 0);
		
		int loopSize = cycles - (occurrences.get(getHash(memory))); 
		
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
	 	it returns the first of them. */
	private static int indexOfLargest(int[] array) {
		int largest = 0;
		for (int current = 0; current < array.length; current++) {
			if (array[current] > array[largest]) {
				largest = current;
			}
		}
		return largest;
	}
	
	/** Returns an integer array representing the memory stored in the file at the specified path. */
	private static int[] getMemoryFromFile(String path) {
		BufferedReader reader = new BufferedReader(getFileReader(path));
		String delimiter = "	";
		// Read all lines from file. Should only be one.
		String[] lines = reader.lines().toArray(String[]::new);
		// Should there be more lines, join them by same delimiter as the rest, and split the entire string.
		String[] bankStrings = String.join(delimiter,lines).split(delimiter);
		int[] result = Arrays.stream(bankStrings).mapToInt((s) -> Integer.parseInt(s)).toArray();
		
		return result;
	}
	
	/** Gets a FileReader that reads from the file at the specified path. */
	private static FileReader getFileReader(String path) {
		try {
			File inputfile = new File(path);
			return new FileReader(inputfile);
		} catch (FileNotFoundException e) {
			System.err.println("File not found at path "+ path);
			e.printStackTrace();
		}
		return null;
	}
	
	public static void main(String[] args) {
		new MemoryReallocation();
	}
}

/** Type for containing information about memory cycles. */
class CycleInfoStruct {
	public CycleInfoStruct(int _cycles, int _loopSize) {
		cycles = _cycles;
		loopSize = _loopSize;
	}
	public int cycles;
	public int loopSize;
}
