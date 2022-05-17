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
        private BoxCollider boxCollider;

        public void ValidateNickname()
        {
            if (nicknameField.text.Trim() != "")
            {
                setNicknameGroup.alpha = 0f;
                boxCollider.enabled = true;
                ActiveData.GameSettings.nickname = nicknameField.text;
                intro.enabled = true;
            }
        }

        private void Start()
        {
            boxCollider = setNicknameGroup.gameObject.transform.parent.GetComponent<BoxCollider>();
            if (string.IsNullOrEmpty(ActiveData.GameSettings.nickname) || ActiveData.GameSettings.nickname == "Player")
            {
                //Set nickname before continuing
                setNicknameGroup.alpha = 1f;
                boxCollider.enabled = false;
            }
            else
            {
                setNicknameGroup.alpha = 0f;
                intro.enabled = true;
            }
        }
    }
}