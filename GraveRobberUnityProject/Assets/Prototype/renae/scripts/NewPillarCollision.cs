using UnityEngine;
using System.Collections;
using Pathfinding;

public class NewPillarCollision : MonoBehaviour
{

    private AttackBase attack;

    private bool isFalling;
    private string rotPosition;
    private WalkablePillarv3 pillar;
    private bool finishedFall;
    private float collision_delay;
    private BoxCollider trigger;
    private BoxCollider wall1 = null;
    private BoxCollider wall2 = null;
    private GraphUpdateScene gus;
    new private BoxCollider collider;

    // Use this for initialization
    void Start()
    {
        attack = GetComponent<AttackBase>();
        rotPosition = transform.rotation.ToString("f7");
        isFalling = false;
        pillar = this.GetComponentInParent<WalkablePillarv3>();
        collision_delay = 0.05f;
        collider = this.GetComponent<BoxCollider>();
        gus = this.GetComponentInParent<GraphUpdateScene>();//transform.Find ("PillarProtector").GetComponent<GraphUpdateScene> ();//this.GetComponent<GraphUpdateScene> ();
    }

    public void SpawnWalls()
    {
        if (wall1 == null || wall2 == null)
        {
            wall1 = (BoxCollider)this.gameObject.AddComponent("BoxCollider");
            wall2 = (BoxCollider)this.gameObject.AddComponent("BoxCollider");

            wall1.size = new Vector3(0.01f, 1, 3);
            wall2.size = new Vector3(0.01f, 1, 3);

            wall1.center = new Vector3(-0.5f, 0, -1);
            wall2.center = new Vector3(0.5f, 0, -1);

        }
        else
        {
            wall1.enabled = true;
            wall2.enabled = true;
        }
    }

    public void DestroyWalls()
    {
        if (wall1 != null || wall2 != null)
        {
            wall1.enabled = false;
            wall2.enabled = false;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (isFalling)
        {
            if (other.gameObject.GetComponent<HealthComponent>() != null && other.gameObject.GetComponent<PlayerBase>() == null)
            {
                // hit something with hp, kill it
                //Debug.Log("Monster was hit: " + other.gameObject.name);
                attack.Attack(other.transform);
            }
            else if (other.gameObject.name != "PillarBase" && other.gameObject.GetComponent<PlayerBase>() == null)
            {
                Debug.Log("Falling pillar hit: " + other.gameObject.name);
                // hit something without hp, stop moving
                pillar.SetFreeze(true);
                FinishFall();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (finishedFall)
        {
            if (collider.gameObject.GetComponent<PlayerBase>() != null && pillar.makesBridge)
            {
                SpawnWalls();
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (finishedFall)
        {
            if (collider.gameObject.GetComponent<PlayerBase>() != null && pillar.makesBridge)
            {
                DestroyWalls();
            }
        }
    }

    public void StartFalling()
    {
        isFalling = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            // This collision delay was needed because of a rounding error in Unity. there wasn't enough difference between rotation during the first frame of the fall
            if (collision_delay < 0)
            {
                // check to see if it is still falling
                if (pillar.FallingPivotPoint.transform.rotation.ToString("f7") != rotPosition)
                {
                    rotPosition = pillar.FallingPivotPoint.transform.rotation.ToString("f7");
                }
                else
                {
                    FinishFall();
                }
            }
            else
            {
                collision_delay -= Time.deltaTime;
            }
        }
    }

    void FinishFall()
    {
        isFalling = false;

        if (this.GetComponentInParent<WalkablePillarv3>().crumbles)
        {
            GameObject rubble1 = pillar.RemainingRubble[0];
            rubble1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject rubble2 = pillar.RemainingRubble[1];
            rubble2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            rubble1 = (GameObject)Instantiate(rubble1, transform.position, transform.localRotation);
            rubble2 = (GameObject)Instantiate(rubble2, transform.position + new Vector3(0, 0, 1), transform.localRotation);
            Destroy(this.GetComponentInParent<WalkablePillarv3>().gameObject);
            rubble1.rigidbody.AddForce(transform.forward * 1000);
        }
        else
        {
            // Permanent Pillar
            //AstarPath.active.UpdateGraphs(this.collider.bounds);
            //BoxCollider collider = new BoxCollider();
            collider.bounds.Expand(.5f);
            gus.Apply();
            collider.bounds.Expand(-.5f);
            //Destroy (this.GetComponentInParent<BoxCollider> ());

            Debug.Log("Permanent pillar finish fall");
        }

        if (this.GetComponentInParent<WalkablePillarv3>().makesBridge)
        {
            // Create the trigger to tell when the player is standing on the fallen pillar
            trigger = (BoxCollider)this.gameObject.AddComponent("BoxCollider");
            trigger.isTrigger = true;
            trigger.center = new Vector3(0f, 0f, -0.75f);
            trigger.size = new Vector3(1f, .85f, 0.5f);
            // "Always on" walls after pillar falls
            //	SpawnWalls();
        }

        Debug.Log("Finished Fall");
        finishedFall = true;
        this.GetComponentInParent<WalkablePillarv3>().OnFinishedFalling();
    }
}
