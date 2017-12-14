#pragma once

#include "stdafx.h"
#include <vector>
#include <string>
#include <map>
#include "Instruction.h"

class Registers {
public:
	Registers(const std::string &path);
	Registers(const std::vector<Instruction> &_instructions);
	int getLargestValue();
	int getMaximumValue();
private:
	void getInstructionListFromFile(const std::string &path);
	void parseInstructionList();
	std::vector<Instruction> instructions;
	std::map<std::string, int> registerMap;
	int maxValue = 0;
};

