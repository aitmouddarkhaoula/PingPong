#if UNITY_EDITOR
using System.IO;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace GameSystems.Shop {
    public class TweakablesEditorWindow : OdinMenuEditorWindow {
        private const string PATH_TWEAKABLES = "Assets/_Game/SO";


        [MenuItem("Proto/Tweakables")]
        public static void OpenWindow() {
            GetWindow<TweakablesEditorWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree() {
            OdinMenuTree tree = new OdinMenuTree();
            GetDirectories(tree, PATH_TWEAKABLES);
            return tree;
        }

        private static void GetDirectories(OdinMenuTree tree, string path) {
            GetScriptableObjects(tree, path);
            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo[] subDirectories = directory.GetDirectories();

            foreach (var subDirectory in subDirectories) {
                GetDirectories(tree, $"{path}/{subDirectory.Name}");
            }
        }

        private static void GetScriptableObjects(OdinMenuTree tree, string directory) {
            var directoryInfo = new DirectoryInfo(directory);
            var files = directoryInfo.GetFiles();

            foreach (var file in files) {
                if (file.FullName.EndsWith(".asset")) {
                    if (file.Directory is { Name: "Upgrades" }) return;

                    var path = $"{directory}/{file.Name}";
                    var menuName = file.Directory != null && file.Directory.Name.StartsWith("Level ")
                        ? $"Levels/{file.Name.Replace(".asset", "")}"
                        : $"{directory.Replace(PATH_TWEAKABLES, "")}/{file.Name.Replace(".asset", "")}";

                    tree.AddAssetAtPath(menuName, path);
                }
            }
        }
    }
}
#endif