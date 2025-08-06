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
                    var addon = (AtkUnitBase*) drawArgs.Addon.Address;
                    if (addon is null)
                    {
                        return;
                    }

                    var node = addon->GetTextNodeById(10);

                    switch (args.AddonName)
                    {
                        case "_FocusTargetInfo":
                            node->SetText("Focus Target");
                            break;
                        // only check for node 7 if we're targeting someone
                        case "_TargetInfoMainTarget":
                        {
                            var targetName = node->GetText().ToString();
                            if (targetName.IndexOf('«') != -1)
                            {
                                var newName = targetName[targetName.IndexOf('«')..]; // «Job»
                                node->SetText(newName);
                            }
                           
                            var targetTargetNode = addon->GetTextNodeById(7); // if your target is targeting something
                            var targetTargetName = targetTargetNode->GetText().ToString();
                            targetTargetNode->SetText(PluginServices.MatchManager.GetCombatantNameOrDefault(targetTargetName));
                            break;
                        }
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