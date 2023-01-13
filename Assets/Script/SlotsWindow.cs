using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SlotsWindow : MonoBehaviour
{
    [SerializeField] private List<SlotColumn> _slots;
    [SerializeField] private Button _spinButton;

    private BetPanel _betPanel;
    private CreditPanel _creditPanel;

    private WinRateGenerator _winRateGenerator;
    private CombinationInterpreter _combinationInterpreter;

    private int? _currentBet;

    [Inject]
    public void Construct(  CombinationInterpreter combinationInterpreter, 
                            WinRateGenerator winRateGenerator, 
                            CreditPanel creditPanel, 
                            BetPanel betPanel)
    {
        _combinationInterpreter = combinationInterpreter;
        _winRateGenerator = winRateGenerator;
        _creditPanel = creditPanel;
        _betPanel = betPanel;
    }

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
            _currentBet = _betPanel.PlayerBet;

            _creditPanel.DecreaseCredits((int)_currentBet);
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
        
        _combinationInterpreter.InterpritateCombination(slotsCombination, (int)_currentBet);

        _spinButton.interactable = true;

        _currentBet = null;
    }
}
