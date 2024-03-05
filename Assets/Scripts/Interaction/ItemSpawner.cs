using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

/// <summary>
/// Spawns items in the scene.
/// </summary>
public class ItemSpawner : ObjectSpawner
{
    private bool m_spawnedStartingItems = false;
    private GameObject m_combinedItemsPrefab;

    private void OnEnable()
    {
        ExperimentManager.startExperiment += setObjectPrefabs;
    }

    private void OnDisable()
    {
        ExperimentManager.startExperiment -= setObjectPrefabs;
    }

    /// <summary>
    /// Sets the object prefabs to the selected experiment items.   
    /// </summary>
    private void setObjectPrefabs()
    {
        // Updates the objectPregabs list with the models from the selected experiment items.
        objectPrefabs = ExperimentManager.instance.selectedExperiment.items.Select((item) => item.model).ToList();
        m_combinedItemsPrefab = ExperimentManager.instance.selectedExperiment.combinedItems;
    }

    public override bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal)
    {
        // Only spawn objects if the experiment has started, or a spawn option has been selected.
        if (m_spawnedStartingItems && m_SpawnOptionIndex == -1) return false;

        if (m_OnlySpawnInView)
        {
            float inViewMin = m_ViewportPeriphery;
            float inViewMax = 1f - m_ViewportPeriphery;
            Vector3 pointInViewportSpace = cameraToFace.WorldToViewportPoint(spawnPoint);
            if (pointInViewportSpace.z < 0f || pointInViewportSpace.x > inViewMax || pointInViewportSpace.x < inViewMin ||
                pointInViewportSpace.y > inViewMax || pointInViewportSpace.y < inViewMin)
            {
                return false;
            }
        }

        // Spawn the combined items prefab if the experiment has started.
        // Otherwise, spawn the selected item from the objectPrefabs list.
        GameObject newObject = m_spawnedStartingItems ? Instantiate(m_ObjectPrefabs[m_SpawnOptionIndex]) : Instantiate(m_combinedItemsPrefab);
        if (m_SpawnAsChildren)
            newObject.transform.parent = transform;

        newObject.transform.position = spawnPoint;
        EnsureFacingCamera();

        Vector3 facePosition = m_CameraToFace.transform.position;
        Vector3 forward = facePosition - spawnPoint;
        BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out Vector3 projectedForward);
        newObject.transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);

        if (m_ApplyRandomAngleAtSpawn)
        {
            float randomRotation = Random.Range(-m_SpawnAngleRange, m_SpawnAngleRange);
            newObject.transform.Rotate(Vector3.up, randomRotation);
        }

        if (m_SpawnVisualizationPrefab != null)
        {
            Transform visualizationTrans = Instantiate(m_SpawnVisualizationPrefab).transform;
            visualizationTrans.position = spawnPoint;
            visualizationTrans.rotation = newObject.transform.rotation;
        }

        InvokeObjectSpawned(newObject);
        m_spawnedStartingItems = true;
        return true;
    }
}