namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.IO;
    using System.Text.RegularExpressions;
    using Blaze.Framework;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 预处理模型设置。
    /// </summary>
    public class ModelProcessor : AssetPostprocessor
    {
        protected void OnPostprocessModel(GameObject obj)
        {
            if (!mRegex.IsMatch(assetPath))
                return;
            var match = mRegex.Match(assetPath);
            var category = match.Result("${category}");
            var id = match.Result("${id}");
            if (category == "Character" || category == "Monster")
            {
//                if (category == "Character")
//                {   
//                    if (id.Contains("!"))
//                    {
//                        postporcessActionModel(obj);
//                    }
//                }
                    
                return;
            }
            else if (category == "Weapon")
            {
                postprocessItemModel(obj);
                postprocessItemMaterial(id);
            }
        }

        protected void OnPreprocessModel()
        {
            if (!mRegex.IsMatch(assetPath))
                return;
            var importer = (ModelImporter) assetImporter;
            var match = mRegex.Match(assetPath);
            var category = match.Result("${category}");
            var id = match.Result("${id}");
            if (category == "Character" || category == "Monster")
            {
                preprocessCharacterModel(importer, id);
            }
            else if (category == "Weapon")
            {
                preprocessItemModel(importer, id);
            }
        }

        //处理动作模型
        private void postporcessActionModel(GameObject item)
        {
            var prefabPath = assetPath.Replace(".FBX", ".prefab");
            var prefab = AssetDatabase.LoadMainAssetAtPath(prefabPath);
            if (prefab != null)
                return;

            item.transform.ResetLocal();
            PrefabUtility.CreatePrefab(prefabPath, item);
        }

        //武器材质添加mjhx/item shader文件
        private void postprocessItemMaterial(string id)
        {
            var dir = UnityPath.GetProjectPath(assetPath.Replace(id + ".FBX", "Materials"));
            foreach (var file in Directory.GetFiles(dir))
            {
                if (!file.EndsWith(".mat"))
                    return;
                var materialPath = UnityPath.GetProjectRelativePath(file);
                var material = (Material) AssetDatabase.LoadMainAssetAtPath(materialPath);
                material.shader = Shader.Find("Mjhx/Item");
                material.color = Color.white;
            }
        }

        //为武器模型生成prefab
        private void postprocessItemModel(GameObject item)
        {
            var prefabPath = assetPath.Replace(".FBX", ".prefab");
            var prefab = AssetDatabase.LoadMainAssetAtPath(prefabPath);
            if (prefab != null)
                return;

            item.transform.ResetLocal();
            var renderer = item.GetComponent<MeshRenderer>();
            renderer.castShadows = false;
            renderer.receiveShadows = false;
            PrefabUtility.CreatePrefab(prefabPath, item);
        }

        //处理角色模型的动画设置
        private void preprocessCharacterModel(ModelImporter importer, string id)
        {
            if (importer.assetPath.Contains("@"))
            {
                importer.importAnimation = true;
                importer.animationType = ModelImporterAnimationType.Legacy;
                importer.generateAnimations = ModelImporterGenerateAnimations.GenerateAnimations;
            }
            else
            {
                importer.importAnimation = false;
            }
        }

        //处理武器动画设置
        private void preprocessItemModel(ModelImporter importer, string id)
        {
            importer.animationType = ModelImporterAnimationType.None;
            importer.importAnimation = false;
        }

        private static readonly Regex mRegex = new Regex("Assets/MjhxResource/(?<category>.+?)/(?<id>.+?)/(.+)");
    }
}