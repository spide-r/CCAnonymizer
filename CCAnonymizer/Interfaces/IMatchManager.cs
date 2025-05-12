using Lumina.Excel.Sheets;

namespace CCAnonymizer.Interfaces;

public interface IMatchManager
{
    string GetCombatantNameOrDefault(string playerName); // returns the passed value if its not detected 
    void UpdateName(string playerName, string replacement);
    
    bool NeedsToUpdateName(string playerName);
}