using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class fileHandler {

	string path = Directory.GetCurrentDirectory () + "/Dialogue/";

	public ArrayList Load(string fileName)
	{
		ArrayList diagArray = new ArrayList ();

		string line;

		StreamReader theReader = new StreamReader(path + fileName, Encoding.Default);
		
		using (theReader)
		{
			do
			{
				line = theReader.ReadLine();
				
				if (line != null)
				{
					string[] entries = line.Split('|');
					if (entries.Length > 2)
					{	
						diagArray.Add(entries);
						//Debug.Log(entries[0]);
					}
				}
			}
			while (line != null);
	
			theReader.Close();

			return diagArray;
		}
	}
}
