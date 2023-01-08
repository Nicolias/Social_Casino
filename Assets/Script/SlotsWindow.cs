using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsWindow : MonoBehaviour
{
    [SerializeField] private List<SlotColumn> _slots;

    [SerializeField] private BetPanel _betPanel;
    [SerializeField] private CreditPanel _creditPanel;

    [SerializeField] private Button _spinButton;

    [SerializeField] private WinWindow _winWindow;

    private WinRateGenerator _winRateGenerator = new();

    private void Awake()
    {
        if (Advertisements.Instance.UserConsentWasSet())
        {
            Advertisements.Instance.Initialize();
        }
        else
        {
            Advertisements.Instance.SetUserConsent(false);
            Advertisements.Instance.Initialize();
        }
    }

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(SpinSlots);
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveAllListeners();
        Advertisements.Instance.HideBanner();
    }

    private void SpinSlots()
    {
        List<CellsType> slotsCombination = _winRateGenerator.GetSlotsCombinationList(_slots.Count);

        if (slotsCombination != null)
        {
            _creditPanel.DecreaseCredits(_betPanel.CurrentBet);
            StartCoroutine(StartSpinAnimation(slotsCombination));
        }
    }

    private IEnumerator StartSpinAnimation(List<CellsType> slotsCombination)
    {
        _spinButton.interactable = false;

        int index = 0;

        foreach (var slot in _slots)
            slot.SpinSlot();

        while (index < _slots.Count)
        {
            var currentSlot = _slots[index];
            currentSlot.StopSlotOnCell(slotsCombination[index]);
            index++;

            yield return new WaitUntil(() => currentSlot.IsStoped != false);
        }

        CombinationInterpreter combinationInterpreter = new();
        combinationInterpreter.InterpritateCombination(slotsCombination, _winWindow, _betPanel.CurrentBet);

        _spinButton.interactable = true;
    }
}
