using UnityEngine;
using System.Collections;

public class CompositeRender : MonoBehaviour {
	public Camera ambientCamera;
	public Camera lightCamera;
	public Shader lightShader;

	public void Start()
	{

	}
	
	public void OnPreRender()
	{
		Color ambientColor = RenderSettings.ambientLight;
		lightCamera.SetReplacementShader(lightShader, null);
		if (!ambientCamera.enabled)
		{
			Light[] lights = FindObjectsOfType<Light>();
			float[] lightIntensities = new float[lights.Length];

			for (int i = 0; i < lights.Length; ++i)
			{
				lightIntensities[i] = lights[i].intensity;
				lights[i].intensity = 0;
			}

			ambientCamera.Render();
			
			for (int i = 0; i < lights.Length; ++i)
			{
				lights[i].intensity = lightIntensities[i];
			}
		}

		if (!lightCamera.enabled)
		{
			RenderSettings.ambientLight = Color.black;
			lightCamera.RenderWithShader(lightShader, null);
			RenderSettings.ambientLight = ambientColor;
		}
	}
}
