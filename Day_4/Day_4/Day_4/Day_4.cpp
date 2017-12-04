#include <iostream>
#include <sstream>
#include <fstream>
#include <set>
#include <vector>
#include <string>
#include <algorithm>

using namespace std;

class PassPhrase {
public:
	PassPhrase();
private:
	static vector<string> getPassphraseList(const string &path);
	static int countValidInPhraseList(const vector<string> &phrases, bool (*evaluator)(const string&));
	static bool phraseHasNoDuplicates(const string &phrase);
	static bool phraseHasNoAnagrams(const string &phrase);
	static bool phraseIsValid(const string &phrase, string(*wordfunction)(const string&) = NULL);

	static string sortWord(const string &word);
	string puzzlepath = "puzzleinput.txt";
};

PassPhrase::PassPhrase() {
	cout << "Passphrases puzzle\n";

	// Get all our passphrases in a vector
	vector<string> passphrases = getPassphraseList(puzzlepath);

	// First task checks for duplicates
	int firstTask = countValidInPhraseList(passphrases, &phraseHasNoDuplicates);
	// Second task checks for anagrams
	int secondTask = countValidInPhraseList(passphrases, &phraseHasNoAnagrams);

	// Print the result to the console
	cout << "First task: " << firstTask << "\n";
	cout << "Second task: " << secondTask;
	
	cin.get();
}

/*	Given the vector of phrase strings and evaluator function, return how many phrase strings are valid according
	to the evaluator function. */
int PassPhrase::countValidInPhraseList(const vector<string> &phrases, bool (*evaluator)(const string&) ) {
	int counter = 0;
	for (string phrase : phrases) {
		if (evaluator(phrase))
			counter++;
	}
	return counter;
}

/*	Given a file path, return a vector of line-separated strings in that file. */
vector<string> PassPhrase::getPassphraseList(const string &path) {
	string phrase;
	vector<string> phrases;
	ifstream inputFile;
	
	// Populate the phrase vector with all the lines of the file, one string per line
	inputFile.open(path);
	while ( getline(inputFile, phrase) ) {
		phrases.push_back(phrase);
	}
	inputFile.close();

	return phrases;
}

/*	Check if a phrase has no duplicate words in it. */
bool PassPhrase::phraseHasNoDuplicates(const string &phrase) {
	return phraseIsValid(phrase);
}

/*	Check if a phrase has no two words in it that are anagrams of each other. */
bool PassPhrase::phraseHasNoAnagrams(const string &phrase) {
	return phraseIsValid(phrase, &sortWord);
}

/*	Check if a phrase contains no duplicate words. The wordfunction is applied to each word
	before it is compared to the other words, and added to the word set itself. */
bool PassPhrase::phraseIsValid(const string &phrase, string (*wordfunction)(const string&)) {
	set<string> wordSet;
	istringstream wordStream(phrase);

	do {
		// Store words in a string.
		string word;
		wordStream >> word;

		// Apply the optional wordfunction, if any.
		if (wordfunction != NULL)
			word = wordfunction(word);

		// Return if word already exists.
		if (wordSet.find(word) != wordSet.end()) {
			return false;
		}
		// Add word to set. 
		wordSet.insert(word);
	} while (wordStream);
}

/*	Returns a new string that is the sorted version of the passed string. */
string PassPhrase::sortWord(const string &word) {
	string sortedWord = word;
	sort(sortedWord.begin(), sortedWord.end());
	return sortedWord;
}

int main()
{
	PassPhrase p;
	return 0;
}