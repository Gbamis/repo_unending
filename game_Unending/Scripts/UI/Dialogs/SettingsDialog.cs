using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

namespace UE
{
    public class SettingsDialog : UIController
    {
        [SerializeField] private AudioMixer gameMixer;

        [SerializeField] private Slider masterVolumneSlider;
        [SerializeField] private Text masterPercent;

        [SerializeField] private Slider sfxVolumneSlider;
        [SerializeField] private Text sfxPercent;

        [SerializeField] private Slider themeVolumneSlider;
        [SerializeField] private Text themePercent;

        [SerializeField] private Slider uiVolumneSlider;
        [SerializeField] private Text uiPercent;

        [SerializeField] private Button closeBtn;
        [SerializeField] private Button resetBtn;




        public override void Init(UIManager ui)
        {
            uiManagerRef = ui;
            ReadVolumne();
        }

        protected override void Enable()
        {
            closeBtn.onClick.AddListener(() => { gameObject.SetActive(false); });
            ReadVolumne();

            masterVolumneSlider.onValueChanged.AddListener((ctx) =>
            {
                float v = Mathf.Log10(ctx) * 20;
                gameMixer.SetFloat("masterVolume", v);
                float per = ctx * 100; per = Mathf.CeilToInt(per);
                masterPercent.text = per.ToString() + "%";

                PlayerPrefs.SetFloat("masterVolume", ctx);
            });

            sfxVolumneSlider.onValueChanged.AddListener((ctx) =>
            {
                float v = Mathf.Log10(ctx) * 20;
                gameMixer.SetFloat("gameplayVolume", v);
                float per = ctx * 100; per = Mathf.CeilToInt(per);
                sfxPercent.text = per.ToString() + "%";

                PlayerPrefs.SetFloat("gameplayVolume", ctx);
            });

            themeVolumneSlider.onValueChanged.AddListener((ctx) =>
            {
                float v = Mathf.Log10(ctx) * 20;
                gameMixer.SetFloat("gameThemeVolume", v);
                float per = ctx * 100; per = Mathf.CeilToInt(per);
                themePercent.text = per.ToString() + "%";

                PlayerPrefs.SetFloat("gameThemeVolume", ctx);
            });

            uiVolumneSlider.onValueChanged.AddListener((ctx) =>
            {
                float v = Mathf.Log10(ctx) * 20;
                gameMixer.SetFloat("uiVolume", v);
                float per = ctx * 100; per = Mathf.CeilToInt(per);
                uiPercent.text = per.ToString() + "%";

                PlayerPrefs.SetFloat("uiVolume", ctx);
            });
        }

        protected override void Disable()
        {
            closeBtn.onClick.RemoveAllListeners();
            masterVolumneSlider.onValueChanged.RemoveAllListeners();
            sfxVolumneSlider.onValueChanged.RemoveAllListeners();
            themeVolumneSlider.onValueChanged.RemoveAllListeners();
            uiVolumneSlider.onValueChanged.RemoveAllListeners();
        }

        private void ReadVolumne()
        {
            float master = PlayerPrefs.GetFloat("masterVolume", 1);
            float gameplay = PlayerPrefs.GetFloat("gameplayVolume", .3f);
            float gameTheme = PlayerPrefs.GetFloat("gameThemeVolume", .3f);
            float ui = PlayerPrefs.GetFloat("uiVolume", .5f);

            gameMixer.SetFloat("masterVolume", Mathf.Log10(master) * 20);
            gameMixer.SetFloat("gameplayVolume", Mathf.Log10(gameplay) * 20);
            gameMixer.SetFloat("gameThemeVolume", Mathf.Log10(gameTheme) * 20);
            gameMixer.SetFloat("uiVolume", Mathf.Log10(ui) * 20);

            masterVolumneSlider.value = master;
            sfxVolumneSlider.value = gameplay;
            themeVolumneSlider.value = gameTheme;
            uiVolumneSlider.value = ui;

            float per = master * 100; per = Mathf.CeilToInt(per); masterPercent.text = per.ToString() + "%";
            float perG = gameplay * 100; perG = Mathf.CeilToInt(perG); sfxPercent.text = perG.ToString() + "%";
            float perT = gameTheme * 100; perT = Mathf.CeilToInt(perT); themePercent.text = perT.ToString() + "%";
            float perU = ui * 100; perU = Mathf.CeilToInt(perU); uiPercent.text = perU.ToString() + "%";
        }
    }
}
