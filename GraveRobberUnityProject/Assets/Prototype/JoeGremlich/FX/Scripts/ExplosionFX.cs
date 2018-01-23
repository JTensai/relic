using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ExplosionFX : MonoBehaviour
{

    public AnimationCurve LightIntensity;
    new public Light light;
    new public Camera camera;
    ParticleSystem _pSystem;

    // Use this for initialization
    void Start()
    {

        _pSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        FastBloom bloom = null;
        if (camera != null)
            bloom = camera.GetComponent<FastBloom>();

        float pTime = _pSystem.time;

        if (light != null)
        {
            light.intensity = LightIntensity.Evaluate(pTime);
        }
        if (bloom != null)
        {
            bloom.intensity = (LightIntensity.Evaluate(pTime));
        }
    }
}
