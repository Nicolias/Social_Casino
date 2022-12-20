using TMPro;
using UnityEngine;
using System;

class CreditPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _creditsPanelText;

    [SerializeField] private int _creditsCount;
    public int CreditsCount => _creditsCount;

    private void OnEnable()
    {
        UpdateCreditsUIText();
    }

    public void AddCredits(int creditsCount)
    {
        _creditsCount += creditsCount;

        UpdateCreditsUIText();
    }

    public void DecreaseCredits(int creditsCount)
    {
        if (_creditsCount - creditsCount < 0)
            throw new InvalidOperationException("Ставка меньше чем количество денег");

        _creditsCount -= creditsCount;

        UpdateCreditsUIText();
    }

    private void UpdateCreditsUIText()
    {
        _creditsPanelText.text = _creditsCount.ToString();
    }
}