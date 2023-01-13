using UnityEngine;
using TMPro;
using Zenject;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _winCountText, _prizeCountText;

    private CreditPanel _creditPanel;

    private int _winCount;

    [Inject]
    public void Constract(CreditPanel creditPanel)
    {
        _creditPanel = creditPanel;
    }

    public void AccrueWinnings(int prize)
    {
        _winCount++;
        _winCountText.text = _winCount.ToString();

        gameObject.SetActive(true);

        _prizeCountText.text = prize.ToString();
        _creditPanel.AddCredits(prize);
    }
}