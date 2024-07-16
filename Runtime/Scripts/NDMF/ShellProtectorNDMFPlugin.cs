#if UNITY_EDITOR
#if MODULAR
using nadena.dev.ndmf;
using Shell.Protector;
using UnityEngine;

[assembly: ExportsPlugin(typeof(ShellProtectorNDMFPlugin))]

public class ShellProtectorNDMFPlugin : Plugin<ShellProtectorNDMFPlugin>
{
    protected override void Configure()
    {
        InPhase(BuildPhase.Generating).
            BeforePlugin("nadena.dev.modular-avatar").
            Run("Encrypting", ctx =>
            {
                Debug.Log("Encrypting Shell Protector");
                var shellProtector = ctx.AvatarRootObject.GetComponentInChildren<ShellProtector>();
                if (shellProtector)
                {
                    shellProtector.Encrypt(isModular: true);
                }
            });
        InPhase(BuildPhase.Transforming).
            AfterPlugin("nadena.dev.modular-avatar").
            Run("Encrypting2", ctx =>
            {
                Debug.Log("After encrypting Shell Protector");
                var shellProtector = ctx.AvatarRootObject.GetComponentInChildren<ShellProtector>();
                if (shellProtector)
                {
                    shellProtector.SetAnimations(ctx.AvatarRootObject, false);
                    shellProtector.ObfuscateBlendShape(ctx.AvatarRootObject, false);
                    shellProtector.ChangeMaterialsInAnims(ctx.AvatarRootObject, false);
                    shellProtector.CleanComponent(ctx.AvatarRootObject);
                }
            });
    }
}
#endif
#endif