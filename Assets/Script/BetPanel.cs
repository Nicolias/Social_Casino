using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BetPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _betCountUnderSlotText, _betCountUpPanelText;
    [SerializeField] private Button _decreaseButton, _addButton;
    [SerializeField] private CreditPanel _creditPanel;

    private int _currentBet = 100;
    private const int _betChangeStep = 100;

    public int CurrentBet => _currentBet;

    private void OnEnable()
    {
        ChangeBetTexts();

        _addButton.onClick.AddListener(AddBet);
        _decreaseButton.onClick.AddListener(DecreaseBet);
    }

    private void OnDisable()
    {
        _addButton.onClick.RemoveAllListeners();
        _decreaseButton.onClick.RemoveAllListeners();
    }

    private void AddBet()
    {
        _currentBet += _betChangeStep;

        ChangeBetTexts();
        CheckButtensOnValid();
    }

    private void DecreaseBet()
    {
        _currentBet -= _betChangeStep;

        ChangeBetTexts();
        CheckButtensOnValid();
    }

    private void ChangeBetTexts()
    {
        _betCountUnderSlotText.text = _currentBet.ToString();
        _betCountUpPanelText.text = _currentBet.ToString();
    }

    private void CheckButtensOnValid()
    {
        _decreaseButton.interactable = _currentBet - _betChangeStep > 0;
        _addButton.interactable = _currentBet + _betChangeStep <= _creditPanel.CreditsCount;
    }
}
