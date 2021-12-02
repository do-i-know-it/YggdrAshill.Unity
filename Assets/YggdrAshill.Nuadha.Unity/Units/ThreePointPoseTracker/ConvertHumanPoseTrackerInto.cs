using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ConvertHumanPoseTrackerInto
    {
        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerProtocol"/> into <see cref="ITransmission{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="protocol">
        /// <see cref="IHumanPoseTrackerProtocol"/> to convert.
        /// </param>
        /// <param name="configuration">
        /// <see cref="IHumanPoseTrackerConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/> converted from <see cref="IHumanPoseTrackerProtocol"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="protocol"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IHumanPoseTrackerSoftware> Transmission(IHumanPoseTrackerProtocol protocol, IHumanPoseTrackerConfiguration configuration)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new TransmitHumanPoseTracker(configuration, protocol);
        }
        private sealed class TransmitHumanPoseTracker :
            ITransmission<IHumanPoseTrackerSoftware>
        {
            private readonly IEmission emission;

            private readonly IConnection<IHumanPoseTrackerSoftware> connection;

            internal TransmitHumanPoseTracker(IHumanPoseTrackerConfiguration configuration, IHumanPoseTrackerProtocol protocol)
            {
                emission = Conduct(configuration, protocol.Software);

                connection = Connection(protocol.Hardware);
            }

            public ICancellation Connect(IHumanPoseTrackerSoftware module)
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
        private static IEmission Conduct(IHumanPoseTrackerConfiguration configuration, IHumanPoseTrackerSoftware software)
            => EmissionSource.Default
            .Synthesize(ConductSignalTo.Consume(configuration.Origin.Position, software.Origin.Position))
            .Synthesize(ConductSignalTo.Consume(configuration.Origin.Rotation, software.Origin.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.Head.Rotation, software.Head.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.Head.Rotation, software.Head.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.Rotation, software.LeftHand.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.LeftHand.Rotation, software.LeftHand.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.Rotation, software.RightHand.Rotation))
            .Synthesize(ConductSignalTo.Consume(configuration.RightHand.Rotation, software.RightHand.Rotation))
            .Build();

        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerSoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IHumanPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerHardware"/> converted from <see cref="IHumanPoseTrackerSoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IHumanPoseTrackerHardware> Connection(IHumanPoseTrackerSoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return new ConnectHumanPoseTrackerHardware(software);
        }
        private sealed class ConnectHumanPoseTrackerHardware :
            IConnection<IHumanPoseTrackerHardware>
        {
            private readonly IHumanPoseTrackerSoftware software;

            internal ConnectHumanPoseTrackerHardware(IHumanPoseTrackerSoftware software)
            {
                this.software = software;
            }

            public ICancellation Connect(IHumanPoseTrackerHardware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertHumanPoseTrackerInto.Connect(module, software);
            }
        }

        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IHumanPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/> converted from <see cref="IHumanPoseTrackerHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IHumanPoseTrackerSoftware> Connection(IHumanPoseTrackerHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return new ConnectHumanPoseTrackerSoftware(hardware);
        }
        private sealed class ConnectHumanPoseTrackerSoftware :
            IConnection<IHumanPoseTrackerSoftware>
        {
            private readonly IHumanPoseTrackerHardware hardware;

            internal ConnectHumanPoseTrackerSoftware(IHumanPoseTrackerHardware hardware)
            {
                this.hardware = hardware;
            }

            public ICancellation Connect(IHumanPoseTrackerSoftware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertHumanPoseTrackerInto.Connect(hardware, module);
            }
        }

        private static ICancellation Connect(IHumanPoseTrackerHardware hardware, IHumanPoseTrackerSoftware software)
            => CancellationSource.Default
            .Synthesize(hardware.Origin.Position.Produce(software.Origin.Position))
            .Synthesize(hardware.Origin.Rotation.Produce(software.Origin.Rotation))
            .Synthesize(hardware.Head.Position.Produce(software.Head.Position))
            .Synthesize(hardware.Head.Rotation.Produce(software.Head.Rotation))
            .Synthesize(hardware.LeftHand.Position.Produce(software.LeftHand.Position))
            .Synthesize(hardware.LeftHand.Rotation.Produce(software.LeftHand.Rotation))
            .Synthesize(hardware.RightHand.Position.Produce(software.RightHand.Position))
            .Synthesize(hardware.RightHand.Rotation.Produce(software.RightHand.Rotation))
            .Build();
    }
}
