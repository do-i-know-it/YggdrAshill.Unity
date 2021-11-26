using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ConvertThreePointPoseTrackerInto
    {
        /// <summary>
        /// Converts <see cref="IThreePointPoseTrackerProtocol"/> into <see cref="ITransmission{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="protocol">
        /// <see cref="IThreePointPoseTrackerProtocol"/> to convert.
        /// </param>
        /// <param name="configuration">
        /// <see cref="IThreePointPoseTrackerConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/> converted from <see cref="IThreePointPoseTrackerProtocol"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="protocol"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IThreePointPoseTrackerSoftware> Transmission(IThreePointPoseTrackerProtocol protocol, IThreePointPoseTrackerConfiguration configuration)
        {
            if (protocol == null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new TransmitThreePointPoseTracker(configuration, protocol);
        }
        private sealed class TransmitThreePointPoseTracker :
            ITransmission<IThreePointPoseTrackerSoftware>
        {
            private readonly IEmission emission;

            private readonly IConnection<IThreePointPoseTrackerSoftware> connection;

            internal TransmitThreePointPoseTracker(IThreePointPoseTrackerConfiguration configuration, IThreePointPoseTrackerProtocol protocol)
            {
                emission = Conduct(configuration, protocol.Software);

                connection = Connection(protocol.Hardware);
            }

            public ICancellation Connect(IThreePointPoseTrackerSoftware module)
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
        private static IEmission Conduct(IThreePointPoseTrackerConfiguration configuration, IThreePointPoseTrackerSoftware software)
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
        /// Converts <see cref="IThreePointPoseTrackerSoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IThreePointPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerHardware"/> converted from <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IThreePointPoseTrackerHardware> Connection(IThreePointPoseTrackerSoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return new ConnectThreePointPoseTrackerHardware(software);
        }
        private sealed class ConnectThreePointPoseTrackerHardware :
            IConnection<IThreePointPoseTrackerHardware>
        {
            private readonly IThreePointPoseTrackerSoftware software;

            internal ConnectThreePointPoseTrackerHardware(IThreePointPoseTrackerSoftware software)
            {
                this.software = software;
            }

            public ICancellation Connect(IThreePointPoseTrackerHardware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertThreePointPoseTrackerInto.Connect(module, software);
            }
        }

        /// <summary>
        /// Converts <see cref="IThreePointPoseTrackerHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IThreePointPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/> converted from <see cref="IThreePointPoseTrackerHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IThreePointPoseTrackerSoftware> Connection(IThreePointPoseTrackerHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return new ConnectThreePointPoseTrackerSoftware(hardware);
        }
        private sealed class ConnectThreePointPoseTrackerSoftware :
            IConnection<IThreePointPoseTrackerSoftware>
        {
            private readonly IThreePointPoseTrackerHardware hardware;

            internal ConnectThreePointPoseTrackerSoftware(IThreePointPoseTrackerHardware hardware)
            {
                this.hardware = hardware;
            }

            public ICancellation Connect(IThreePointPoseTrackerSoftware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                return ConvertThreePointPoseTrackerInto.Connect(hardware, module);
            }
        }

        private static ICancellation Connect(IThreePointPoseTrackerHardware hardware, IThreePointPoseTrackerSoftware software)
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
