package day_11;

/** Type for containing information about the results of a navigation. */
public class HexGridNavInfo {
	HexGridNavInfo(int _steps, int _maxDist){
		steps = _steps;
		maxDist = _maxDist;
	}
	private int steps;
	private int maxDist;
	
	public int getSteps() {
		return steps;
	}
	
	public int getMaxDist() {
		return maxDist;
	}
}