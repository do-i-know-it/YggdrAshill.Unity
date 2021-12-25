using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ConvertHeadMountedDisplayInto
    {
        /// <summary>
        /// Converts <see cref="IHeadMountedDisplayProtocol"/> into <see cref="ITransmission{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/>.
        /// </summary>
        /// <param name="protocol">
        /// <see cref="IHeadMountedDisplayProtocol"/> to convert.
        /// </param>
        /// <param name="configuration">
        /// <see cref="IHeadMountedDisplayConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/> converted from <see cref="IHeadMountedDisplayProtocol"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="protocol"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IHeadMountedDisplaySoftware> Transmission(IHeadMountedDisplayProtocol protocol, IHeadMountedDisplayConfiguration configuration)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new TransmitHeadMountedDisplay(configuration, protocol);
        }
        private sealed class TransmitHeadMountedDisplay :
            ITransmission<IHeadMountedDisplaySoftware>
        {
            private readonly IEmission emission;

            private readonly IConnection<IHeadMountedDisplaySoftware> connection;

            internal TransmitHeadMountedDisplay(IHeadMountedDisplayConfiguration configuration, IHeadMountedDisplayProtocol protocol)
            {
                emission = Conduct(configuration, protocol.Software);

                connection = Connection(protocol.Hardware);
            }

            public ICancellation Connect(IHeadMountedDisplaySoftware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return connection.Connect(module);
            }

            public void Emit()
            {
                emission.Emit();
            }
        }
        private static IEmission Conduct(IHeadMountedDisplayConfiguration configuration, IHeadMountedDisplaySoftware software)
            => EmissionSource.Default
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.HandGrip.Touch, software.RightHand.HandGrip.Touch))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.HandGrip.Pull, software.RightHand.HandGrip.Pull))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.HandGrip.Touch, software.LeftHand.HandGrip.Touch))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.HandGrip.Pull, software.LeftHand.HandGrip.Pull))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.IndexFinger.Touch, software.RightHand.IndexFinger.Touch))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.IndexFinger.Pull, software.RightHand.IndexFinger.Pull))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.IndexFinger.Touch, software.LeftHand.IndexFinger.Touch))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.IndexFinger.Pull, software.LeftHand.IndexFinger.Pull))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.Thumb.Touch, software.RightHand.Thumb.Touch))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.Thumb.Tilt, software.RightHand.Thumb.Tilt))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.Thumb.Touch, software.LeftHand.Thumb.Touch))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.Thumb.Tilt, software.LeftHand.Thumb.Tilt))
            .Synthesize(ConductSignalTo.Consume(configuration.Origin.Position, software.Origin.Position))
            .Synthesize(ConductSignalTo.Consume(configuration.Origin.Rotation, software.Origin.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.Head.Pose.Position, software.Head.Pose.Position))
            .Synthesize(ConductSignalTo.Consume(configuration.Head.Pose.Rotation, software.Head.Pose.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.Head.Direction, software.Head.Direction))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.Pose.Position, software.RightHand.Pose.Position))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.Pose.Rotation, software.RightHand.Pose.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.Pose.Position, software.LeftHand.Pose.Position))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.Pose.Rotation, software.LeftHand.Pose.Rotation))
            .Build();

        /// <summary>
        /// Converts <see cref="IHeadMountedDisplaySoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplayHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplayHardware"/> converted from <see cref="IHeadMountedDisplaySoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IHeadMountedDisplayHardware> Connection(IHeadMountedDisplaySoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return new ConnectHeadMountedDisplayHardware(software);
        }
        private sealed class ConnectHeadMountedDisplayHardware :
            IConnection<IHeadMountedDisplayHardware>
        {
            private readonly IHeadMountedDisplaySoftware software;

            internal ConnectHeadMountedDisplayHardware(IHeadMountedDisplaySoftware software)
            {
                this.software = software;
            }

            public ICancellation Connect(IHeadMountedDisplayHardware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertHeadMountedDisplayInto.Connect(module, software);
            }
        }

        /// <summary>
        /// Converts <see cref="IHeadMountedDisplayHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/> converted from <see cref="IHeadMountedDisplayHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IHeadMountedDisplaySoftware> Connection(IHeadMountedDisplayHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return new ConnectHeadMountedDisplaySoftware(hardware);
        }
        private sealed class ConnectHeadMountedDisplaySoftware :
            IConnection<IHeadMountedDisplaySoftware>
        {
            private readonly IHeadMountedDisplayHardware hardware;

            internal ConnectHeadMountedDisplaySoftware(IHeadMountedDisplayHardware hardware)
            {
                this.hardware = hardware;
            }

            public ICancellation Connect(IHeadMountedDisplaySoftware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertHeadMountedDisplayInto.Connect(hardware, module);
            }
        }

        private static ICancellation Connect(IHeadMountedDisplayHardware hardware, IHeadMountedDisplaySoftware software)
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
            .Synthesize(hardware.RightHand.Thumb.Tilt.Produce(software.RightHand.Thumb.Tilt))
            .Synthesize(hardware.LeftHand.Thumb.Touch.Produce(software.LeftHand.Thumb.Touch))
            .Synthesize(hardware.LeftHand.Thumb.Tilt.Produce(software.LeftHand.Thumb.Tilt))
            .Synthesize(hardware.Origin.Position.Produce(software.Origin.Position))
            .Synthesize(hardware.Origin.Rotation.Produce(software.Origin.Rotation))
            .Synthesize(hardware.Head.Pose.Position.Produce(software.Head.Pose.Position))
            .Synthesize(hardware.Head.Pose.Rotation.Produce(software.Head.Pose.Rotation))
            .Synthesize(hardware.Head.Direction.Produce(software.Head.Direction))
            .Synthesize(hardware.RightHand.Pose.Position.Produce(software.RightHand.Pose.Position))
            .Synthesize(hardware.RightHand.Pose.Rotation.Produce(software.RightHand.Pose.Rotation))
            .Synthesize(hardware.LeftHand.Pose.Position.Produce(software.LeftHand.Pose.Position))
            .Synthesize(hardware.LeftHand.Pose.Rotation.Produce(software.LeftHand.Pose.Rotation))
            .Build();

        /// <summary>
        /// Converts <see cref="IHeadMountedDisplayHardware"/> into <see cref="IPulsatedHeadMountedDisplayHardware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IHeadMountedDisplayHardware"/> to convert.
        /// </param>
        /// <param name="pulsation">
        /// <see cref="IHeadMountedDisplayPulsation"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IPulsatedHeadMountedDisplayHardware"/> converted from <see cref="IHeadMountedDisplayHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="pulsation"/> is null.
        /// </exception>
        public static IPulsatedHeadMountedDisplayHardware PulsatedHeadMountedDisplay(IHeadMountedDisplayHardware hardware, IHeadMountedDisplayPulsation pulsation)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (pulsation == null)
            {
                throw new ArgumentNullException(nameof(pulsation));
            }

            return new PulsatedHeadMountedDisplayHardware(hardware, pulsation);
        }
        private sealed class PulsatedHeadMountedDisplayHardware :
            IPulsatedHeadMountedDisplayHardware
        {
            internal PulsatedHeadMountedDisplayHardware(IHeadMountedDisplayHardware hardware, IHeadMountedDisplayPulsation pulsation)
            {
                LeftHand = ConvertHandControllerInto.PulsatedHandController(hardware.LeftHand, pulsation.LeftHand);

                RightHand = ConvertHandControllerInto.PulsatedHandController(hardware.RightHand, pulsation.RightHand);
            }

            public IPulsatedHandControllerHardware LeftHand { get; }

            public IPulsatedHandControllerHardware RightHand { get; }
        }
    }
}
