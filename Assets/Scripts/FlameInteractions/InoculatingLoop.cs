using Codice.CM.WorkspaceServer.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

/// <summary>
/// Controls the inoculating loop prefab 'picking up' a sample of whatever metal it collides with
/// </summary>
public class InoculatingLoop : MonoBehaviour
{
    [SerializeField] private GameObject[] metalSamples;
    private bool m_isColliding;

    // Start is called before the first frame update
    void Start()
    {
        m_isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_isColliding=false;
    }

    ///<summary>
    /// When the inoculating loop collides with a metal source, remove any other child metals and add the current one
    ///<\summary>
    public void OnTriggerEnter(Collider other)
    {
        // Prevent detecting multiple collisions per update
        // NB: Possibly better ways to do this than dropping all other collisions,
        //     but right now performance hit is too great to leave, so improve in future
        if (m_isColliding) { return; }
        m_isColliding=true;

        Debug.Log("Collided with" + other.gameObject.name);
        if (other.gameObject.CompareTag("Metal"))
        {
            int i = 0;

            // Remove existing metal sample
            foreach (Transform child in transform)
            {
                // Determine which metal was on the inoculation loop
                // NB: Consider improvements for this code in future
                if (child.gameObject.name == "CopperSample(Clone)") { i = 1; } 
                else if (child.gameObject.name == "LithiumSample(Clone)") { i = 2; }
                else if (child.gameObject.name == "IronSample(Clone)") { i = 3; }
                else { continue; }

                // Remove the metal from the inoculating loop
                Destroy(child.gameObject);
                
                //Fail the given stage, as the metal was put aside
                StageManager.instance.FinishStage(i, false);
            }

            // Determine which metal should be added to the loop
            if (other.gameObject.name == "CopperSource") { i = 1; }
            else if (other.gameObject.name == "LithiumSource") { i = 2; }
            else if(other.gameObject.name == "IronSource") { i = 3; }

            // Spawn the metal on the loop
            GameObject newChild = Instantiate(metalSamples[i-1], new Vector3(0, 0, 0), Quaternion.identity);
            newChild.transform.parent = transform;

            Vector3 colliderPos = new Vector3(0.00465f, 0, 0);
            newChild.transform.localPosition = colliderPos;

            // Start the correct stage of the experiment
            StageManager.instance.EnterStage(i);
        }
    }
}
