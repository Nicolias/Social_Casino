using System.Collections.Generic;
using UnityEngine;

public class CombinationInterpreter
{
    private WinWindow _winWindow;
    private AudioServise _audioServise;

    public CombinationInterpreter(WinWindow winWindow, AudioServise audioServise)
    {
        _winWindow = winWindow;
        _audioServise = audioServise;
    }

    public void InterpritateCombination(List<CellsType> slotsCombination, int currentBet)
    {
        int largestWinStrik = 0;
        int winStrik = 1;

        for (int i = 1; i < slotsCombination.Count; i++)
        {
            winStrik = slotsCombination[i - 1] == slotsCombination[i] ? winStrik += 1 : 1;

            if (winStrik > largestWinStrik)
                largestWinStrik = winStrik;
        }

        if (largestWinStrik == 3)
            AccurePrize(currentBet * 1.3f);

        if (largestWinStrik == 4)
            AccurePrize(currentBet * 1.4f);

        if (largestWinStrik == 5)
            AccurePrize(currentBet * 2f);
    }

    private void AccurePrize(float prize)
    {
        _winWindow.AccrueWinnings((Mathf.RoundToInt(prize)));
        _audioServise.WinAudio.Play();
    }
}
