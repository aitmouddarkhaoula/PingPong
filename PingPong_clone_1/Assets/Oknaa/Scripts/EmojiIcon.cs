using System;
using UnityEngine;
using UnityEngine.UI;

namespace Oknaa.Scripts
{
    public class EmojiIcon : MonoBehaviour
    {
        public int Id;
        public Image Image;
        public Button Button;
        public Action OnClickEmojisButton;

        public void Init(int id, EmojiData emojiData, Transform vfxPosition, EmojisPanel emojisPanel)
        {
            Id = id;
            Image.sprite = Sprite.Create((Texture2D)emojiData.Sprite,
                new Rect(0, 0, emojiData.Sprite.width, emojiData.Sprite.height), Vector2.zero);
            Button.onClick.AddListener(() =>
            {
                
                FindObjectOfType<PaddleController>().ShowEmojiVFXServerRpc(Id);
                // emojisPanel.LaunchVFX(Id, true);
            });
        }
    }
}