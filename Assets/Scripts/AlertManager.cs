using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Alert Spawner Singleton that spawns alerts of different types.
/// </summary>
public class AlertManager : MonoBehaviour
{
    public static AlertManager instance { get; private set; }

    public enum ALERT_TYPE
    {
        WARNING,
        INFO,
        ERROR
    }
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject WarningPrefab;
    [SerializeField] private GameObject InfoPrefab;
    [SerializeField] private GameObject ErrorPrefab;
    [SerializeField] private AudioClip WarningSound;
    [SerializeField] private AudioClip InfoSound;
    [SerializeField] private AudioClip ErrorSound;
    private Dictionary<ALERT_TYPE, GameObject> AlertPrefabs = new Dictionary<ALERT_TYPE, GameObject>();
    private Dictionary<ALERT_TYPE, AudioClip> AlertSounds = new Dictionary<ALERT_TYPE, AudioClip>();
    private Stack<GameObject> AlertStack = new Stack<GameObject>(); // store alerts that are to be displayed

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        // Add prefabs & sounds to dictionary with their associated types
        AlertPrefabs.Add(ALERT_TYPE.WARNING, WarningPrefab);
        AlertPrefabs.Add(ALERT_TYPE.INFO, InfoPrefab);
        AlertPrefabs.Add(ALERT_TYPE.ERROR, ErrorPrefab);
        AlertSounds.Add(ALERT_TYPE.WARNING, WarningSound);
        AlertSounds.Add(ALERT_TYPE.INFO, InfoSound);
        AlertSounds.Add(ALERT_TYPE.ERROR, ErrorSound);
    }

    // Spawn a new alert of specified type. Only most recent alert is visible.
    public void CreateAlert (ALERT_TYPE alertType, string message)
    {
        if (!AlertPrefabs.ContainsKey(alertType))
        {
            Debug.LogError("Alert type '" + alertType + "' not found in the dictionary.");
            return;
        }

        DeactivateAllAlerts();
        GameObject AlertInstance = Instantiate(AlertPrefabs[alertType], UI.transform);
        AlertStack.Push(AlertInstance);
        // Make most recent alert visible
        AlertStack.Peek().SetActive(true);

        // Play alert sound (don't know if this works)
        AudioClip AlertSound = AlertSounds[alertType];
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
                AlertMessage.text = message;
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

