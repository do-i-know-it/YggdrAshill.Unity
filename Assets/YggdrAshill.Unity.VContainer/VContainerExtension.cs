using YggdrAshill.Ragnarok.Periodization;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity.VContainer
{
    public static class VContainerExtension
    {
        public static RegistrationBuilder RegisterEntryPoint<TSpan>(this IContainerBuilder builder, Lifetime lifetime)
            where TSpan : ISpan
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Register<TSpan>(lifetime).AsImplementedInterfaces();

            return builder.RegisterEntryPoint<InitializableSpan>();
        }
        
        public static RegistrationBuilder RegisterEntryPoint<TCycle>(this IContainerBuilder builder, Lifetime lifetime, VContainerUpdateClock clock)
            where TCycle : ICycle
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Register<TCycle>(lifetime).AsImplementedInterfaces();

            switch (clock)
            {
                case VContainerUpdateClock.PreUpdate:
                    return builder.RegisterEntryPoint<PreTickableCycle>();
                case VContainerUpdateClock.PostUpdate:
                    return builder.RegisterEntryPoint<PostTickableCycle>();
                case VContainerUpdateClock.PreLateUpdate:
                    return builder.RegisterEntryPoint<PreLateTickableCycle>();
                case VContainerUpdateClock.PostLateUpdate:
                    return builder.RegisterEntryPoint<PostLateTickableCycle>();
                default:
                    throw new NotSupportedException(nameof(VContainerUpdateClock));
            }
        }
    }
}
