using System;
using Dalamud.Configuration;
using Dalamud.Plugin;

namespace CCAnonymizer;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool MaskNameplates { get; set; } = true;
    public bool MaskTargeting { get; set; } = true;
    public bool MaskScoreboard { get; set; } = true;
    public bool MaskPlayerListAndCombatLog { get; set; } = true;
    public bool MaskPortraits { get; set; } = true;
    public bool MaskPlayerAppearance { get; set; } = true;
    public bool MaskPlayerOutfit { get; set; } = true;
    public bool MaskChat { get; set; } = true;
    public bool MaskSelf { get; set; } = false;
    public bool AbbreviateJobs { get; set; } = false;
    
    [NonSerialized]
    private IDalamudPluginInterface? _pluginInterface;
    
    public void Initialize(IDalamudPluginInterface pluginInterface)
    {
        this._pluginInterface = pluginInterface;
    }
    public void Save()
    {
        _pluginInterface?.SavePluginConfig(this);
    }
}