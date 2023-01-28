using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AdsServise: MonoBehaviour
{
    [SerializeField] private Button _showAdsInShopButton;

    private CreditPanel _creditPanel;

    private DateTime? _lastBlockAdsDay
    {
        get
        {
            string data = PlayerPrefs.GetString("lastBlockAdsDay", null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastBlockAdsDay", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastBlockAdsDay");
        }
    }

    private DateTime? _nextCreditsAccureDay
    {
        get
        {
            string data = PlayerPrefs.GetString("nextCreditsAccureDay", null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return DateTime.UtcNow;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("nextCreditsAccureDay", value.ToString());
            else
                PlayerPrefs.DeleteKey("nextCreditsAccureDay");
        }
    }

    public bool IsAdsBlocked { get; private set; }

    [Inject]
    public void Construct(CreditPanel creditPanel)
    {
        _creditPanel = creditPanel;
    }

    private void OnEnable()
    {
        _showAdsInShopButton.onClick.AddListener(() => ShowAdAndAccrue(100));

        StartCoroutine(BlockAdsStateUpdater());
    }

    private void OnDisable()
    {
        _showAdsInShopButton.onClick.RemoveAllListeners();

        StopAllCoroutines();
    }

    public void ShowAdAndAccrue(int prize)
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);

        void CompleteMethod(bool completed, string advertiser)
        {
            if (completed == true)
            {
                _creditPanel.AddCredits(prize);
            }
            else
            {
                //no reward
            }
        }
    }

    public void BlockAdsOnPeriods(int days)
    {
        IsAdsBlocked = true;

        _lastBlockAdsDay = _lastBlockAdsDay.HasValue ? _lastBlockAdsDay.Value.AddDays(days) : DateTime.UtcNow.AddDays(days);
        _nextCreditsAccureDay = DateTime.UtcNow;

        StartCoroutine(BlockAdsStateUpdater());
    }

    private IEnumerator BlockAdsStateUpdater()
    {
        if (_lastBlockAdsDay == null)
            yield break;

        while (_lastBlockAdsDay.HasValue)
        {
            UpdateAdsBlockState();
            UpdateAccurePrizeState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateAdsBlockState()
    {
        IsAdsBlocked = false;

        if (_lastBlockAdsDay.HasValue)
        {
            if (DateTime.UtcNow > _lastBlockAdsDay.Value)
                _lastBlockAdsDay = null;
            else
                IsAdsBlocked = true;
        }
    }

    private void UpdateAccurePrizeState()
    {
        if (DateTime.UtcNow.Day < _nextCreditsAccureDay.Value.Day)
            return;

        _creditPanel.AddCredits(1000);
        _nextCreditsAccureDay = DateTime.UtcNow.AddDays(1);
    }
}

