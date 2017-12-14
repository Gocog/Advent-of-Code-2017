package day_2;

import java.io.BufferedReader;
import java.io.IOException;
import java.util.Arrays;
import java.util.function.Function;

public class CorruptionChecksum {

	public int[][] spreadsheet;
	
	/** Constructs spreadsheet from file at path. */
	public CorruptionChecksum(String path) {
		spreadsheet = getArrayFromFile(path);
	}
	
	/** Uses predefined spreadsheet. */
	public CorruptionChecksum(int[][] _spreadsheet) {
		spreadsheet = _spreadsheet;
	}
	
	/** Given a two-dimensional integer array, gets the sum of the line sums. Each line sum is
	 	calculated using the provided function.*/
	public int getCheckSum(Function<int[],Integer> linesum) {
		int checksum = 0;
		for (int i = 0; i < spreadsheet.length; i++) {
			checksum += linesum.apply(spreadsheet[i]);
		}
		return checksum;
	}
	
	/** Finds the difference between the largest and smallest numbers in an integer array.
	  	@param _numbers The array to compare within. 
	  	@return The difference between the smallest and largest numbers in the array.*/
	public static int largestDifference(int[] _numbers) {
		// Sort array, so we can just take the difference between first and last values in sorted array.
		int[] numbers = _numbers.clone();
		Arrays.sort(numbers);
		return numbers[numbers.length -1] - numbers[0];
	}
	
	/** Given an integer array of numbers wherein only one pair of integers in the array can be divided
	 	resulting in an integer, returns the result of that division. If multiple pairs should exist,
	 	it returns the first pair it finds.
	 	@param _numbers The array to find the pair in.
	 	@return The result of the division between the only two numbers that can be divided in the array.*/
	public static int onlyDivisible(int[] _numbers) throws IllegalArgumentException {
		// Sort array, as we are only interested in dividing by smaller numbers.
		int[] numbers = _numbers.clone();
		Arrays.sort(numbers);
		
		// Iterate descending. Never check larger numbers as dividing by a larger number never results in an integer.
		for (int i = numbers.length - 1; i > 0; i--) {
			for (int j = i - 1; j >= 0; j--) {
				if (numbers[i] % numbers[j] == 0)
					return numbers[i] / numbers[j];
			}
		}
		throw new IllegalArgumentException("No divisible pair found in array!");
	}
	
	/** Returns a two-dimensional integer array representing the "spreadsheet" stored in the file at the specified path. 
	 	@param path The path to the file. 
	 	@return The 2D array of integers representing the spreadsheet. */
	private static int[][] getArrayFromFile(String path) {
		BufferedReader reader = new BufferedReader(file.FileUtil.getFileReader(path));
		String[] lines = reader.lines().toArray(String[]::new);
		int rows = lines.length;
		
		int[][] result = new int[rows][];
		for (int i = 0; i < rows; i++) {
			result[i] = getArrayFromString(lines[i]);
		}
		return result;
	}
	
	/** Takes a tab-separated string of integer values and returns an array containing those values as integers.
	 	@param inputString the string to split into integers.
	 	@return The integers found in the string. */
	private static int[] getArrayFromString(String inputString) {
		int[] numbers = Arrays.stream(inputString.split("	")).mapToInt(s-> Integer.parseInt(s)).toArray();
		return numbers;
	}

	
	public static void main (String[] args) {
		String puzzlepath = "src/puzzleinput.txt";
		System.out.println("Corruption Checksum puzzle");
		CorruptionChecksum cs = new CorruptionChecksum(puzzlepath);
		
		System.out.println("First task: " + cs.getCheckSum(CorruptionChecksum::largestDifference));
		System.out.println("Second task: " + cs.getCheckSum(CorruptionChecksum::onlyDivisible));
		
		
		try {
			System.in.read();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
