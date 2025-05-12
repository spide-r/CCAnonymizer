using System.Collections.Generic;
using System.Text.RegularExpressions;
using CCAnonymizer.Interfaces;

namespace CCAnonymizer.Impl;

public partial class MatchManager: IMatchManager
{
    private readonly Dictionary<string, string> _playerCombatantMap = new();
    public string GetCombatantNameOrDefault(string playerName)
    {
        return IsLastCharNumber(playerName) ? playerName : _playerCombatantMap.GetValueOrDefault(playerName, playerName);
    }
    
    [GeneratedRegex("[0-9]")]
    private static partial Regex CharRegex();

    private static bool IsLastCharNumber(string name) 
    {
        return CharRegex().IsMatch(name[name.Length].ToString());
    }

    public void UpdateName(string playerName, string replacement)
    {
        _playerCombatantMap[playerName] = replacement;
    }

    public bool NeedsToUpdateName(string playerName)
    {
        return !IsLastCharNumber(playerName);
    }
}
