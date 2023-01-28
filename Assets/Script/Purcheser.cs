using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

public class Purcheser : MonoBehaviour
{
    [SerializeField] private CreditPanel _creditPanel;

    private AdsServise _adsServise;

    [Inject]
    public void Construct(AdsServise adsServise)
    {
        _adsServise = adsServise;
    }

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "20k.golds":
                AddCoins(20000);
                break;

            case "50k.golds":
                AddCoins(50000);
                break;

            case "200k.golds":
                AddCoins(200000);
                break;

            case "7.days.without.ads":
                _adsServise.BlockAdsOnPeriods(7);
                break;

            case "30.days.without.ads":
                _adsServise.BlockAdsOnPeriods(30);
                break;
        }
    }

    private void AddCoins(int coinsCount)
    {
        _creditPanel.AddCredits(coinsCount);
    }
}
