using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSystems.Shop {
    [CreateAssetMenu(fileName = "LevelingTable", menuName = "LevelingTable", order = 0)]
    public class LevelingTableSO : SerializedScriptableObject {
        [BoxGroup("CHARACTERS", false)]
        
        [ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 30)]
        [BoxGroup("CHARACTERS", false), LabelText("Upgrades"), PropertySpace(10, 20), Searchable, ]
        public List<Emoji> LevelEmojies = new List<Emoji>(); // Upgrades Available
        
    }

    public class Emoji {
        [TitleGroup("")]
        [HorizontalGroup("/1", PaddingLeft = 0, PaddingRight = 0, Width = 300)]
        [VerticalGroup("/1/Left")]
        [BoxGroup("/1/Left/Image", false)] [PreviewField(150, ObjectFieldAlignment.Center)] [HideLabel] public Sprite Sprite;
        [BoxGroup("/1/Right/Image", false)] [PreviewField(150, ObjectFieldAlignment.Center)] [HideLabel] public GameObject VFX;
    }
}