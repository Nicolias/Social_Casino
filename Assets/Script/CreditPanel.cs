using TMPro;
using UnityEngine;
using System;

public class CreditPanel : MonoBehaviour
{
    public event Action OnCreditChanged;

    [SerializeField] private TMP_Text _creditsPanelText;

    private int _creditsCount;
    public int CreditsCount => _creditsCount;

    private void Awake()
    {
        LoadCoinsCount();
    }

    public void AddCredits(int creditsCount)
    {
        if (creditsCount < 0)
            throw new InvalidOperationException
                ("Начисляется отрицательное число");

        _creditsCount += creditsCount;

        SaveNewCoinsCount();
    }

    public void DecreaseCredits(int creditsCount)
    {
        if (_creditsCount - creditsCount < 0)
            throw new InvalidOperationException("Недостаточно средств");

        _creditsCount -= creditsCount;

        SaveNewCoinsCount();        
    }

    private void LoadCoinsCount()
    {
        _creditsCount = PlayerPrefs.HasKey("Credits") ? PlayerPrefs.GetInt("Credits") : 2000;

        UpdateCreditsUIText();
    }

    private void SaveNewCoinsCount()
    {
        PlayerPrefs.SetInt("Credits", _creditsCount);

        UpdateCreditsUIText();
        OnCreditChanged?.Invoke();
    }

    private void UpdateCreditsUIText()
    {
        _creditsPanelText.text = _creditsCount.ToString();
    }
}