using Lumina.Excel.Sheets;

namespace CCAnonymizer.Interfaces;

public interface IMatchManager
{
    string GetJobOrDefault(string playerName); // returns the passed value if its not detected 
    void UpdateJob(string playerName, ClassJob job);
    
    string GetJobOrDefault(int job);
}