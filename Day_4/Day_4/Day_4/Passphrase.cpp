#include "stdafx.h"
#include "Passphrase.h"

Passphrase::Passphrase(const std::string &path) {
	PassphraseList = getPassphraseList(path);
}

/*	Given the std::vector of phrase std::strings and evaluator function, return how many phrase std::strings are valid according
to the evaluator function. */
int Passphrase::countValidInPhraseList(bool(*evaluator)(const std::string&)) {
	int counter = 0;
	for (std::string phrase : PassphraseList) {
		if (evaluator(phrase))
			counter++;
	}
	return counter;
}

/*	Given a file path, return a std::vector of line-separated std::strings in that file. */
std::vector<std::string> Passphrase::getPassphraseList(const std::string &path) {
	std::string phrase;
	std::vector<std::string> phrases;
	std::ifstream inputFile;

	// Populate the phrase std::vector with all the lines of the file, one std::string per line
	inputFile.open(path);
	while (getline(inputFile, phrase)) {
		phrases.push_back(phrase);
	}
	inputFile.close();

	return phrases;
}

/*	Check if a phrase has no duplicate words in it. */
bool Passphrase::phraseHasNoDuplicates(const std::string &phrase) {
	return phraseIsValid(phrase);
}

/*	Check if a phrase has no two words in it that are anagrams of each other. */
bool Passphrase::phraseHasNoAnagrams(const std::string &phrase) {
	return phraseIsValid(phrase, &sortWord);
}

/*	Check if a phrase contains no duplicate words. The wordfunction is applied to each word
before it is compared to the other words, and added to the word std::set itself. */
bool Passphrase::phraseIsValid(const std::string &phrase, std::string(*wordfunction)(const std::string&)) {
	std::set<std::string> wordset;
	std::istringstream wordStream(phrase);

	do {
		// Store words in a std::string.
		std::string word;
		wordStream >> word;

		// Apply the optional wordfunction, if any.
		if (wordfunction != NULL)
			word = wordfunction(word);

		// Return if word already exists.
		if (wordset.find(word) != wordset.end()) {
			return false;
		}
		// Add word to std::set. 
		wordset.insert(word);
	} while (wordStream);
}

/*	Returns a new std::string that is the sorted version of the passed std::string. */
std::string Passphrase::sortWord(const std::string &word) {
	std::string sortedWord = word;
	std::sort(sortedWord.begin(), sortedWord.end());
	return sortedWord;
}
