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
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, "PvPMKSBattleLog", OnBattleLog);
    }
    
    private void OnBattleLog(AddonEvent type, AddonArgs args) {
        unsafe
        {
            if (!PluginServices.Config.MaskBattleLog || !PluginServices.ClientState.IsPvPExcludingDen)
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
                    tt->SetText("Dummy Value " + i); //todo: update w/ job
                }
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "BattleLogMasker - OnBattleLog");
            }
        }
    }
}