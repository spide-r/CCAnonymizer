using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace CCAnonymizer.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration _configuration;
    public ConfigWindow(CCAnonymizerPlugin plugin) : base("CCAnonymizer", ImGuiWindowFlags.NoResize)
    {
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(300, 200),
            MaximumSize = new Vector2(500, 500)
        };
        SizeCondition = ImGuiCond.Always;

        _configuration = PluginServices.Config;
    }

    public void Dispose() { }
    
    public override void Draw()
    {

        var abbreviateJobs = _configuration.AbbreviateJobs;
        var maskChat = _configuration.MaskChat;
        var maskNameplates = _configuration.MaskNameplates;
        var maskPortraits = _configuration.MaskPortraits;
        var maskScoreboard = _configuration.MaskScoreboard;
        var maskSelf = _configuration.MaskSelf;
        var maskTargeting = _configuration.MaskTargeting;
        var maskAppearance = _configuration.MaskPlayerAppearance;
        var maskPlayerOutfit = _configuration.MaskPlayerOutfit;
        var maskPlayerListAndCombatLog = _configuration.MaskPlayerListAndCombatLog;
        
        if (ImGui.Checkbox("Mask Player Nameplates", ref maskNameplates))
        {
            _configuration.MaskNameplates = maskNameplates;
            _configuration.Save();
        }

        if (maskNameplates)
        {
            if (ImGui.Checkbox("Abbreviate Jobs When Hiding Nameplates", ref abbreviateJobs))
            {
                _configuration.AbbreviateJobs = abbreviateJobs;
                _configuration.Save();
            }
        }
        
        if (ImGui.Checkbox("Mask Player Chat", ref maskChat))
        {
            _configuration.MaskChat = maskChat;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Mask Portrait Introductions", ref maskPortraits))
        {
            _configuration.MaskPortraits= maskPortraits;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Mask Scoreboard", ref maskScoreboard))
        {
            _configuration.MaskScoreboard= maskScoreboard;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Mask Self", ref maskSelf))
        {
            _configuration.MaskSelf= maskSelf;
            _configuration.Save();
        }
        if (ImGui.Checkbox("Mask Targeting", ref maskTargeting))
        {
            _configuration.MaskTargeting = maskTargeting;
            _configuration.Save();
        }
        
        if (ImGui.Checkbox("Mask Player List and Combat Log", ref maskPlayerListAndCombatLog))
        {
            _configuration.MaskPlayerListAndCombatLog = maskPlayerListAndCombatLog;
            _configuration.Save();
        }
        
        ImGui.TextWrapped("The following settings require glamourer:");
        if (ImGui.Checkbox("Mask Player Appearance", ref maskAppearance))
        {
            _configuration.MaskPlayerAppearance = maskAppearance;
            _configuration.Save();
        }
        
        ImGui.SameLine();
        
        if (ImGui.Checkbox("Mask Player Outfits", ref maskPlayerOutfit))
        {
            _configuration.MaskPlayerOutfit = maskPlayerOutfit;
            _configuration.Save();
        }
        ImGui.Separator();
        ImGui.BulletText("Thank you to Mutant Standard (CC BY-NC-SA) - https://mutant.tech");
    }
}
