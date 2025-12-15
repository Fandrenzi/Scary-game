using UnityEngine;

public class PagePickup : MonoBehaviour
{

    public float range = 5f;

    public Camera PickUpCam;
    private int PageCount = 0;

    private GameObject GameLogic;

    private void Start()
    {
        GameLogic = GameObject.FindWithTag("GameLogic");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }
    void Pickup()
        {
            RaycastHit hit;
            if (Physics.Raycast(PickUpCam.transform.position, PickUpCam.transform.forward, out hit, range))
            {
                if(hit.collider.CompareTag("Page"))
                {
                    hit.collider.enabled = false;
                    Destroy(hit.collider.gameObject);
                    
                    PageCount++;
                    Debug.Log("Picked up page" + PageCount);
                    GameLogic.GetComponent<GameLogic>().pageCount += 1;

                }

                if (PageCount == 3)
                {
                Debug.Log("Game Won");
                }
            }
        }
    }