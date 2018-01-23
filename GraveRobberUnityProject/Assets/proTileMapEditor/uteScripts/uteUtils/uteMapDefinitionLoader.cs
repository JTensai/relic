using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public abstract class uteMapDefinitionLoader
{
	public abstract uteMapDefinition Load(string content);

	private static bool IsXml(string content)
	{
		return content.Length > 0 && content[0] == '<';
	}

	public static uteMapDefinition LoadDefinition(string content)
	{
		if (IsXml(content))
		{
			return new uteMapXMLDefinitionLoader().Load(content);
		}
		else
		{
			return new uteMapSplitStringDefinitionLoader().Load(content);
		}
	}
}

public class uteMapSplitStringDefinitionLoader : uteMapDefinitionLoader
{
	public uteMapSplitStringDefinitionLoader()
	{

	}

	public override uteMapDefinition Load (string content)
	{
		uteMapDefinition result = new uteMapDefinition();

		string[] tiles = content.Split('$');

		foreach (string tile in tiles)
		{
			if (tile.Length > 0)
			{
				string[] tileParts = tile.Split(':');

				uteMapTileDefinition tileDefinition = new uteMapTileDefinition(
					// the prefab GUID
					tileParts[0],
					// parse the position
					new Vector3(float.Parse(tileParts[1]), float.Parse(tileParts[2]), float.Parse(tileParts[3])),
					// parse the orientation
					new Vector3(int.Parse(tileParts[4]), int.Parse(tileParts[5]), int.Parse(tileParts[6])),
					// is static
					tileParts[7] == "1",
					// tile connected family
					(tileParts[8] == "1") ? tileParts[9] : null);

				result.AddTile(tileDefinition);
			}
		}

		return result;
	}
}

public class uteMapXMLDefinitionLoader : uteMapDefinitionLoader
{
	public uteMapXMLDefinitionLoader()
	{

	}

	private Vector3 ParseVector(string stringVector)
	{
		if (stringVector == null)
		{
			return new Vector3();
		}
		else
		{
			string[] parts = stringVector.Split(',');
			return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
		}
	}

	private uteMapTileDefinition ParseTile(XmlReader reader)
	{
		if (reader.IsStartElement("tile"))
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.ReadToDescendant("prefab-guid");
				string prefabGUID = reader.ReadElementContentAsString();
				reader.ReadToNextSibling("position");
				Vector3 position = ParseVector(reader.ReadElementContentAsString());
				reader.ReadToNextSibling("euler-angles");
				Vector3 eulerAngles = ParseVector(reader.ReadElementContentAsString());
				reader.ReadToNextSibling("is-static");
				bool isStatic = reader.ReadElementContentAsString() == "true";
				string tcFamily = null;

				if (reader.ReadToNextSibling("tc-family"))
				{
					tcFamily = reader.ReadElementContentAsString();
				}
				
				while (reader.NodeType != XmlNodeType.EndElement && reader.Name != "tile")
				{
					if (reader.IsStartElement())
					{
						reader.Skip();
					}
					else
					{
						reader.Read();
					}
				}
				
				reader.ReadEndElement();

				return new uteMapTileDefinition(prefabGUID, position, eulerAngles, isStatic, tcFamily);
			}
		}

		return null;
	}

	private void ParseTiles(XmlReader reader, uteMapDefinition mapDefinition)
	{
		if (reader.IsStartElement("tiles"))
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
			}
			else
			{
				reader.Read();

				while (reader.ReadToNextSibling("tile"))
				{
					mapDefinition.AddTile(ParseTile(reader));
				}

				reader.ReadEndElement();
			}
		}
	}

	public override uteMapDefinition Load (string content)
	{
		uteMapDefinition result = new uteMapDefinition();

		using (XmlReader reader = XmlReader.Create(new StringReader(content)))
		{
			if (reader.IsStartElement("map"))
			{
				if (reader.IsEmptyElement)
				{
					reader.Read();
				}
				else
				{
					reader.Read();
					ParseTiles(reader, result);
				}
			}
		}

		return result;
	}
}
