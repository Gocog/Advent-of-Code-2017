#pragma once

#include "stdafx.h"
#include <vector>
#include <string>
#include <map>
#include "Instruction.h"

class Registers {
public:
	Registers();
private:
	static std::vector<Instruction> getInstructionList(const std::string &path);
	static std::pair<std::map<std::string, int>, int> parseInstructionList(const std::vector<Instruction> & instructions);
	static int largestValue(const std::map<std::string, int> &registerMap);
	std::string puzzlepath = "puzzleinput.txt";
};

