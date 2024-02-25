using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SpawnedObjectManager : MonoBehaviour
{
    ObjectSpawner objectSpawner;
    GameObject inputPhoneManager;
    [SerializeField] GameObject spawnableObject;
    void Start()
    {
        objectSpawner = GameObject.Find("Object Spawner").GetComponent<ObjectSpawner>();
        inputPhoneManager = GameObject.Find("Input Phone Manager");
        /*if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            //inputPhoneManager.SetActive(false);
        }*/
    }
  

    public void OnObjectSelectedFromMenu()
    {
        Debug.Log("Object selected from menu");
        objectSpawner.objectPrefabs[0] = spawnableObject;
        /*if (inputPhoneManager.activeSelf)
        {*/
            inputPhoneManager.GetComponent<InputPhoneManager>().spawnablePrefab = spawnableObject;
        //}
    }
}
