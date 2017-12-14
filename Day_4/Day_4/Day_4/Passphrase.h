#pragma once
#include "stdafx.h"

class Passphrase {
public:
	Passphrase(const std::string &path);
	std::vector<std::string> PassphraseList;
	int countValidInPhraseList(bool(*evaluator)(const std::string&));
	static bool phraseHasNoDuplicates(const std::string &phrase);
	static bool phraseHasNoAnagrams(const std::string &phrase);
private:
	static std::vector<std::string> getPassphraseList(const std::string &path);
	static bool phraseIsValid(const std::string &phrase, std::string(*wordfunction)(const std::string&) = NULL);

	static std::string sortWord(const std::string &word);
};