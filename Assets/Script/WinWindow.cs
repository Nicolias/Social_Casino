using UnityEngine;
using TMPro;

class WinWindow : MonoBehaviour
{
    [SerializeField] private CreditPanel _creditPanel;
    [SerializeField] private TMP_Text _winCountText;

    private int _winCount;

    public void AccrueWinnings(int prize)
    {
        _winCount++;
        _winCountText.text = _winCount.ToString();

        gameObject.SetActive(true);

        _creditPanel.AddCredits(prize);
    }
}