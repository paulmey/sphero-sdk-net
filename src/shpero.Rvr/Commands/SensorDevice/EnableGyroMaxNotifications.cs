﻿using shpero.Rvr.Protocol;

namespace shpero.Rvr.Commands.SensorDevice
{
    [Command(CommandId, DeviceId)]
    public class EnableGyroMaxNotifications : Command
    {
        private readonly bool _enable;

        public const byte CommandId = 0x0F;

        public const DeviceIdentifier DeviceId = DeviceIdentifier.Sensor;

        public EnableGyroMaxNotifications(bool enable)
        {
            _enable = enable;
        }

        public override Message ToMessage()
        {
            var header = new Header(
                commandId: CommandId,
                targetId: 0x02,
                deviceId: DeviceId,
                sourceId: ApiTargetsAndSources.ServiceSource,
                sequence: GetSequenceNumber(),
                flags: Flags.DefaultRequestWithNoResponseFlags);
            return new Message(header, _enable ? new byte []{0x01} : new byte[] { 0x00 });
        }
    }
}
