package file;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;

public class FileUtil {
	/** Gets a FileReader that reads from the file at the specified path. */
	public static FileReader getFileReader(String path) {
		try {
			File inputfile = new File(path);
			return new FileReader(inputfile);
		} catch (FileNotFoundException e) {
			System.err.println("File not found at path "+ path);
			e.printStackTrace();
		}
		return null;
	}
}
