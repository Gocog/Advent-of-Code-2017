package day_6;

/** Type for containing information about memory cycles. */
public class CycleInfoStruct {
	public CycleInfoStruct(int _cycles, int _loopSize) {
		cycles = _cycles;
		loopSize = _loopSize;
	}
	public int getCycles() {
		return cycles;
	}

	public int getLoopSize() {
		return loopSize;
	}
	
	private int cycles;
	private int loopSize;
}