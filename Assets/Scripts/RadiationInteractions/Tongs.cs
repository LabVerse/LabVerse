using UnityEngine;

public class Tongs : MonoBehaviour
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
        // NB: Needs to be tested & improved

        float rotationX = transform.eulerAngles.x; //default 270, keep between 180 and 360
        //float rotationZ = transform.eulerAngles.z; //default 0, keep ?
        if (rotationX > 360) rotationX = 360 - rotationX;
        else if (rotationX < -360) rotationX = 360 + rotationX;
        //if (rotationZ > 180) rotationZ = 360 - rotationZ;
    
        bool upsideDown = !(200 < rotationX && rotationX < 330);
        if (upsideDown) DropObject();
    }

    private void OnEnable()
    {
        AlertManager.instance.CreateAlert(AlertManager.ALERT_TYPE.INFO, "Use the tongs to pick up the radioactive samples.");
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
        m_currentMaterial.GetComponent<Rigidbody>().useGravity = false;
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
        m_currentMaterial.transform.rotation = Quaternion.identity;
        m_currentMaterial.GetComponent<Rigidbody>().useGravity = true;
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
