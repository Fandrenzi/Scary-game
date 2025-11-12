using UnityEngine;

public class PagePickup : MonoBehaviour
{

    public float range = 5f;

    public Camera PickUpCam;
    private int PageCount = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }

    void Pickup()
        {
            RaycastHit hit;
            if (Physics.Raycast(PickUpCam.transform.position, PickUpCam.transform.forward, out hit, range))
            {
                Object.Destroy(GameObject.FindWithTag("Page"));
                PageCount++;
                Debug.Log("Picked up page" + PageCount);
                
            }
        }
    }
}



