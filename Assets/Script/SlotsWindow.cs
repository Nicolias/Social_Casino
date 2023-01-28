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

    private AudioServise _audioServise;

    private int? _currentBet;

    [Inject]
    public void Construct(  CombinationInterpreter combinationInterpreter, 
                            WinRateGenerator winRateGenerator, 
                            CreditPanel creditPanel, 
                            BetPanel betPanel,
                            AudioServise audioServise)
    {
        _combinationInterpreter = combinationInterpreter;
        _winRateGenerator = winRateGenerator;
        _creditPanel = creditPanel;
        _betPanel = betPanel;

        _audioServise = audioServise;
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
        _spinButton.onClick.AddListener(TrySpinSlots);
        StartCoroutine(LoadBaner());

        IEnumerator LoadBaner()
        {
            while (Advertisements.Instance.IsBannerOnScreen() == false)
            {
                yield return new WaitForSeconds(0.1f);
                Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
            }
        }
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveAllListeners();
        Advertisements.Instance.HideBanner();
    }

    private void TrySpinSlots()
    {
        _audioServise.PressSpinButton.Play();

        if (_betPanel.PlayerBet == 0)
            return;

        List<CellsType> slotsCombination = _winRateGenerator.GetSlotsCombinationList(_slots.Count);

        if (slotsCombination != null)
        {
            _currentBet = _betPanel.PlayerBet;

            _spinButton.interactable = false;
            _creditPanel.DecreaseCredits((int)_currentBet);
            StartCoroutine(StartSpinAnimation(slotsCombination));
        }
    }

    private IEnumerator StartSpinAnimation(List<CellsType> slotsCombination)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            var currentSlot = _slots[i];
            currentSlot.SetStopCell(slotsCombination[i]);
        }

        _audioServise.SpinSound.Play();

        foreach (var slot in _slots)
        {
            slot.SpinSlot();
            yield return new WaitForSeconds(0.33f);
        }

        foreach (var slot in _slots)
        {
            slot.CanStop();

            yield return new WaitUntil(() => slot.IsStoped);
        }
        
        _combinationInterpreter.InterpritateCombination(slotsCombination, (int)_currentBet);

        _spinButton.interactable = true;

        _currentBet = null;
    }
}
