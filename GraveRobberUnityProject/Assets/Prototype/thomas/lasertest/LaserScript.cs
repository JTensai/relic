using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	LineRenderer line;
	public Transform reflectedPoint;

	void Start () 
	{
		line = gameObject.GetComponent<LineRenderer>();
		//line.enabled = false;
		line.enabled = true;

		//StartCoroutine ("FireLaser");
	}

	void Update () 
	{
		/*if (Input.GetButtonDown ("Fire1")) 
		{
			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
		}*/

		line.renderer.material.mainTextureOffset = new Vector2(Time.time,Time.time);
		
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		
		line.SetPosition(0, ray.origin);
		line.SetPosition(1, hit.point);
		line.SetVertexCount (2);
		
		int point = 2;
		while (hit.collider.CompareTag ("Mirror")) 
		{
			line.SetVertexCount (point + 1);
			ray = new Ray (hit.point, Vector3.Reflect (ray.direction, hit.normal.normalized));
			Physics.Raycast (ray, out hit);
			line.SetPosition (point, hit.point);
			point++;
		} 
	}

	IEnumerator FireLaser()
	{
		//line.enabled = true;

		/*while (true) 
		{
			line.renderer.material.mainTextureOffset = new Vector2(0, Time.time);

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);

			line.SetPosition(0, ray.origin);
			line.SetPosition(1, hit.point);

			int point = 2;
			if(hit.collider.CompareTag("Mirror"))
			{
				line.SetVertexCount(point + 1);
				reflectedPoint.transform.position = Vector3.Reflect(ray.origin, hit.collider.transform.forward);
				Ray tempRay = new Ray(reflectedPoint.transform.position, reflectedPoint.transform.forward);
				Physics.Raycast(tempRay, out hit);
				line.SetPosition(point, hit.point);
				point++;
			}*/

			yield return null;
		//}

		//line.enabled = false;
	}
}
