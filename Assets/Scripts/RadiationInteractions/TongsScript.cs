using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TongsScript : MonoBehaviour
{
    private GameObject m_currentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        m_currentMaterial = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if tongs are upside-down
        // NB: Needs to be improved, other rotations should cause drop
        Vector3 rotation = transform.rotation.eulerAngles;
        bool upsideDown = false; // add in actual logic here
        if (upsideDown) DropObject();

    }

    public void Pickup(GameObject obj)
    {
        // Snap to end of tongs
        Vector3 position = gameObject.GetComponent<Collider>().bounds.center;
        obj.transform.position = new Vector3(position.x, position.y+0.01f, position.z);

        // Set tongs as parent
        obj.transform.parent = transform;

        // Set current material as object
        m_currentMaterial = obj;
    }

    /// <summary>
    /// Drop the material currently held in the tongs
    /// </summary>
    public void DropObject()
    {
        // Do nothing if no material is held
        if (!m_currentMaterial) return;

        // Detach the material from the tongs
        m_currentMaterial.transform.parent = null;
        m_currentMaterial.transform.position = new Vector3(m_currentMaterial.transform.position.x, m_currentMaterial.transform.position.y - 0.03f, m_currentMaterial.transform.position.z); ;
        m_currentMaterial = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (m_currentMaterial) return; 
        if(!other.gameObject.CompareTag("Radioactive")) return;

        // If gameobject is a radioactive sample:

        // Drop current sample, replace with new
        DropObject();
        Pickup(other.gameObject);
    }
}
