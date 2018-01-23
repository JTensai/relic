using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pusher : Triggerable
{
    public float pushStrength = 1f;
    private float currentPushStrength;
    public float effectLength = 4f;
    public float intervalLength = 0f;
    public float intervalOffset = 0f;
    private float intervalTimer = 0f;
    new private bool active = true;
    private ParticleSystem ps;
    private VisionCube vc;
    private bool instantiated = false;
    public EffectBase ImpactEffect;
    public SoundInformation ActiveSoundEffect;
    private bool spinningUp = true;
    private bool windingDown = false;
    private VisionBase sight;

    // Use this for initialization
    void Start()
    {
        vc = GetComponent<VisionCube>();
        vc.SetSize(new Vector3(1, 1, effectLength));
        vc.SetCenter(new Vector3(0, 0, effectLength / 2));
        ps = GetComponent<ParticleSystem>();
        sight = VisionBase.GetVisionByVariant(VisionEnum.Default, this.gameObject);
        if (intervalLength == 0f)
        {
            ps.Play();
            currentPushStrength = pushStrength;
        }
        else
        {
            intervalTimer = intervalOffset;
        }
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if pusher is assigned an interval
        if (intervalLength > 0f)
        {
            if (intervalTimer > intervalLength)
            {
                Swap();
                intervalTimer = 0f;
            }
            else
            {
                intervalTimer += Time.deltaTime;
            }
        }

        if (currentPushStrength < 0f)
        {
            currentPushStrength = 0f;
        }
        if (spinningUp)
        {
            if (currentPushStrength < pushStrength)
            {
                //currentPushStrength += Time.deltaTime*5;
                currentPushStrength = pushStrength;
            }
            else
            {
                active = true;
                spinningUp = false;
            }
        }
        else if (windingDown)
        {
            if (currentPushStrength > 0f)
            {
                //currentPushStrength -= Time.deltaTime*10;
                currentPushStrength = 0f;
            }
            else
            {
                active = false;
                windingDown = false;
            }
        }

        if (active)
        {
            //VisionBase sight = VisionBase.GetVisionByVariant (VisionEnum.Default, this.gameObject);
            //GameObject[] objectsToMove = sight.ObjectsInVision ();
            HashSet<GameObject> allInVision = sight.ObjectsInVisionV3();
            /*	for(int i=0;i<objectsToMove.Length;i++){
                    GameObject objectToMove = objectsToMove[i];*/
            foreach (GameObject objectToMove in allInVision)
            {
                //foreach (GameObject objectToMove in objectsToMove) {
                MovementComponent move = objectToMove.GetComponent<MovementComponent>();
                // Debug.Log(objectToMove.name);
                if (move != null)
                {
                    move.Move(0, transform.forward * currentPushStrength * Time.deltaTime);
                }
                Rigidbody rBody = objectToMove.GetComponent<Rigidbody>();
                if (rBody != null)
                {
                    rBody.AddForce(transform.forward * currentPushStrength * 200 * Time.deltaTime);
                    // Debug.Log("Pusher applying force of " + transform.forward + " to " + objectToMove);
                }
            }
        }

        if (!instantiated)
        {
            instantiated = true;
            vc.SetLayer("Pusher_Puller");
        }
    }

    private void Swap()
    {
        //Debug.Log ("Swapping");
        if (active)
        {
            Deactivate(false);
        }
        else
        {
            Activate();
        }
    }

    public void Deactivate(bool permanent = true)
    {
        //Debug.Log ("Deactivated");
        ps.Stop();
        windingDown = true;
        spinningUp = false;
        if (permanent)
        {
            if (ImpactEffect != null)
            {
                EffectBase newInstance = ImpactEffect.GetInstance(transform.position);
                newInstance.transform.rotation = this.transform.rotation;
                newInstance.PlayEffect();
            }
        }

        if (ActiveSoundEffect.SoundFile != null)
        {
            ActiveSoundEffect.Stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void Activate()
    {
        //Debug.Log ("Activated");s
        ps.Play();
        spinningUp = true;
        windingDown = false;

        if (ActiveSoundEffect.SoundFile != null)
        {
            SoundInstance s = ActiveSoundEffect.CreateSoundInstance(this.gameObject);
            s.Play();
        }
    }

    //public override void TriggeredActions(bool activated) {
    //	Debug.Log ("Switch Pushed");
    //	if (activated)
    //		Activate();
    //}
}
