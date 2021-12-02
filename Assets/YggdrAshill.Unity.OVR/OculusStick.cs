using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;

namespace YggdrAshill.Unity.OVR
{
    internal sealed class OculusStick :
        IStickConfiguration
    {
        internal OculusStick(OVRInput.RawTouch touch, OVRInput.RawAxis2D tilt)
        {
            Touch = Generate.Touch(() => OVRInput.Get(touch));

            Tilt = Generate.Tilt(() => OVRInput.Get(tilt));
        }

        public IGeneration<Touch> Touch { get; }

        public IGeneration<Tilt> Tilt { get; }
    }
}
