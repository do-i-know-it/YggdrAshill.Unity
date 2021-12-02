using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;

namespace YggdrAshill.Unity.OVR
{
    internal sealed class OculusTrigger :
        ITriggerConfiguration
    {
        internal OculusTrigger(OVRInput.RawTouch touch, OVRInput.RawAxis1D pull)
        {
            Touch = Generate.Touch(() => OVRInput.Get(touch));

            Pull = Generate.Pull(() => OVRInput.Get(pull));
        }

        internal OculusTrigger(OVRInput.RawAxis1D pull)
        {
            Touch = Generate.Touch(() => OVRInput.Get(pull) > 0.1f);

            Pull = Generate.Pull(() => OVRInput.Get(pull));
        }

        public IGeneration<Touch> Touch { get; }

        public IGeneration<Pull> Pull { get; }
    }
}
