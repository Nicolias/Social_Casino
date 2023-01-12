using TMPro;
using UnityEngine;
using System;

public class CreditPanel : MonoBehaviour
{
    public event Action OnCreditChanged;

    [SerializeField] private TMP_Text _creditsPanelText;

    [SerializeField] private int _creditsCount;
    public int CreditsCount => _creditsCount;

    private void OnEnable()
    {
        UpdateCreditsUIText();
    }

    public void AddCredits(int creditsCount)
    {
        if (creditsCount < 0)
            throw new InvalidOperationException("Начисляется отрицательное число");

        _creditsCount += creditsCount;

        UpdateCreditsUIText();
        OnCreditChanged?.Invoke();
    }

    public void DecreaseCredits(int creditsCount)
    {
        if (_creditsCount - creditsCount < 0)
            throw new InvalidOperationException("Недостаточно средств");

        _creditsCount -= creditsCount;

        UpdateCreditsUIText();
        OnCreditChanged?.Invoke();
    }

    private void UpdateCreditsUIText()
    {
        _creditsPanelText.text = _creditsCount.ToString();
    }
}