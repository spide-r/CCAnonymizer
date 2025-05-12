using System;
using System.Collections.Generic;
using CCAnonymizer.Interfaces;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Glamourer.Api.Enums;
using Glamourer.Api.IpcSubscribers;

namespace CCAnonymizer.Impl.Maskers;

public class AppearanceMasker : IMasker // Masks both outfit and Character appearance
{
    
    private readonly HashSet<ulong> _appliedUsers = [];

    private readonly ApplyState _applyState;

    public AppearanceMasker(IDalamudPluginInterface pluginInterface)
    {
        _applyState = new ApplyState(pluginInterface);
        PluginServices.Framework.Update += OnFrameworkUpdate;
        PluginServices.ClientState.TerritoryChanged += TerritoryChanged;

    }

    private void TerritoryChanged(ushort obj)
    {
        _appliedUsers.Clear();
    }

    private void OnFrameworkUpdate(IFramework framework)
    {
        if (!PluginServices.ClientState.IsPvPExcludingDen)
        {
            return;
        }
     
        try
        {
            ApplyMasking();
        }
        catch (Exception e)
        {
            PluginServices.PluginLog.Error("AppearanceMasker#OnFrameworkUpdate", e);
        }
        
        
    }

    private void ApplyMasking()
    {
        foreach (var gameObject in PluginServices.ObjectTable)
        {
            try
            {
                if (gameObject.ObjectKind != ObjectKind.Player) continue;
                if (_appliedUsers.Contains(gameObject.GameObjectId)) continue;


                if (PluginServices.ClientState.LocalPlayer != null &&
                    gameObject.GameObjectId == PluginServices.ClientState.LocalPlayer.GameObjectId && !PluginServices.Config.MaskSelf)
                {
                    continue;
                }

                var pc = (IPlayerCharacter) gameObject;

                var job = pc.ClassJob.Value.Abbreviation.ToString();
                var glamour = Glamours.GetGlamour(job);
                if (PluginServices.Config.MaskPlayerAppearance)
                {
                    ApplyCustomization(gameObject.ObjectIndex, Glamours.GetDefaultAppearance());

                }

                if (PluginServices.Config.MaskPlayerOutfit)
                {
                    ApplyOutfit(gameObject.ObjectIndex, glamour);

                }

                _appliedUsers.Add(gameObject.GameObjectId);
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error("AppearanceMasker#ApplyMasking", e);
            }
        }
    }
    private void ApplyCustomization(ushort objectIndex, string state)
    {
        _applyState.Invoke(state, objectIndex, 0, ApplyFlag.Customization | ApplyFlag.Once);
    }
    
    private void ApplyOutfit(ushort objectIndex, string outfit)
    {
        _applyState.Invoke(outfit, objectIndex, 0, ApplyFlag.Equipment | ApplyFlag.Once); 
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.Framework.Update -= OnFrameworkUpdate;
        PluginServices.ClientState.TerritoryChanged -= TerritoryChanged;
    }
}