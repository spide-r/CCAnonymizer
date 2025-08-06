using System;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace CCAnonymizer.Impl.Maskers;

public class ScoreboardMasker: IMasker
{
    public ScoreboardMasker()
    {
        PluginServices.AddonLifecycle.RegisterListener(AddonEvent.PreDraw, "MKSRecord", OnBattleRecord);

    }

    private static void OnBattleRecord(AddonEvent type, AddonArgs args)
    {
        unsafe
        {
           
            if (!PluginServices.Config.MaskScoreboard || !PluginServices.ClientState.IsPvPExcludingDen)
            {
                return;
            }
            
            try
            {
                var addon = (AtkUnitBase*) args.Addon.Address;
                var listComponent = addon->GetComponentListById(56);
                // node id 5 is name, 6 is world
                for (var i = 0; i < listComponent->GetItemCount(); i++)
                {
                    var item = listComponent->GetItemRenderer(i);
                    var nameNode = item->GetTextNodeById(5)->GetAsAtkTextNode();
                    var worldNode = item->GetTextNodeById(6)->GetAsAtkTextNode();
                    var newName = PluginServices.MatchManager.GetCombatantNameOrDefault(nameNode->GetText());
                    if (newName.Equals(nameNode->GetText())) continue;
                    nameNode->SetText(PluginServices.MatchManager.GetCombatantNameOrDefault(nameNode->GetText()));
                    worldNode->SetText("Homeworld");

                }
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "BattleLogMasker - OnBattleLog");
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, "MKSRecord", OnBattleRecord);

    }
}