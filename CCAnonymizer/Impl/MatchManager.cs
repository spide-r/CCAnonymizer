﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using CCAnonymizer.Interfaces;

namespace CCAnonymizer.Impl;

public partial class MatchManager: IMatchManager
{
    private readonly Dictionary<string, string> _playerCombatantMap = new();
    //todo: eventually create support for different ways of displaying names
    public string GetCombatantNameOrDefault(string playerName)
    {
        return IsLastCharNumber(playerName) ? playerName : _playerCombatantMap.GetValueOrDefault(playerName,"Combatant");
    }
    
    private static bool IsLastCharNumber(string name) 
    {
        return name.EndsWith('1') || name.EndsWith('2') || name.EndsWith('3') || name.EndsWith('4')
               || name.EndsWith('5') || name.EndsWith('6') || name.EndsWith('7') || name.EndsWith('8') || name.EndsWith('9') 
               || name.EndsWith('0');
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
