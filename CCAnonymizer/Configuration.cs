using System;
using Dalamud.Configuration;
using Dalamud.Plugin;

namespace CCAnonymizer;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool MaskNameplates { get; set; } = true;
    public bool MaskPortraits { get; set; } = true;
    public bool MaskPlayerAppearance { get; set; } = true;
    public bool MaskPlayerOutfit { get; set; } = true;
    public bool MaskChat { get; set; } = true;
    
    public bool AbbreviateJobs { get; set; } = false;
    
    public bool UseCustomOutfit { get; set; } = false;
    public bool UseCustomAppearance { get; set; } = false;
    
    public string CustomOutfit { get; set; } = "";
    public string CustomAppearance { get; set; } = "";

    
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