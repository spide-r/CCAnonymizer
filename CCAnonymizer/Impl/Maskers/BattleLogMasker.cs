using System;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace CCAnonymizer.Impl.Maskers;

public class BattleLogMasker: IMasker
{
    public BattleLogMasker()
    {
        PluginServices.AddonLifecycle.RegisterListener(AddonEvent.PreDraw, "PvPMKSBattleLog", OnBattleLog);
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, "PvPMKSBattleLog", OnBattleLog);
    }
    
    private void OnBattleLog(AddonEvent type, AddonArgs args) {
        unsafe
        {
            if (!PluginServices.Config.MaskPlayerAndCombatLog || !PluginServices.ClientState.IsPvPExcludingDen)
            {
                return;
            }
            try
            {
                var addon = (AtkUnitBase*)args.Addon;
                for (int i = 7; i < 13; i++)
                {
                    var aa = addon->GetComponentByNodeId((uint)i);
                    var tt = aa->GetTextNodeById(8)->GetAsAtkTextNode();
                    var text = tt->GetText().ToString();
                    tt->SetText(PluginServices.MatchManager.GetCombatantNameOrDefault(text)); 
                }
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "BattleLogMasker - OnBattleLog");
            }
        }
    }
}