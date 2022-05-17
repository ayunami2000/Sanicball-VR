using Sanicball.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Sanicball
{
    public class Startup : MonoBehaviour
    {
        public UI.Intro intro;
        public CanvasGroup setNicknameGroup;
        public InputField nicknameField;

        public void ValidateNickname()
        {
            if (nicknameField.text.Trim() != "")
            {
                //setNicknameGroup.alpha = 0f;
                setNicknameGroup.gameObject.SetActive(false);
                ActiveData.GameSettings.nickname = nicknameField.text;
                intro.enabled = true;
            }
        }

        private void Start()
        {
            if (string.IsNullOrEmpty(ActiveData.GameSettings.nickname) || ActiveData.GameSettings.nickname == "Player")
            {
                //Set nickname before continuing
                //setNicknameGroup.alpha = 1f;
                setNicknameGroup.gameObject.SetActive(true);
            }
            else
            {
                //setNicknameGroup.alpha = 0f;
                setNicknameGroup.gameObject.SetActive(false);
                intro.enabled = true;
            }
        }
    }
}