using UnityEngine;
using TMPro;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private CreditPanel _creditPanel;
    [SerializeField] private TMP_Text _winCountText, _prizeCountText;

    private int _winCount;

    public void AccrueWinnings(int prize)
    {
        _winCount++;
        _winCountText.text = _winCount.ToString();

        gameObject.SetActive(true);

        _prizeCountText.text = prize.ToString();
        _creditPanel.AddCredits(prize);
    }
}