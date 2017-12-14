#include "stdafx.h"
#include "Registers.h"

int main() {
	std::string puzzlepath = "puzzleinput.txt";
	Registers r = Registers(puzzlepath);
	std::cout << "Registers puzzle\n";

	std::cout << "First task: " << r.getLargestValue() << "\n";
	std::cout << "Second task: " << r.getMaximumValue();

	std::cin.get();
	return 0;
}