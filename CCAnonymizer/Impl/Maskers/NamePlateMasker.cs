using System;
using System.Collections.Generic;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Gui.NamePlate;

namespace CCAnonymizer.Impl.Maskers;

public class NamePlateMasker: IMasker
{
    public NamePlateMasker()
    {
        PluginServices.NamePlateGui.OnNamePlateUpdate += OnNameplateUpdate;
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.NamePlateGui.OnNamePlateUpdate -= OnNameplateUpdate;
    }

    private static void OnNameplateUpdate(INamePlateUpdateContext context, IReadOnlyList<INamePlateUpdateHandler> handlers)
    {
        try
        {
            if (!PluginServices.ClientState.IsPvPExcludingDen || !PluginServices.Config.MaskNameplates)
            {
                return;
            }
            foreach (var plate in handlers)
            {
                if(plate.PlayerCharacter == null) continue;
                if(plate.BattleChara == null) continue;
                if(PluginServices.ObjectTable.LocalPlayer == null) continue;
                if(plate.PlayerCharacter.GameObjectId == PluginServices.ObjectTable.LocalPlayer.GameObjectId) continue;
            
                if (plate.Name.ToString().Contains('《')) continue; //《 and 》 - No need to mask this plate
            
                var j = plate.BattleChara.ClassJob.Value;
                var jobStr = PluginServices.Config.AbbreviateJobs ? j.Abbreviation.ToString() : j.NameEnglish.ToString();
                plate.Name = '《' + jobStr + '》';
                plate.Title = "";
                plate.FreeCompanyTag = "";
                plate.RemoveField(NamePlateStringField.Title);
                plate.RemoveField(NamePlateStringField.FreeCompanyTag);
            }    
        }
        catch (Exception e)
        {
            PluginServices.PluginLog.Error(e, "OnNameplateUpdate");
        }
        
    }
}