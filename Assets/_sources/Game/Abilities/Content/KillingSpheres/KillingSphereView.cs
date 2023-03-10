using Game.Characters;
using UnityEngine;

namespace Game.Abilities
{
    //client only component
    [RequireComponent(typeof(KillingSphere))]
    public sealed class KillingSphereView : MonoBehaviour
    {
        [SerializeField] private Transform _debugVisual;
        //todo: add pooling
        [SerializeField] private ParticleSystem _onHitParticles;
        //todo: add pooling
        [SerializeField] private ParticleSystem _onSphereDestroyParticles;


        private void Awake()
        {
            var ks = GetComponent<KillingSphere>();
            HandleOnInited(ks); //in case (impossible) Killing Sphere had already been inited
            ks.OnInited += HandleOnInited;
            ks.OnCharacterHitted += HandleOnCharacterHitted;
            ks.OnBeforeDestruction += HandleOnBeforeDestruction;
        }

        private void HandleOnInited(KillingSphere ks)
        {
            AdjustVisualSphereRadius(ks.SphereRadius);
        }


        private void HandleOnCharacterHitted(KillingSphere ks, Character hittedCharacter)
        {
            if (_onHitParticles == null)
                return;

            var targetTr = hittedCharacter.transform;
            var particlesInst = Instantiate(_onHitParticles, targetTr.position, Quaternion.identity, targetTr);
            SetupVfx(particlesInst);
        }

        private void HandleOnBeforeDestruction(KillingSphere ks)
        {
            var particlesInst = Instantiate(_onSphereDestroyParticles, ks.transform.position, Quaternion.identity);
            SetupVfx(particlesInst);
        }

        private void SetupVfx(ParticleSystem vfx)
        {
            ParticleSystem.MainModule main = vfx.main;
            main.stopAction = ParticleSystemStopAction.Destroy;
            vfx.Play(true);
        }


        private void AdjustVisualSphereRadius(float sphereRadius)
        {
            var parent = _debugVisual.parent;
            _debugVisual.SetParent(null, true);
            _debugVisual.localScale = Vector3.one * sphereRadius;
            _debugVisual.SetParent(parent, true);
        }

    }
}
