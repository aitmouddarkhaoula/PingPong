using UnityEngine;
using UnityEngine.UI;

namespace Oknaa.Scripts {
    public class EmojisPanel : MonoBehaviour {
        public EmojisPanelData Data;
        public EmojiIcon EmojiIconPrefab;

        public GameObject Panel;
        public Button EmojisPanelButton;
        public Transform Container;
        public Transform VFXPosition;
        
        
        private void Start() {
            EmojisPanelButton.onClick.AddListener(() => Panel.SetActive(!Panel.activeSelf));
            
            foreach (var emojiData in Data.Emojis) { 
                Instantiate(EmojiIconPrefab, Container).Init(emojiData, VFXPosition);
            }
        }
    }
}