using System;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace CCAnonymizer.Impl.Maskers;

public class PlayerListMasker: IMasker
{
    public PlayerListMasker()
    {
        PluginServices.AddonLifecycle.RegisterListener(AddonEvent.PreDraw, ["PvPMKSPartyList1", "PvPMKSPartyList3"], OnPlayerListPreDraw);

    }

    private void OnPlayerListPreDraw(AddonEvent type, AddonArgs args)
    {
        try
        {
            if (!PluginServices.Config.MaskPlayerAndCombatLog || !PluginServices.ClientState.IsPvPExcludingDen)
            {
                return;
            }
            unsafe
            {
                var addon = (AtkUnitBase*) args.Addon;

                for (var i = 6; i < 11; i++)
                {
                    var aa = addon->GetComponentByNodeId((uint)i);
                    var tt = aa->GetTextNodeById(21)->GetAsAtkTextNode();
                    var text = tt->GetText().ToString();
                    if (!PluginServices.MatchManager.NeedsToUpdateName(text))
                    {
                        continue;
                    }
                    var combatant = args.AddonName.Equals("PvPMKSPartyList1") ? "Ally" : "Enemy";

                    if (combatant.Equals("Ally") && i == 6) 
                    {
                        // don't mask ourselves
                        // GOTCHA: this assumes that we are always the first in the party list
                        // Some plugins may change the party list order
                        PluginServices.MatchManager.UpdateName(text, text);
                    }
                    else
                    {
                        var newName = combatant + " " + (i - 5);
                        PluginServices.MatchManager.UpdateName(text, newName);
                        tt->SetText(newName); 
                    }
             
                    
                }
            }
        }
        catch (Exception e)
        {
            PluginServices.PluginLog.Error(e, "OnPlayerListPreDraw");
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, ["PvPMKSPartyList1", "PvPMKSPartyList3"], OnPlayerListPreDraw);

    }
}