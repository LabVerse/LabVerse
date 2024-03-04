using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class EquipmentSpawner : ObjectSpawner
{
    bool spawned = false;

    override public bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal)
    {
        if (m_SpawnOptionIndex == -1 || spawned == true) return false;

        if (m_OnlySpawnInView)
        {
            var inViewMin = m_ViewportPeriphery;
            var inViewMax = 1f - m_ViewportPeriphery;
            var pointInViewportSpace = cameraToFace.WorldToViewportPoint(spawnPoint);
            if (pointInViewportSpace.z < 0f || pointInViewportSpace.x > inViewMax || pointInViewportSpace.x < inViewMin ||
                pointInViewportSpace.y > inViewMax || pointInViewportSpace.y < inViewMin)
            {
                return false;
            }
        }

        var newObject = Instantiate(m_ObjectPrefabs[m_SpawnOptionIndex]);
        if (m_SpawnAsChildren)
            newObject.transform.parent = transform;

        newObject.transform.position = spawnPoint;
        EnsureFacingCamera();

        var facePosition = m_CameraToFace.transform.position;
        var forward = facePosition - spawnPoint;
        BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out var projectedForward);
        newObject.transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);

        if (m_ApplyRandomAngleAtSpawn)
        {
            var randomRotation = Random.Range(-m_SpawnAngleRange, m_SpawnAngleRange);
            newObject.transform.Rotate(Vector3.up, randomRotation);
        }

        if (m_SpawnVisualizationPrefab != null)
        {
            var visualizationTrans = Instantiate(m_SpawnVisualizationPrefab).transform;
            visualizationTrans.position = spawnPoint;
            visualizationTrans.rotation = newObject.transform.rotation;
        }
        spawned = true;
        return true;
    }
}