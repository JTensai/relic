using UnityEngine;
using System.Collections;

public class PreRenderSettings : MonoBehaviour {

	public bool renderAmbient = false;
	public bool renderLights = true;
	public Color AmbientLight = Color.white;

	private Color previousColor;
	private Light[] lights;
	private float[] lightIntensity;

	public void OnPreRender()
	{
		previousColor = RenderSettings.ambientLight;

		if (!renderAmbient)
		{
			RenderSettings.ambientLight = Color.black;
		}
		else{
			RenderSettings.ambientLight = AmbientLight;
		}

		if (!renderLights)
		{
			lights = FindObjectsOfType<Light>();
			lightIntensity = new float[lights.Length];

			for (int i = 0; i < lights.Length; ++i)
			{
				lightIntensity[i] = lights[i].intensity;
				lights[i].intensity = 0;
			}
		}
	}

	public void OnPostRender()
	{
		RenderSettings.ambientLight = previousColor;
		
		if (!renderLights)
		{			
			for (int i = 0; i < lights.Length; ++i)
			{
				lights[i].intensity = lightIntensity[i];
			}
		}
	}
}
