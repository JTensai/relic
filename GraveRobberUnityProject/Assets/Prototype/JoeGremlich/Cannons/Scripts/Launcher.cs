using UnityEngine;
using System.Collections;

public class Launcher : FireRoutine
{

    public Projectile bulletType;
    public float chargeTime;
    public EffectBase chargeFX;
    public float coolDownTime;
    public EffectBase coolDownFX;


    new private bool active;

    // Use this for initialization
    void Start()
    {

    }

    IEnumerator coolDownWeapon()
    {
        EffectBase newInstance = null;
        if (coolDownFX != null)
        {
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newInstance = coolDownFX.GetInstance(spawnLocation);
            newInstance.PlayEffect();
        }
        yield return new WaitForSeconds(coolDownTime);
        if (newInstance != null)
        {
            newInstance.StopEffectImmediately();
        }
        this.active = false;
    }

    IEnumerator fireWeapon()
    {
        GameObject.Instantiate(bulletType, transform.position, transform.rotation);
        StartCoroutine("coolDownWeapon");
        yield return null;
    }

    IEnumerator chargeWeapon()
    {
        EffectBase newInstance = null;
        if (chargeFX != null)
        {
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newInstance = chargeFX.GetInstance(spawnLocation);
            newInstance.PlayEffect();
        }
        yield return new WaitForSeconds(chargeTime);
        if (newInstance != null)
        {
            newInstance.StopEffectImmediately();
        }
        StartCoroutine("fireWeapon");
        yield return null;
    }

    public void Update()
    {

        if (canFire && !active)
        {
            this.active = true;
            StartCoroutine("chargeWeapon");
        }
    }
}
