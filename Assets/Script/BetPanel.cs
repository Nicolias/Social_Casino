using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class BetPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _betCountUnderSlotText, _betCountUpPanelText;
    [SerializeField] private Button _decreaseButton, _addButton;

    private CreditPanel _creditPanel;

    private const int _betChangeStep = 100;
    private int _playerBet = 100;

    public int PlayerBet => _playerBet;

    [Inject]
    public void Construct(CreditPanel creditPanel)
    {
        _creditPanel = creditPanel;
    }

    private void OnEnable()
    {
        _creditPanel.OnCreditChanged += AdjustBet;

        ChangeBetTexts();

        _addButton.onClick.AddListener(AddBet);
        _decreaseButton.onClick.AddListener(DecreaseBet);
    }

    private void OnDisable()
    {
        _creditPanel.OnCreditChanged -= AdjustBet;

        _addButton.onClick.RemoveAllListeners();
        _decreaseButton.onClick.RemoveAllListeners();
    }

    private void AdjustBet()
    {
        if (_playerBet > _creditPanel.CreditsCount)
            _playerBet = _creditPanel.CreditsCount;

        ChangeBetTexts();
    }

    private void AddBet()
    {
        _playerBet += _betChangeStep;

        ChangeBetTexts();
    }

    private void DecreaseBet()
    {
        _playerBet -= _betChangeStep;

        ChangeBetTexts();
    }

    private void ChangeBetTexts()
    {
        _betCountUnderSlotText.text = _playerBet.ToString();
        _betCountUpPanelText.text = _playerBet.ToString();

        CheckButtonsOnInteractable();
    }

    private void CheckButtonsOnInteractable()
    {
        _decreaseButton.interactable = _playerBet - _betChangeStep > 0;
        _addButton.interactable = _playerBet + _betChangeStep <= _creditPanel.CreditsCount;
    }
}
