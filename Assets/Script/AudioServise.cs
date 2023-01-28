using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioServise : MonoBehaviour
{
    [field: SerializeField] public AudioSource WinAudio { get; private set; }
    [field: SerializeField] public AudioSource BackgroundSound { get; private set; }
    [field: SerializeField] public AudioSource PressSpinButton { get; private set; }
    [field: SerializeField] public AudioSource SpinSound { get; private set; }

    [SerializeField] private Button _muteButtonInUpPanel, _muteButtonInMenu;
    [SerializeField] private TMP_Text _soundOnOffText;
    private bool _isMute = false;

    private List<AudioSource> _audioClips = new();

    private void Awake()
    {
        _audioClips.Add(WinAudio);
        _audioClips.Add(BackgroundSound);
        _audioClips.Add(PressSpinButton);
        _audioClips.Add(SpinSound);
    }

    private void OnEnable()
    {
        _muteButtonInUpPanel.onClick.AddListener(ChangeMuteStatus);
        _muteButtonInMenu.onClick.AddListener(ChangeMuteStatus);
    }

    private void OnDisable()
    {
        _muteButtonInUpPanel.onClick.RemoveAllListeners();
        _muteButtonInMenu.onClick.RemoveAllListeners();
    }

    private void ChangeMuteStatus()
    {
        _isMute = !_isMute;

        foreach (var audioClip in _audioClips)
            audioClip.mute = _isMute;

        ChangeMuteUi();
    }

    private void ChangeMuteUi()
    {
        _soundOnOffText.text = _isMute == false ? "On" : "Off";
    }
}
