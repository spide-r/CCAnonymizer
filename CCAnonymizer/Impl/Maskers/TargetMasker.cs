using System;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace CCAnonymizer.Impl.Maskers;

public class TargetMasker: IMasker
{
    public TargetMasker()
    {
        PluginServices.AddonLifecycle.RegisterListener(AddonEvent.PreDraw, ["_TargetInfoMainTarget", "_FocusTargetInfo"], OnPreDraw);
    }

    private void OnPreDraw(AddonEvent type, AddonArgs args)
    {
        if (!PluginServices.ClientState.IsPvPExcludingDen || !PluginServices.Config.MaskTargeting)
        {
            return;
        }
        unsafe
        {
            try
            {
                if (args is AddonDrawArgs drawArgs)
                {
                    var a = (AtkUnitBase*) drawArgs.Addon;
                    if (a is null)
                    {
                        return;
                    }

                    var node = a->GetTextNodeById(10);
                    var targetName = node->GetText().ToString();
                    node->SetText(PluginServices.MatchManager.GetCombatantNameOrDefault(targetName));
                    if(args.AddonName.Equals("_TargetInfoMainTarget"))
                    {
                        var targetTargetNode = a->GetTextNodeById(7); // if your target is targeting something
                        var targetTargetName = targetTargetNode->GetText().ToString();
                        targetTargetNode->SetText(PluginServices.MatchManager.GetCombatantNameOrDefault(targetTargetName));
                    }
           
                }
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "OnPreDraw");
            }
        }
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, ["_TargetInfoMainTarget", "_FocusTargetInfo"], OnPreDraw);


    }
}