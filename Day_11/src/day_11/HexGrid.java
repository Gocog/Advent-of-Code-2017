package day_11;

import java.io.BufferedReader;

public class HexGrid {
	/** Gets a HexGridNavInfo object containing the minimum number of steps required
	 	to reach the destination the directions array point to, and the maximum distance
	 	from the starting position reached while following the directions. 
	 	@param directions The array of Strings indicating the direction to take each turn.
	 	@return The HexGridNavInfo object containing information about the path.*/
	public static HexGridNavInfo getNavInfo(String[] directions) {
		int x = 0;
		int y = 0;
		int maxDist = 0;
		int dist = 0;
		for (String direction : directions) {
			int deltaY = 1;
			if (direction.contains("e")) {
				x++;
			} else if (direction.contains("w")) {
				x--;
			} else {
				deltaY = 2;
			}
			
			if (direction.startsWith("n")) {
				y+=deltaY;
			} else {
				y-=deltaY;
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
	 	@param y The y coordinate in the hex grid.
	 	@return The number of steps to go.*/
	public static int getSteps(int x, int y) {
		x = Math.abs(x);
		y = Math.abs(y);
		int steps = 0;
		if (x > y) {
			steps = x;
		} else {
			// Move horizontally until directly above/below.
			steps = x;
			y -= x;
			
			// Move up/down in steps of 2.
			steps += y/2;
		}

		return steps;
	}
	
	/** Returns a String array containing all the directions. */
	public static String[] getDirectionsFromFile(String path) {
		BufferedReader reader = new BufferedReader(file.FileUtil.getFileReader(path));
		String delimiter = ",";
		// Read all lines from file. Should only be one.
		String[] lines = reader.lines().toArray(String[]::new);
		// Should there be more lines, join them by same delimiter as the rest, and split the entire string.
		String[] directionStrings = String.join(delimiter,lines).split(delimiter);
		
		return directionStrings;
	}

	
	public static void main(String[] args) {
		String puzzlepath = "src/puzzleinput.txt";
		
		System.out.println("Hex Ed puzzle");
		String[] directions = getDirectionsFromFile(puzzlepath);
		
		HexGridNavInfo result = getNavInfo(directions);

		System.out.println("First task: " + result.getSteps());
		System.out.println("Second task: " + result.getMaxDist());
	}
}

