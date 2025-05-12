using CCAnonymizer.Interfaces;
using Lumina.Excel.Sheets;

namespace CCAnonymizer.Impl;

public class MatchManager: IMatchManager
{
    public string GetJobOrDefault(string playerName)
    {
        return "REPLACE ME!"; //todo
    }

    public void UpdateJob(string playerName, ClassJob job)
    {
    }

    public string GetJobOrDefault(int job)
    {
        return "PvP Job!!";
    }
}
