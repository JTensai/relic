using UnityEngine;
using System.Collections;

public class DamageVignette : MonoBehaviour
{
    private float alpha = 0f;
    private GUITexture texture;
    new private bool active = false;
    private Color curColor;
    private float timer = 0;
    private float FadeInRate;
    private float FadeOutRate;
    public float FadeInTime = .125f;
	public float FadeOutTime = .375f;
	public float maxOpacity = .5f;

    // Use this for initialization
    void Start()
    {
        //Debug.Log(Screen.width);
        texture = GetComponent<GUITexture>();
        curColor = texture.color;
        Rect r = new Rect(0, 0, Screen.width, Screen.height);
        texture.pixelInset = r;
		FadeInRate = maxOpacity / FadeInTime;
		FadeOutRate = maxOpacity / FadeOutTime;
    }

    // Update is called once per frame
    void Update()
    {
        curColor = texture.color;

		if (active)
		{
			float deltaTime = Time.deltaTime;
			timer += deltaTime;

			if (timer < FadeInTime && alpha < maxOpacity)
				alpha += deltaTime * FadeInRate;

			else if (alpha > 0)
				alpha -= deltaTime * FadeOutRate;

			curColor.a = alpha;
			texture.color = curColor;
			//Debug.Log("alpha: " + alpha);
		}

		//if (timer > FadeInTime + FadeOutTime)
		//{
		//	timer = 0;
		//	active = false;
		//}

		if (alpha <= 0)
		{
			alpha = 0;
			active = false;
		}
    }

	//public void Activate()
	//{
	//	//Debug.Log("Activate vignette");
	//	active = true;
	//	alpha = 0;
	//	timer = 0;
	//}

	// Add more to the alpha based on amount of damage taken
	public void AddIntensity(float dmgAmount)
	{
		float alphaPerDmg = .25f;
		float alphaToAdd = dmgAmount * alphaPerDmg;

		active = true;
		timer = 0;
		maxOpacity = alpha + alphaToAdd;

		if (maxOpacity >= 1) maxOpacity = 1;

		FadeInRate = maxOpacity / FadeInTime;
		FadeOutRate = maxOpacity / FadeOutTime;
	}

    public static GUITexture CreateVignette()
    {
        GameObject retVal = new GameObject("DamageVignette");
        GUITexture damVin = retVal.AddComponent<GUITexture>();
        retVal.AddComponent<DamageVignette>();

        return damVin;
    }

    public void Destroy()
    {
        this.Destroy();
    }

}
