using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ItemSpawner : ObjectSpawner
{
    private void OnEnable()
    {
        ExperimentManager.startExperiment += setObjectPrefabs;
    }

    private void OnDisable()
    {
        ExperimentManager.startExperiment -= setObjectPrefabs;
    }

    private void setObjectPrefabs()
    {
        objectPrefabs = ExperimentManager.instance.selectedExperiment.items.Select((item) => item.model).ToList();
    }
}