#include "stdafx.h"
#include "Registers.h"


Registers::Registers(const std::string &path) {
	getInstructionListFromFile(path);
	parseInstructionList();
}

Registers::Registers(const std::vector<Instruction> &_instructions) {
	instructions = _instructions;
	parseInstructionList();
}

/* Parses the instructions in the instructions vector and updates the registers..*/
void Registers::parseInstructionList() {
	maxValue = INT_MIN;

	for (const Instruction & ins : instructions) {

		int conditionValue = registerMap[ins.con.ref];
		int registerValue = registerMap[ins.reg];

		if (ins.evaluateCondition(conditionValue)) {
			registerMap[ins.reg] = registerValue + ins.val;
		}

		if (registerMap[ins.reg] > maxValue) {
			maxValue = registerMap[ins.reg];
		}
	}
}

/* Gets the largest value contained in the registry. */
int Registers::getLargestValue() {
	int largestValue = INT_MIN;

	for (const std::pair<std::string, int> & p : registerMap) {
		if (p.second > largestValue) {
			largestValue = p.second;
		}
	}

	return largestValue;
}

/* Gets the maximum value that any register has had during the execution of instructions. */
int Registers::getMaximumValue() {
	return maxValue;
}

/*	Loads the instructions from a file at the specified path. */
void Registers::getInstructionListFromFile(const std::string &path) {
	std::string line;
	std::vector<std::string> lines;
	std::ifstream inputFile;

	// Populate the lines vector with all the lines of the file.
	inputFile.open(path);
	while (getline(inputFile, line)) {
		lines.push_back(line);
	}
	inputFile.close();

	// Generate instructions from all the lines.
	instructions = std::vector<Instruction>();
	std::vector<std::string>::iterator it = lines.begin();
	while (it != lines.end()) {
		instructions.push_back(Instruction(*it));
		it++;
	}
}
