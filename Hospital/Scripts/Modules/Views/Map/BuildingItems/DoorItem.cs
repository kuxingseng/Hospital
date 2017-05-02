namespace Assets.Hospital.Scripts.Modules.Views.Map.BuildingItems
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/27 18:17:48 
    /// Desc:
    /// </summary>
    ///
    public class DoorItem:BuildingItem
    {
        protected override IEnumerator OnBuild()
        {
            var randomTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(randomTime);

            //设置门的方向及删除墙体
            Image.rectTransform.localEulerAngles = new Vector3(0, 0, BindTileItems[0].WallItem.WallRotation);
            yield return StartCoroutine(BindTileItems[0].WallItem.Remove(true));

            Image.color = NormalColor;
        }
    }
}
