#include "stdafx.h"
#include "Registers.h"


Registers::Registers() {
	std::cout << "Registers puzzle";
	std::vector<Instruction> instructions = getInstructionList(puzzlepath);
	std::pair<std::map<std::string, int>, int> registerPair = parseInstructionList(instructions);
	std::cout << "First task: " << largestValue(registerPair.first) << "\n";
	std::cout << "Second task: " << registerPair.second;

	std::cin.get();
}

/* Takes a vector of Instructions and returns a pair: a map from string to int of final registry values, and an int representing
	the maximum value any register reached during execution of the instructions.*/
std::pair<std::map<std::string, int>, int> Registers::parseInstructionList(const std::vector<Instruction> & instructions) {
	std::map<std::string, int> registerMap;
	int maxValue = INT_MIN;

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

	return std::pair<std::map<std::string, int>, int>(registerMap, maxValue);
}

/* Gets the largest value contained in the registry. */
int Registers::largestValue(const std::map<std::string, int> &registerMap) {
	int largestValue = INT_MIN;

	for (const std::pair<std::string, int> & p : registerMap) {
		if (p.second > largestValue) {
			largestValue = p.second;
		}
	}

	return largestValue;
}

/*	Given a file path, return a vector of line-separated strings in that file. */
std::vector<Instruction> Registers::getInstructionList(const std::string &path) {
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
	std::vector<Instruction> instructions;
	std::vector<std::string>::iterator it = lines.begin();
	while (it != lines.end()) {
		instructions.push_back(Instruction(*it));
		it++;
	}

	return instructions;
}
