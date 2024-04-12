using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UE
{
    public class SFXManager : MonoBehaviour
    {
        private WaitForSeconds duckWait;
        public AudioSource[] uiAudio;
        public AudioSource[] gamePlayAudio;
        public AudioSource[] themeAudioSources;
        public AudioClip[] randThemeSounds;
        public AudioClip[] randEventSounds;

        public void ButtonClickFX() { uiAudio[0].Play(); }
        public void UIElementClickFX() { uiAudio[1].Play(); }
        public void ReturnClickFX() { uiAudio[2].Play(); }
        public void SelectedItemClickFX() { uiAudio[3].Play(); }
        public void SwapWeaponFX() { uiAudio[4].Play(); }
        public void EquipWeaponFX() { uiAudio[5].Play(); }

        public void NitroModeFX() { }
        public void SheildModeFX() { }
        public void GamePausedFX() { gamePlayAudio[2].Play(); }

        public void GamePausedResumeFX() { gamePlayAudio[2].Stop(); }
        public void GameDamgeFX() { gamePlayAudio[2].Play(); }
        public void GameDustFX() { if (gamePlayAudio[4].isPlaying) { return; } gamePlayAudio[4].Play(); }
        public void JetEngineFX() { gamePlayAudio[3].Play(); }
        public void RumbleFX() { gamePlayAudio[5].Play(); }
        public void ExplodeFX() { gamePlayAudio[6].Play(); gamePlayAudio[6].pitch = Random.Range(0.9f, 1.2f); }
        public void HitFX() { gamePlayAudio[7].Play(); }
        public void HealthAdded() { gamePlayAudio[8].Play(); }

        public void StopJetEngineFX() { gamePlayAudio[3].Stop(); }

        public void FiringModeFX(AudioClip clip) { gamePlayAudio[0].clip = clip; gamePlayAudio[0].Play(); }

        public void EnemyFiringModeFX() { if (!gamePlayAudio[1].isPlaying) { gamePlayAudio[1].Play(); } }

        public void PlayGameEventAudio(bool status = false)
        {
            if (status)
            {
                int rand = Random.Range(0, randEventSounds.Length);
                themeAudioSources[1].clip = randEventSounds[rand];
                themeAudioSources[1].volume = 1;
                StartCoroutine(DuckVolumne(themeAudioSources[0], themeAudioSources[1]));
            }
            else
            {
                //themeAudioSources[1].Stop();
                StartCoroutine(DuckVolumne(themeAudioSources[1], themeAudioSources[0]));
            }
        }

        public void PlayGameTheme()
        {
            int rand = Random.Range(0, randThemeSounds.Length);
            //themeAudioSources[0].clip = randThemeSounds[rand];
            themeAudioSources[0].clip = randThemeSounds[^1];

            //themeAudioSources[0].volume = 1;
            StartCoroutine(DuckVolumne(themeAudioSources[1], themeAudioSources[0]));
        }

        private IEnumerator DuckVolumne(AudioSource cOut, AudioSource cIn)
        {
            float step = 1;
            while (step > 0)
            {
                cOut.volume = step;
                cIn.volume = 1 - step;
                step -= Time.deltaTime;
                yield return null;
            }
            cOut.Stop();
            cIn.Play();
        }

        private void Start()
        {
            PlayGameTheme();
        }
    }
}
