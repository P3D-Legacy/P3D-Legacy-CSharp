using System.Collections.Generic;

namespace P3D.Legacy.Core.World
{
    public interface IRadioStation
    {
        string Music { get; set; }
        string Name { get; set; }
        decimal ChannelMin { get; set; }
        bool CanListen { get; }

        List<IRadioStation> OverwriteChannels(List<IRadioStation> channelList);
        bool IsInterfering(decimal frequenz);
        List<string> GenerateText();
    }
}