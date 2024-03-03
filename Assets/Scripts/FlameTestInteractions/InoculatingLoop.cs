using Codice.CM.WorkspaceServer.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class InoculatingLoop : MonoBehaviour
{
    public GameObject[] metalSamples;
    bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        isColliding=false;
    }

    //When the inoculating loop collides with a metal source, remove any other child metals and add the current one
    public void OnTriggerEnter(Collider other)
    {
        if (isColliding) { return; }
        isColliding=true;
        if (other.gameObject.CompareTag("Metal"))
        {
            int i = 0;

            //remove existing metal sample
            foreach (Transform child in transform)
            {
                //determine which metal was on the inoculation loop
                if (child.gameObject.name == "CopperSample") { i = 1; }
                else if (child.gameObject.name == "LithiumSample") { i = 2; }
                else if (child.gameObject.name == "IronSample") { i = 3; }
                else { continue; }
                Destroy(child.gameObject);
                //fail the given stage
                //StageManager.instance.FinishStage(i, false);
            }

            //determine which metal should be added to the loop
            if (other.gameObject.name == "CopperSource") { i = 1; }
            else if (other.gameObject.name == "LithiumSource") { i = 2; }
            else if(other.gameObject.name == "IronSource") { i = 3; }

            //spawn the metal on the loop
            GameObject newChild = Instantiate(metalSamples[i-1], new Vector3(0, 0, 0), Quaternion.identity);
            newChild.transform.parent = transform;

            Vector3 colliderPos = new Vector3(0.00465f, 0, 0);
            newChild.transform.localPosition = colliderPos;

            //start the correct stage of the experiment
            //StageManager.instance.EnterStage(i);
        }
    }
}
