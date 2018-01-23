using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class uteMapDatabase {
	
	private List<string> maps;
	private List<string> patterns;

	private static uteMapDatabase singleton = null;

	public static uteMapDatabase GetSingleton()
	{
		if (singleton == null)
		{
			singleton = new uteMapDatabase();
		}

		return singleton;
	}

	private List<string> ReadListFile(string directory, string suffix)
	{
		List<string> result = new List<string>();

		foreach (string file in Directory.GetFiles(directory))
		{
			if (file.Substring(file.Length - suffix.Length) == suffix)
			{
				result.Add(Path.GetFileName(file.Substring(0, file.Length - suffix.Length)));
			}
		}

		return result;
	}

	public void ReloadMapList()
	{
#if UNITY_EDITOR
		maps = ReadListFile(uteGLOBAL3dMapEditor.getMapsDir(), "_info.txt");
		patterns = ReadListFile(uteGLOBAL3dMapEditor.getPatternsDir(), ".xml");
#endif
	}

	private uteMapDatabase()
	{
		ReloadMapList();
	}

	private void WriteListFile(string path, List<string> list)
	{
		StreamWriter sw = new StreamWriter(path);
		sw.Write("");
		sw.Write(string.Join(":", list.ToArray()));
		sw.Flush();
		sw.Close();
	}

	public void CreateNewMap(string name)
	{
		maps.Add(name);
	}

	public bool DoesMapExist(string name)
	{
		return maps.Contains(name);
	}

	public List<string> EnumerateMaps()
	{
		return maps;
	}

	public void DeleteAllMaps()
	{
		while (maps.Count > 0)
		{
			DeleteMap(maps[0]);
		}
	}

	public void DeleteMap(string name)
	{
		maps.Remove(name);

#if UNITY_EDITOR
		string mapPath = uteGLOBAL3dMapEditor.getMapsDir()+name;
		
		if (File.Exists(mapPath + ".xml"))
		{
			File.Delete(mapPath + ".xml");
		}
		
		if (File.Exists(mapPath + ".txt"))
		{
			File.Delete(mapPath + ".txt");
		}
		
		if (File.Exists(mapPath + "_info.txt"))
		{
			File.Delete(mapPath + "_info.txt");
		}
#endif
	}
	
	
	public void CreateNewPattern(string name)
	{
		patterns.Add(name);
	}
	
	public bool DoesPatternExist(string name)
	{
		return patterns.Contains(name);
	}
	
	public List<string> EnumeratePatterns()
	{
		return patterns;
	}
	
	public void DeleteAllPatterns()
	{
		while (patterns.Count > 0)
		{
			DeletePattern(patterns[0]);
		}
	}
	
	public void DeletePattern(string name)
	{
		patterns.Remove(name);
				
#if UNITY_EDITOR
		string mapPath = uteGLOBAL3dMapEditor.getPatternsDir()+name;
		
		if (File.Exists(mapPath + ".xml"))
		{
			File.Delete(mapPath + ".xml");
		}
		
		if (File.Exists(mapPath + ".txt"))
		{
			File.Delete(mapPath + ".txt");
		}
		
		if (File.Exists(mapPath + "_info.txt"))
		{
			File.Delete(mapPath + "_info.txt");
		}
#endif
	}
}
