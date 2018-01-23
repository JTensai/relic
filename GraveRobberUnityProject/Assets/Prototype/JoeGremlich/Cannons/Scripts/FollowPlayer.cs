using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FollowPlayer : CannonMovement
{

    private VisionBase sight;
    private bool playerInSight = false;

    new public bool active = true;
    public float smooth = 1.5f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sight = VisionBase.GetVisionByVariant(VisionEnum.Default, this.gameObject);
        if (active && sight != null)
        {
            bool playerFound = false;
            GameObject[] objects = sight.ObjectsInVision();
            for (int i = 0; i < objects.Length; i++)
            {
                GameObject g = objects[i];
                Vector3 lookAt = new Vector3(g.transform.position.x, g.transform.position.y + 0.5f, g.transform.position.z);
                if (g.tag == "Player")
                {
                    playerFound = true;
                    SmoothLookAt(lookAt);
                    canFire = true;
                }
            }
            playerInSight = playerFound;
        }
    }

    void SmoothLookAt(Vector3 position)
    {
        // Create a vector from the camera towards the player.
        Vector3 relPlayerPosition = position - transform.position;

        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }

    public bool isPlayerInSight()
    {
        return playerInSight;
    }
}
