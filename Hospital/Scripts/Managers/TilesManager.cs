namespace Assets.Hospital.Scripts.Managers
{
    using System.Collections.Generic;
    using Modules.Views.Map;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/24 18:10:07 
    /// Desc:
    /// </summary>
    ///
    public static class TilesManager
    {
        /// <summary>
        /// 所有格子字典
        /// </summary>
        public static readonly Dictionary<Vector2, TileItem> TileItemDicts = new Dictionary<Vector2, TileItem>(); 

        /// <summary>
        /// 获取格子
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static TileItem GetTileItem(Vector2 position)
        {
            TileItem item;
            if (TileItemDicts.TryGetValue(position, out item))
                return item;
            return null;
        }

        /// <summary>
        /// 获取格子
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static TileItem GetTileItem(int indexX, int indexY)
        {
            TileItem item;
            if (TileItemDicts.TryGetValue(new Vector2(indexX, indexY), out item))
                return item;
            return null;
        }

        /// <summary>
        /// 获取周边格子列表（四向）
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static List<TileItem> GetArroundTiles(int indexX, int indexY)
        {
            var items = new List<TileItem>();
            TileItem item = GetUpperTile(indexX, indexY);
            if(item!=null)
                items.Add(item);
            item = GetBottomTile(indexX, indexY);
            if (item != null)
                items.Add(item);
            item = GetLeftTile(indexX, indexY);
            if (item != null)
                items.Add(item);
            item = GetRightTile(indexX, indexY);
            if (item != null)
                items.Add(item);
            return items;
        }

        /// <summary>
        /// 获取上下左右顺序的格子，若没有格子为空
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static List<TileItem> GetArroundTilesWithNull(int indexX, int indexY)
        {
            var items = new List<TileItem>();
            TileItem item=GetUpperTile(indexX,indexY);
            items.Add(item);
            item = GetBottomTile(indexX, indexY);
            items.Add(item);
            item = GetLeftTile(indexX, indexY);
            items.Add(item);
            item = GetRightTile(indexX, indexY);
            items.Add(item);
            return items;
        }

        /// <summary>
        /// 获取上面的格子
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static TileItem GetUpperTile(int indexX, int indexY)
        {
            TileItem item;
            if (TileItemDicts.TryGetValue(new Vector2(indexX, indexY - 1), out item))
                return item;
            return null;
        }

        /// <summary>
        /// 获取上面的格子
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static TileItem GetBottomTile(int indexX, int indexY)
        {
            TileItem item;
            if (TileItemDicts.TryGetValue(new Vector2(indexX, indexY + 1), out item))
                return item;
            return null;
        }

        /// <summary>
        /// 获取上面的格子
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static TileItem GetLeftTile(int indexX, int indexY)
        {
            TileItem item;
            if (TileItemDicts.TryGetValue(new Vector2(indexX-1, indexY), out item))
                return item;
            return null;
        }

        /// <summary>
        /// 获取上面的格子
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public static TileItem GetRightTile(int indexX, int indexY)
        {
            TileItem item;
            if (TileItemDicts.TryGetValue(new Vector2(indexX+1, indexY), out item))
                return item;
            return null;
        }
    }
}
