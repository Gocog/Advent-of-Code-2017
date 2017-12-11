package day_11;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;

public class HexGrid {
	private String puzzlepath = "src/puzzleinput.txt";
	
	public HexGrid() {
		String[] directions = getDirectionsFromFile(puzzlepath);
		
		HexGridNavInfo result = getNavInfo(directions);

		System.out.println("First task: " + result.steps);
		System.out.println("Second task: " + result.maxDist);
	}
	
	/** Gets a HexGridNavInfo object containing the minimum number of steps required
	 	to reach the destination the directions array point to, and the maximum distance
	 	from the starting position reached while following the directions. 
	 	@param directions The array of Strings indicating the direction to take each turn.*/
	HexGridNavInfo getNavInfo(String[] directions) {
		int x = 0;
		int y = 0;
		int maxDist = 0;
		int dist = 0;
		for (String direction : directions) {
			if (direction.contains("e")) {
				x++;
			} else if (direction.contains("w")) {
				x--;
			}
			
			if (direction.startsWith("n")) {
				y++;
			} else {
				y--;
			}
			
			dist = getSteps(x,y);
			if (dist > maxDist) {
				maxDist = dist;
			}
		}
		
		return new HexGridNavInfo(dist,maxDist);
	}
	
	/** Returns the distance from origin in a hex grid.  
	 	@param x The x coordinate in the hex grid.
	 	@param y The y coordinate in the hex grid.*/
	int getSteps(int x, int y) {
		/* In a hex grid using axial coordinates, the distance
			between two points is simply the largest coordinate
			delta between them. For distance to origin, that means
			simply the largest coordinate component.
		*/
		
		x = Math.abs(x);
		y = Math.abs(y);

		return x > y ? x : y;
	}
	
	/** Returns a String array containing all the directions */
	private String[] getDirectionsFromFile(String path) {
		BufferedReader reader = new BufferedReader(getFileReader(path));
		String delimiter = ",";
		// Read all lines from file. Should only be one.
		String[] lines = reader.lines().toArray(String[]::new);
		// Should there be more lines, join them by same delimiter as the rest, and split the entire string.
		String[] directionStrings = String.join(delimiter,lines).split(delimiter);
		
		return directionStrings;
	}
	
	/** Gets a FileReader that reads from the file at the specified path. */
	private FileReader getFileReader(String path) {
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
		new HexGrid();
	}
}

