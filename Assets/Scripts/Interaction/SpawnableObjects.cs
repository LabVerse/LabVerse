using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SpawnableObjects : MonoBehaviour
{
    public GameObject[] spawnableObjects;
    [SerializeField] GameObject scrollMenuContainer;

    // Start is called before the first frame update
    void Start()
    {
        scrollMenuContainer = GameObject.Find("Scroll Menu Container");
        InstantiateList(spawnableObjects);
    }

    private void InstantiateList(GameObject[] spawnableObjects)
    {
        for(int i = 0; i < spawnableObjects.Length; i++) {             
            GameObject go = Instantiate(spawnableObjects[i], scrollMenuContainer.transform);
            go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y, 1f);
            go.transform.SetParent(scrollMenuContainer.transform);
        }
    }
}
