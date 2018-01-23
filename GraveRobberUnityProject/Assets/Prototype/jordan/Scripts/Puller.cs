using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Puller : MonoBehaviour
{
    public float pullStrength = 1f;
    public float effectLength = 4f;
    new private bool active;
    private bool instantiated;
    public bool deadly = false;
    private ParticleSystem ps;
    public List<GameObject> pusherToStop;
    private VisionBase sight;
    private AttackBase attack;

    private VisionCube vc;
    public EffectBase ImpactEffect;
    public SoundInformation ActiveSoundEffect;



    // Use this for initialization
    void Start()
    {
        vc = GetComponent<VisionCube>();
        vc.SetSize(new Vector3(1.3f, 2.2f, effectLength + 0.5f));
        vc.SetCenter(new Vector3(0, 0, effectLength / 2));
        active = true;
        ps = GetComponent<ParticleSystem>();
        ps.Play();
        sight = VisionBase.GetVisionByVariant(VisionEnum.Default, this.gameObject);
        attack = AttackBase.GetAttackByVariant(AttackEnum.Puller, this.gameObject);

    
        instantiated = false;

        if (ActiveSoundEffect.SoundFile != null)
        {
            SoundInstance s = ActiveSoundEffect.CreateSoundInstance(this.gameObject);
            s.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            GameObject[] objects = sight.ObjectsInVision();
            if (objects.Length > 0)
            {
                if (objects[0])
                {
                    MovementComponent move = objects[0].GetComponent<MovementComponent>();
                    if (move != null)
                    {
                        move.Move(0, -transform.forward * pullStrength * Time.deltaTime);
                    }

                    Rigidbody rBody = objects[0].GetComponent<Rigidbody>();
                    if (rBody != null)
                    {
                        rBody.AddForce(-transform.forward * pullStrength * 200 * Time.deltaTime);
                        // Debug.Log("Pusher applying force of " + transform.forward + " to " + objectToMove);
                    }
                }
            }
        }
        if (!instantiated)
        {
            instantiated = true;
            vc.SetLayer("Pusher_Puller");
        }
    }

    public void Deactivate()
    {
        active = false;
        ps.Stop();
        ps.Clear();

        if (ActiveSoundEffect.SoundFile != null)
        {
            ActiveSoundEffect.Stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    void OnTriggerEnter(Collider c)
	{GameObject target = c.gameObject;
        if (active)
		{
			
		
           if (target)
				{	
					if (!target.GetComponent<PlayerController>())
							{attack.Damage.DamageAmount =10; 
							attack.Attack(target.transform); }
                          

						if (ImpactEffect != null &&!target.GetComponent<BroodSwarm>())
                            {
                                Vector3 spawnLocation = this.transform.position;
                                EffectBase newInstance = ImpactEffect.GetInstance(spawnLocation);
                                newInstance.transform.rotation = this.transform.rotation;

                                newInstance.PlayEffect();
								}
                        }
                    
				if(!target.GetComponent<BroodSwarm>() && !target.GetComponent<PlayerController>()){
					Deactivate();
                    foreach (GameObject obj in pusherToStop)
                        if (obj != null)
                        {
                            obj.GetComponent<pusher>().Deactivate();
                        }
					}


            
        }
    }

    void OnTriggerStay(Collider c)
    {
		if (c.gameObject.GetComponent<PlayerController>())
        { // is the player
			if(active){Debug.Log("Attacking player");
				attack.Damage.DamageAmount =1;
				attack.Attack (c.gameObject.transform);}

        }
    }
}
