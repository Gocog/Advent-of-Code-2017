#include "stdafx.h"
#include "Instruction.h"

/*Creates a new instruction based on a string.*/
Instruction::Instruction(std::string instruction) {
	std::istringstream componentStream(instruction);
	std::string valStr;

	componentStream >> reg;
	componentStream >> op;
	componentStream >> valStr;
	val = stoi(valStr);

	if (op == "dec") {
		val *= -1;
	}

	// Skip "if" symbol
	componentStream >> valStr;

	componentStream >> con.ref;
	componentStream >> con.op;
	componentStream >> valStr;
	con.val = stoi(valStr);
}

/*Evaluates the conditional of this instruction. First value of the expression is supplied
	by the caller, since the instruction does not contain it.*/
bool Instruction::evaluateCondition(const int &val) const {
	return operatorMap[con.op](val, con.val);
}

std::map<std::string, OperatorFunction> Instruction::operatorMap{
	{ "<", [](const int& val1, const int& val2) {return val1 < val2;} },
{ ">", [](const int& val1, const int& val2) {return val1 > val2;} },
{ "<=",[](const int& val1, const int& val2) {return val1 <= val2;} },
{ ">=",[](const int& val1, const int& val2) {return val1 >= val2;} },
{ "!=",[](const int& val1, const int& val2) {return val1 != val2;} },
{ "==",[](const int& val1, const int& val2) {return val1 == val2;} }
};