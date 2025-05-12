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
            if (!PluginServices.Config.MaskPlayerList || !PluginServices.ClientState.IsPvPExcludingDen)
            {
                return;
            }
            unsafe
            {
                var addon = (AtkUnitBase*) args.Addon;

                for (int i = 6; i < 11; i++)
                {
                    var aa = addon->GetComponentByNodeId((uint)i);
                    var tt = aa->GetTextNodeById(21)->GetAsAtkTextNode();
                    var combatant = args.AddonName.Equals("PvPMKSPartyList1") ? "Ally" : "Enemy";
                    tt->SetText(combatant + " " + (i - 5)); 
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
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, ["PvPMKSPartyList1", "PvPMKSPartyList3"], OnPlayerListPreDraw);

    }
}