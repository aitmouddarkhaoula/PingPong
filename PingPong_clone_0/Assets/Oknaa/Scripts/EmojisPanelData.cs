using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Oknaa.Scripts {
    [CreateAssetMenu(fileName = "EmojisPanelData", menuName = "EmojisPanelData", order = 0)]
    public class EmojisPanelData : ScriptableObject {
        [BoxGroup("CHARACTERS", false)]
        
        [ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 30)]
        [BoxGroup("CHARACTERS", false), LabelText("Upgrades"), PropertySpace(10, 20)]
        public List<EmojiData> Emojis = new List<EmojiData>();

    }
    
    
    [Serializable]
    public class EmojiData {
        [TitleGroup("")]
        [HorizontalGroup("/1", PaddingLeft = 0, PaddingRight = 0, Width = 300)]
        // [VerticalGroup("/1/Left")]
        [VerticalGroup("/1/Left")] [PreviewField(150, ObjectFieldAlignment.Center)] [HideLabel] 
        [AssetList(Path = "Assets/_Game/Sprites/EmojisIcons")] public Texture Sprite;
        [VerticalGroup("/1/Right")] [PreviewField(150, ObjectFieldAlignment.Center)] [HideLabel] public GameObject VFX;
    }
}