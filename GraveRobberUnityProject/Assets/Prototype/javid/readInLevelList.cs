using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;  

public class readInLevelList : MonoBehaviour {

	public static string[,] levelData = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static string[,] getLevelList(){
		if (levelData == null) {
			TextAsset t = Resources.Load("LevelList")as TextAsset;

			string text = t.text;
			string[] lines = text.Split('\n');
			//Debug.Log (lines);

			//Debug.Log (levelData[0]);
			levelData = new string[lines.Length,3];
			for(int i =0;i<lines.Length;i++){
				string[] a = lines[i].Split(',');
				levelData[i,0] = a[0];
				levelData[i,1] = a[1];
				levelData[i,2] = a[2];
			}
		}
		/*string[,] levelData = new string[,]
		{
			{"Assets/Scenes/MainMenuScene.unity","MainMenuScene", ""},
			{"Assets/Scenes/LevelSelectScene.unity","LevelSelectScene", ""},
			{"Assets/Scenes/VictoryScene.unity","VictoryScene", ""},
			{"Assets/Prototype/jordan/SmallCompilation.unity","SmallCompilation", "1 - The Sarcadian Sanctuary"},
			{"Assets/Prototype/Matthew/Compilation.unity","Compilation", "2 - The Nameless Catacombs"},
			{"Assets/Prototype/javid/TempleRun.unity","TempleRun", "S1 - Temple Run"},
			{"Assets/Prototype/javid/RelicRoomBridgePuzzle2.unity","RelicRoomBridgePuzzle2", "S2 - Light Bridge Puzzle"},
			{"Assets/Prototype/JoeGremlich/PusherPullerAwesome.unity","PusherPullerAwesome", "S3 - Pusher! Puller! Awesome!"},
		};*/
		//TextAsset mydata = Resources.Load("Prototype/javid/Resourses/LevelList.txt") as TextAsset;

		//TextAsset txt = (TextAsset)Resources.Load("a", typeof(TextAsset));
		//Debug.Log (txt.text);
	//	Debug.Log (t.text);

		return levelData;
	}

	/*private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(',');
						if (entries.Length > 0)
							DoStuff(entries);
					}
				}
				while (line != null);
				
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}*/


}

