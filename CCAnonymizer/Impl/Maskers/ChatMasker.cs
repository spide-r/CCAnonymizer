using System;
using CCAnonymizer.Interfaces;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace CCAnonymizer.Impl.Maskers;

public class ChatMasker: IMasker
{
    public ChatMasker()
    {
        PluginServices.ChatGui.ChatMessage += OnMessage;
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        PluginServices.ChatGui.ChatMessage -= OnMessage;
    }
    
    private void OnMessage(XivChatType type, int timestamp, ref SeString sender, ref SeString message, ref bool isHandled)
    {
        try
        {

            if (!PluginServices.Config.MaskChat || !PluginServices.ClientState.IsPvPExcludingDen)
            {
                return;
            }
            try
            {
                //todo: rewrite 
                if (type is not (XivChatType.Party or XivChatType.Alliance)) return;
                var job = "";
                foreach (var payload in sender.Payloads)
                {
                    if (payload.Type == PayloadType.Player)
                    {
                        isHandled = true;
                    }
                    
                    var isNumber = int.TryParse(payload.ToString(), out _);

                    if (payload.Type != PayloadType.Icon || payload.ToString()!.Contains("CrossWorld") ||
                        isNumber) continue;
                    var raw = payload.ToString();
                    if (raw != null) job = raw.Substring(7);
                }

                if (!isHandled) return;
                
                var chat = new XivChatEntry
                {
                    Message = message,
                    Name = job,
                    Type = type
                };
                PluginServices.ChatGui.Print(chat);
            }
            catch (Exception e)
            {
                PluginServices.PluginLog.Error(e, "Issue in chat masker!");
            }
        }
        catch (Exception e)
        {
            PluginServices.PluginLog.Error(e, "OnMessage");
        }
    }
}