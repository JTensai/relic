using UnityEngine;
using System.Collections;
using Pathfinding;

public class BreakingPot : MonoBehaviour
{
//	private Animator animator;
	private bool isBroken;

    private GameObject spawnEffect = null;
    public GameObject[] PrefabList = new GameObject[6];
    public int[] ProbabilityList = new int[6];

    public Vector3 spawnOffset = Vector3.up * 0.1f;
	public GameObject lid;
	public GameObject unbrokenGlass;

	private InteractableComponent ic;
	private Vector3 initPos;
//	private Vector3 finalPos;
//	private float elapsedTime = 0;
	public float collapseTime = 0.5f;
	public AnimationCurve DropCurve;

	public EffectBase BreakFX;
	public SoundInformation BreakSoundEffect;

	// Use this for initialization
	void Start ()
	{
//        animator = GetComponentInChildren<Animator>();
		ic = gameObject.GetComponent<InteractableComponent>();
		if (ic != null){
	        ic.OnLargeNotify += HandleOnLargeNotify;
//	        ic.IsInteractable = true;
		}
        
        int randomNum = Random.Range(1, 100);
        int cumulative = 0;
        int index = 0;
        foreach (int probability in ProbabilityList)
        {
            cumulative += probability;
            if (randomNum < cumulative)
            {
                spawnEffect = PrefabList[index];
                break;
            }
            index++;
        }
//		if (lid != null)
//			initPos = lid.transform.localPosition;
//		finalPos = new Vector3 (0, -0.45f, 0);
	}

    void HandleOnLargeNotify(InteractableLargeNotifyEventData data)
	{
    }


	IEnumerator Break(Collider c)
	{
		if (!isBroken)
		{
			isBroken = true;
			unbrokenGlass.GetComponent<MeshRenderer>().enabled = false;
			lid.GetComponent<MeshRenderer>().enabled = false;

			if(BreakFX != null){
				EffectBase newInstance = BreakFX.GetInstance(c.gameObject.transform.position);
				newInstance.transform.LookAt(this.transform.position);
				newInstance.transform.position = this.transform.position;
				newInstance.transform.rotation = Quaternion.Euler(new Vector3(0, newInstance.transform.eulerAngles.y, 0));
				newInstance.PlayEffect();
			}

			playBreakSound();

			//animator.speed = 0.45f;
			//animator.SetTrigger("Break");

			// Destroy(this.gameObject);		
			//while (elapsedTime < collapseTime){
				//elapsedTime += Time.deltaTime;
				//float delta = DropCurve.Evaluate(elapsedTime / collapseTime);
				//lid.transform.localPosition = Vector3.Lerp(initPos, finalPos, delta);
				//yield return null;
			//}

			Spawn();
		}
		yield return null;
	}

	void OnTriggerEnter(Collider c){
	//	if (c.gameObject.CompareTag("Player")){
			StartCoroutine("Break",c);
//			Break ();
			UpdateStatTracker();
		//}
	}

    public void Spawn()
    {
        if (spawnEffect != null)
        {
            GameObject obj = (GameObject)Instantiate(spawnEffect, transform.position + spawnOffset, transform.rotation);
			obj.name = "PotSpawnling";
            if (obj.GetComponent<MonsterBase>() != null)
            {
                obj.GetComponent<MonsterBase>().ActivateEntity();
            }
        }
    }

    public void UpdateStatTracker()
    {
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        if (inventories.Length > 0)
        {
//            string type = gameObject.name;
            StatTracker st = inventories[0].GetComponent<StatTracker>();
            st.InteractableHit(this.gameObject);
        }
    }
	
	public void playBreakSound() {
		if(BreakSoundEffect != null && BreakSoundEffect.SoundFile != null) {
			SoundInstance s = BreakSoundEffect.CreateSoundInstance(this.gameObject);
			s.Play();
		}
	}
}
