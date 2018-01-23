using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;

public abstract class uteMapDefinitionWriter {
	public abstract string Write(uteMapDefinition mapDefinition);

	public static string Write(uteMapDefinition mapDefinition, bool useXML)
	{
		if (useXML)
		{
			return new uteMapXMLDefinitionWriter().Write(mapDefinition);
		}
		else
		{
			return new uteMapSplitStringDefinitionWriter().Write(mapDefinition);
		}
	}
}

public class uteMapSplitStringDefinitionWriter : uteMapDefinitionWriter {
	public override string Write (uteMapDefinition mapDefinition)
	{
		StringBuilder result = new StringBuilder();

		for(int i=0;i<mapDefinition.TileCount;i++)
		{
			uteMapTileDefinition tileDef = mapDefinition.GetTile(i);
			result.Append(tileDef.PrefabGUID);
			result.Append(':');

			result.Append(tileDef.Position.x);
			result.Append(':');
			result.Append(tileDef.Position.y);
			result.Append(':');
			result.Append(tileDef.Position.z);
			result.Append(':');
			
			result.Append(tileDef.EulerAngles.x);
			result.Append(':');
			result.Append(tileDef.EulerAngles.y);
			result.Append(':');
			result.Append(tileDef.EulerAngles.z);
			result.Append(':');

			result.Append(tileDef.IsStatic ? "1" : "0");
			result.Append(':');
			result.Append(tileDef.IsTileConnected ? "1" : "0");
			result.Append(':');
			result.Append(tileDef.IsTileConnected ? tileDef.ConnectionFamily : "-");
			result.Append(":$");
		}

		return result.ToString();
	} 
}

public class uteMapXMLDefinitionWriter : uteMapDefinitionWriter {

	private void WriteTile(XmlWriter xmlWriter, uteMapTileDefinition tile)
	{
		xmlWriter.WriteStartElement("tile");

		xmlWriter.WriteElementString("prefab-guid", tile.PrefabGUID);
		xmlWriter.WriteElementString("position", tile.Position.x + "," + tile.Position.y + "," + tile.Position.z);
		xmlWriter.WriteElementString("euler-angles", tile.EulerAngles.x + "," + tile.EulerAngles.y + "," + tile.EulerAngles.z);
		xmlWriter.WriteElementString("is-static", tile.IsStatic ? "true" : "false");

		if(tile.IsTileConnected)
		{
			xmlWriter.WriteElementString("tc-family", tile.ConnectionFamily);
		}
		
		xmlWriter.WriteEndElement();
	}

	private void WriteTiles(XmlWriter xmlWriter, uteMapDefinition mapDefinition)
	{
		xmlWriter.WriteStartElement("tiles");

		for (int i = 0; i < mapDefinition.TileCount; ++i)
		{
			WriteTile(xmlWriter, mapDefinition.GetTile(i));
		}

		xmlWriter.WriteEndElement();
	}

	public override string Write (uteMapDefinition mapDefinition)
	{
		XmlWriterSettings settings = new XmlWriterSettings();
		settings.Indent = true;
		settings.IndentChars = "    ";

		StringBuilder result = new StringBuilder();
		using (XmlWriter xmlWriter = XmlWriter.Create(result, settings))
		{
			xmlWriter.WriteStartDocument();
			
			xmlWriter.WriteStartElement("map");
			WriteTiles(xmlWriter, mapDefinition);
			xmlWriter.WriteEndElement();

			xmlWriter.WriteEndDocument();
		}

		return result.ToString();
	} 
}