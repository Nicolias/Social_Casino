using System.Collections.Generic;

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
        int winStrik = 0;

        foreach (CellsType cell in slotsCombination)
            if (cell == CellsType.SevenBar)
                winStrik++;

        if (winStrik == slotsCombination.Count)
        {
            _winWindow.AccrueWinnings(currentBet * 2);
            _audioServise.WinAudio.Play();
        }
    }
}
