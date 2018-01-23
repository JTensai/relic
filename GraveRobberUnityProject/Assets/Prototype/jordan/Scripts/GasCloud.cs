using UnityEngine;
using System.Collections;

public class GasCloud : MonsterBase
{

    private AttackBase a;
    private ParticleSystem ps;
    new private bool active = false;
    private float remaining;
    //private bool residueExists = false;
    public float duration = 10.0f;
    public float damageDelay = 1.2f;

    // Use this for initialization
    void Start()
    {
        a = GetComponent<AttackBase>();
        ps = GetComponent<ParticleSystem>();
        remaining = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated)
        {
            remaining -= Time.deltaTime;
            // Debug.Log ("Remaing damage time: " + remaining);

            if (damageDelay > 0)
            {
                damageDelay -= Time.deltaTime;
                if (damageDelay < 0)
                {
                    active = true;
                }
            }

            if (remaining < 0)
            {
                Deactivate();
            }
            if (!ps.IsAlive())
            {
                Destroy(ps.gameObject);
                Destroy(this);
            }
        }
    }



    void Deactivate()
    {
        active = false;
        ps.Stop();
    }

    void OnTriggerStay(Collider col)
    {
        if (active)
        {
            GameObject hitObject = col.gameObject;
            a.Attack(hitObject.transform);
        }
    }
}
