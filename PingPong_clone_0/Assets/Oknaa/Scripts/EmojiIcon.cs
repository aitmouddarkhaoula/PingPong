using UnityEngine;
using UnityEngine.UI;

namespace Oknaa.Scripts {
    public class EmojiIcon : MonoBehaviour {
        public Image Image;
        public Button Button;

        public void Init(EmojiData emojiData, Transform vfxPosition) {
            Image.sprite = Sprite.Create((Texture2D)emojiData.Sprite, new Rect(0, 0, emojiData.Sprite.width, emojiData.Sprite.height), Vector2.zero);
            Button.onClick.AddListener(() => LaunchVFX(emojiData, vfxPosition));
        }
        
        private static void LaunchVFX(EmojiData emojiData, Transform vfxPosition) {
            var vfxInstance = Instantiate(emojiData.VFX, Vector3.zero, Quaternion.identity, vfxPosition);
            vfxInstance.transform.SetAsFirstSibling();
            vfxInstance.transform.localScale = 250 * Vector3.one;
        }
    }
}