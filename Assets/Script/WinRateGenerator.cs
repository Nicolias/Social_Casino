using UnityEngine;
using System.Collections.Generic;

public class WinRateGenerator
{
    private Dictionary<int, int> _rateBySpineDictionary;
    private int _spinStrik;

    private List<CellsType> _currenCellsList = new();

    private CreditPanel _creditPanel;

    public WinRateGenerator(CreditPanel creditPanel)
    {
        _creditPanel = creditPanel;

        _spinStrik = 0;
        _rateBySpineDictionary = new()
        {
            { 1, 0 },
            { 2, 20 },
            { 3, 50 },
            { 4, 100 },
            { 5, 2500},
            { 6, 0 },
            { 7, 0 },
            { 8, 50 },
            { 9, 80 },
            { 10, 3000 },
            { 11, 0 },
            { 12, 0 },
            { 13, 80 },
            { 14, 100 },
            { 15, 400 }
        };
    }

    public List<CellsType> GetSlotsCombinationList(int slotsCount)
    {
        _spinStrik++;
        _currenCellsList.Clear();

        if (_spinStrik > _rateBySpineDictionary.Count)
            _spinStrik = 1;

        if (_spinStrik % 5 == 0)
        {
            if (Advertisements.Instance.IsRewardVideoAvailable())
            {
                ShowAdAndAccrue(_rateBySpineDictionary[_spinStrik]);
                return null;
            }
            else
            {
                _spinStrik++;
            }
        }

        if (Random.Range(0, 99) < _rateBySpineDictionary[_spinStrik])
            for (int i = 0; i < slotsCount; i++)
                _currenCellsList.Add(CellsType.SevenBar);
        else
            GenerateRandomCombinationCells(slotsCount);

        return _currenCellsList;
    }

    private void GenerateRandomCombinationCells(int slotsCount)
    {
        for (int i = 0; i < slotsCount; i++)
        {
            CellsType currentCell = (CellsType)Random.Range(0, 7);

            if (currentCell != CellsType.SevenBar)
                _currenCellsList.Add(currentCell);
            else
                slotsCount++;
        }
    }

    private void ShowAdAndAccrue(int prize)
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);

        void CompleteMethod(bool completed, string advertiser)
        {
            if (Advertisements.Instance.debug)
            {
                Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
                GleyMobileAds.ScreenWriter.Write("Closed rewarded from: " + advertiser + " -> Completed " + completed);
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
    }   
}
