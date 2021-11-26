using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines conversion for <see cref="IPulsatedHeadMountedDisplayHardware"/> and <see cref="IPulsatedHeadMountedDisplaySoftware"/>.
    /// </summary>
    public static class ConvertPulsatedHeadMountedDisplayInto
    {
        /// <summary>
        /// Converts <see cref="IPulsatedHeadMountedDisplaySoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplayHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IPulsatedHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplayHardware"/> converted from <see cref="IPulsatedHeadMountedDisplaySoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IPulsatedHeadMountedDisplayHardware> Connection(IPulsatedHeadMountedDisplaySoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return new ConnectPulsatedHeadMountedDisplayHardware(software);
        }
        private sealed class ConnectPulsatedHeadMountedDisplayHardware :
            IConnection<IPulsatedHeadMountedDisplayHardware>
        {
            private readonly IPulsatedHeadMountedDisplaySoftware software;

            internal ConnectPulsatedHeadMountedDisplayHardware(IPulsatedHeadMountedDisplaySoftware software)
            {
                this.software = software;
            }

            public ICancellation Connect(IPulsatedHeadMountedDisplayHardware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertPulsatedHeadMountedDisplayInto.Connect(module, software);
            }
        }

        /// <summary>
        /// Converts <see cref="IPulsatedHeadMountedDisplayHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplaySoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IPulsatedHeadMountedDisplayHardware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplaySoftware"/> converted from <see cref="IPulsatedHeadMountedDisplayHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IPulsatedHeadMountedDisplaySoftware> Connection(IPulsatedHeadMountedDisplayHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return new ConnectPulsatedHeadMountedDisplaySoftware(hardware);
        }
        private sealed class ConnectPulsatedHeadMountedDisplaySoftware :
            IConnection<IPulsatedHeadMountedDisplaySoftware>
        {
            private readonly IPulsatedHeadMountedDisplayHardware hardware;

            internal ConnectPulsatedHeadMountedDisplaySoftware(IPulsatedHeadMountedDisplayHardware hardware)
            {
                this.hardware = hardware;
            }

            public ICancellation Connect(IPulsatedHeadMountedDisplaySoftware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertPulsatedHeadMountedDisplayInto.Connect(hardware, module);
            }
        }

        private static ICancellation Connect(IPulsatedHeadMountedDisplayHardware hardware, IPulsatedHeadMountedDisplaySoftware software)
            => CancellationSource.Default
            .Synthesize(hardware.RightHand.HandGrip.Touch.Produce(software.RightHand.HandGrip.Touch))
            .Synthesize(hardware.RightHand.HandGrip.Pull.Produce(software.RightHand.HandGrip.Pull))
            .Synthesize(hardware.LeftHand.HandGrip.Touch.Produce(software.LeftHand.HandGrip.Touch))
            .Synthesize(hardware.LeftHand.HandGrip.Pull.Produce(software.LeftHand.HandGrip.Pull))
            .Synthesize(hardware.RightHand.IndexFinger.Touch.Produce(software.RightHand.IndexFinger.Touch))
            .Synthesize(hardware.RightHand.IndexFinger.Pull.Produce(software.RightHand.IndexFinger.Pull))
            .Synthesize(hardware.LeftHand.IndexFinger.Touch.Produce(software.LeftHand.IndexFinger.Touch))
            .Synthesize(hardware.LeftHand.IndexFinger.Pull.Produce(software.LeftHand.IndexFinger.Pull))
            .Synthesize(hardware.RightHand.Thumb.Touch.Produce(software.RightHand.Thumb.Touch))
            .Synthesize(hardware.RightHand.Thumb.Tilt.Distance.Produce(software.RightHand.Thumb.Tilt.Distance))
            .Synthesize(hardware.RightHand.Thumb.Tilt.Left.Produce(software.RightHand.Thumb.Tilt.Left))
            .Synthesize(hardware.RightHand.Thumb.Tilt.Right.Produce(software.RightHand.Thumb.Tilt.Right))
            .Synthesize(hardware.RightHand.Thumb.Tilt.Forward.Produce(software.RightHand.Thumb.Tilt.Forward))
            .Synthesize(hardware.RightHand.Thumb.Tilt.Backward.Produce(software.RightHand.Thumb.Tilt.Backward))
            .Synthesize(hardware.LeftHand.Thumb.Touch.Produce(software.LeftHand.Thumb.Touch))
            .Synthesize(hardware.LeftHand.Thumb.Tilt.Distance.Produce(software.LeftHand.Thumb.Tilt.Distance))
            .Synthesize(hardware.LeftHand.Thumb.Tilt.Left.Produce(software.LeftHand.Thumb.Tilt.Left))
            .Synthesize(hardware.LeftHand.Thumb.Tilt.Right.Produce(software.LeftHand.Thumb.Tilt.Right))
            .Synthesize(hardware.LeftHand.Thumb.Tilt.Forward.Produce(software.LeftHand.Thumb.Tilt.Forward))
            .Synthesize(hardware.LeftHand.Thumb.Tilt.Backward.Produce(software.LeftHand.Thumb.Tilt.Backward))
            .Build();
    }
}
