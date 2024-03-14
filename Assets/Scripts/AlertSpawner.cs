using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertSpawner : MonoBehaviour
{
    [SerializeField] private GameObject WarningPrefab;
    [SerializeField] private GameObject InfoPrefab;
    [SerializeField] private GameObject ErrorPrefab;
    [SerializeField] private AudioClip WarningSound;
    [SerializeField] private AudioClip InfoSound;
    [SerializeField] private AudioClip ErrorSound;
    private Dictionary<string, GameObject> AlertPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> AlertSounds = new Dictionary<string, AudioClip>();
    private Stack<GameObject> AlertStack = new Stack<GameObject>(); // store alerts that are to be displayed

    void Start()
    {
        // Add prefabs & sounds to dictionary with their associated types
        AlertPrefabs.Add("Warning", WarningPrefab);
        AlertPrefabs.Add("Info", InfoPrefab);
        AlertPrefabs.Add("Error", ErrorPrefab);
        AlertSounds.Add("Warning", WarningSound);
        AlertSounds.Add("Info", InfoSound);
        AlertSounds.Add("Error", ErrorSound);
    }

    // Spawn a new alert of specified type. Only most recent alert is visible.
    public void CreateAlert (string AlertType, string Message)
    {
        if (!AlertPrefabs.ContainsKey(AlertType))
        {
            Debug.LogError("Alert type '" + AlertType + "' not found in the dictionary.");
            return;
        }

        DeactivateAllAlerts();
        GameObject AlertInstance = Instantiate(AlertPrefabs[AlertType]);
        AlertStack.Push(AlertInstance);
        // Make most recent alert visible
        AlertStack.Peek().SetActive(true);

        // Play alert sound (don't know if this works)
        AudioClip AlertSound = AlertSounds[AlertType];
        if (AlertSound != null)
        {
            AudioSource.PlayClipAtPoint(AlertSound, AlertInstance.transform.position);
        }

        // Attach close function to button
        Transform CloseButtonTransform = AlertInstance.transform.Find("UI/Canvas/AlertPanel/Close button");
        if (CloseButtonTransform != null)
        {
            Button CloseButton = CloseButtonTransform.GetComponent<Button>();
            if (CloseButton != null)
            {
                CloseButton.onClick.AddListener(() =>
                {
                    // Remove most recent alert from stack and make previous visible
                    Destroy(AlertStack.Pop());
                    if (AlertStack.Count > 0)
                    {
                        AlertStack.Peek().SetActive(true);
                    }
                });
            }
            else
            {
                Debug.LogError("Close button does not have a Button component.");
            }
        }
        else
        {
            Debug.LogError("Close button not found.");
        }

        // Set message text
        Transform MessageTransform = AlertInstance.transform.Find("UI/Canvas/AlertPanel/Message");
        if (MessageTransform != null)
        {
            TMP_Text AlertMessage = MessageTransform.gameObject.GetComponent<TMP_Text>();
            if (AlertMessage != null)
            {
                AlertMessage.text = Message;
            }
            else
            {
                Debug.LogError("Message does not have a TMP_Text component.");
            }
        }
        else
        {
            Debug.LogError("Message not found.");
        }
    }

    // Set all alerts in stack to inactive
    void DeactivateAllAlerts()
    {
        foreach (var Alert in AlertStack)
        {
            Alert.SetActive(false);
        }
    }
}

