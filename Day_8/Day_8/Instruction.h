#pragma once
#include "stdafx.h"
#include <string>
#include <map>

typedef bool(*OperatorFunction)(const int&, const int&);

struct ConditionalStruct {
	std::string ref;
	std::string op;
	int val;
};

class Instruction {
public:
	std::string reg;
	std::string op;
	int val;
	ConditionalStruct con;

	Instruction(std::string);
	static std::map<std::string, OperatorFunction> operatorMap;
	bool evaluateCondition(const int &val) const;
};