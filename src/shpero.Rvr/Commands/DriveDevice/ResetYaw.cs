﻿using shpero.Rvr.Protocol;

namespace shpero.Rvr.Commands.DriveDevice
{
    [Command(CommandId, DeviceId)]
    public class ResetYaw : Command
    {
        public const byte CommandId = 0x06;

        public const DeviceIdentifier DeviceId = DeviceIdentifier.Drive;

        public override Message ToMessage()
        {
            var header = new Header(
                commandId: CommandId,
                targetId: 0x02,
                deviceId: DeviceId,
                sourceId: ApiTargetsAndSources.ServiceSource,
                sequence: GetSequenceNumber(),
                flags: Flags.DefaultRequestWithNoResponseFlags);
            return new Message(header);
        }
    }
}