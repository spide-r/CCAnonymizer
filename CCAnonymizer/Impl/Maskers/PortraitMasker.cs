using System;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace CCAnonymizer.Impl.Maskers;

public class PortraitMasker: IMasker
{
    public PortraitMasker()
    {
        PluginServices.AddonLifecycle.RegisterListener(AddonEvent.PreDraw, ["PvPMKSIntroduction", "PvPMKSRankRatingFunction"], OnPreDraw);
    }

    private void OnPreDraw(AddonEvent type, AddonArgs args)
    {
        if (!PluginServices.ClientState.IsPvPExcludingDen || !PluginServices.Config.MaskPortraits)
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
                    a->SetAlpha(0);
                }
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "OnPreDraw - Portrait Masker");
            }
        }
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.AddonLifecycle.UnregisterListener(AddonEvent.PreDraw, ["PvPMKSIntroduction", "PvPMKSRankRatingFunction"], OnPreDraw);

        
    }
}