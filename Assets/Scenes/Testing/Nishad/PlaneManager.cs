using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneManager : MonoBehaviour
{
    [SerializeField] ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    public GameObject spawnablePrefab;

    Camera arCam;
    GameObject spawnedObject;

    Vector2 firstTouch, secondTouch;
    float currentDistance, previousDistance;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (spawnablePrefab == null)
        {
            Debug.Log("No spawnable prefab selected");
            return;
        }
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    spawnedObject = hit.collider.gameObject;
                }

            }
        }
        if (spawnedObject != null)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                spawnedObject.transform.position += translate();
            }
            spawnedObject.transform.rotation = rotate();
            spawnedObject.transform.localScale *= scale();
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            spawnedObject.GetComponent<Rigidbody>().isKinematic = false;
            spawnedObject = null;
        }
    }

    private Vector3 translate()
    {
        Vector3 curPos = arCam.ScreenToViewportPoint(Input.touches[0].position);
        Vector3 lastPos = arCam.ScreenToViewportPoint(Input.touches[0].position - Input.touches[0].deltaPosition);
        Vector3 touchDir = curPos - lastPos;

        Vector3 camUp = arCam.transform.up;
        Vector3 camRight = arCam.transform.right;

        Vector3 rightRelative = camRight * touchDir.x;
        Vector3 upRelative = camUp * touchDir.y;
        Vector3 moveDir = rightRelative + upRelative;

        return moveDir;
    }

    /*NOTE: Needs to be tested for vr kit. Shouldn't use arCam but instead device gyro*/
    private Quaternion rotate()
    {
        Quaternion rotation = arCam.transform.rotation;
        //rotation.x = 0;
        //rotation.y = 0;
        return rotation;
    }

    private float scale()
    {
        float scaleFactor = 1;
        if (Input.touchCount >= 2)
        {
            firstTouch = Input.GetTouch(0).position;
            secondTouch = Input.GetTouch(1).position;

            currentDistance = Vector2.Distance(firstTouch, secondTouch);
            if (previousDistance == 0)
            {
                previousDistance = currentDistance;
            }
            else
            {
                scaleFactor = currentDistance / previousDistance;
                previousDistance = currentDistance;
            }
        }
        else
        {
            previousDistance = 0;
        }
        return scaleFactor;
    }
    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnPosition.y += 0.1f;
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
    }

}
