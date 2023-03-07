using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _confidentiallyButton;

    private void OnEnable()
    {
        _confidentiallyButton.onClick.AddListener(OpenConfidentiallyURL);
    }

    private void OnDisable()
    {
        _confidentiallyButton.onClick.RemoveAllListeners();
    }

    private void OpenConfidentiallyURL()
    {
        Application.OpenURL("https://pastebin.com/jxULxxdk");
    }
}
