using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class InputPhoneManager : MonoBehaviour
{
    [SerializeField] GameObject spawnedObject;
    Camera arCam;
    // Start is called before the first frame update
    void Start()
    {
        arCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;


        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Touch Began");
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 200, false);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit Something");
                IPointerClickHandler clickHandler = hit.collider.gameObject.GetComponent<IPointerClickHandler>();
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                clickHandler.OnPointerClick(pointerEventData);
                //Instantiate(spawnedObject,hit.transform);
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {

        }
    }
}
