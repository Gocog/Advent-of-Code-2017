#include "stdafx.h"
#include "Passphrase.h"

int main()
{
	std::string puzzlepath = "puzzleinput.txt";
	Passphrase p = Passphrase(puzzlepath);
	std::cout << "Passphrases puzzle\n";

	// First task checks for duplicates
	int firstTask = p.countValidInPhraseList(&p.phraseHasNoDuplicates);
	// Second task checks for anagrams
	int secondTask = p.countValidInPhraseList(&p.phraseHasNoAnagrams);

	// Print the result to the console
	std::cout << "First task: " << firstTask << "\n";
	std::cout << "Second task: " << secondTask;

	std::cin.get();
	return 0;
}