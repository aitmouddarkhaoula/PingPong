using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Oknaa.Scripts
{
    public class EmojisPanel : MonoBehaviour
    {
        public EmojisPanelData Data;
        public EmojiIcon EmojiIconPrefab;

        public GameObject Panel;
        public Button EmojisPanelButton;
        public Button ClosePanelButton;
        public Transform Container;
        public Transform VFXPosition;
        


        public void Init(bool isServer)
        {
            EmojisPanelButton.onClick.AddListener(() => Panel.SetActive(!Panel.activeSelf));
            ClosePanelButton.onClick.AddListener(() => Panel.SetActive(false));
            if(isServer)  ActiveButton(true);
            else ActiveButton(false);
            var count = Data.Emojis.Count;
            for (var i = 0; i < count; i++)
            {
                var emojiData = Data.Emojis[i];
                Instantiate(EmojiIconPrefab, Container).Init(i, emojiData, VFXPosition, this );
            }
        }


        public void LaunchVFX(int id, bool isOwner)
        {
            var emoji = Data.Emojis[id].VFX;
            var vfxInstance = Instantiate(emoji, Vector3.zero, Quaternion.identity, VFXPosition);
            vfxInstance.transform.SetAsFirstSibling();
            vfxInstance.transform.localScale = 100 * Vector3.one;
        }
        
        public void ActiveButton(bool isActive)
        {
            EmojisPanelButton.gameObject.SetActive(isActive); 
        }
    }
}