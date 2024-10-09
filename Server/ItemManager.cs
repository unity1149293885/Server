using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ItemManager
    {
        public static List<int> DownItemList = new List<int>();

        public static bool ChangeItemState(int id,bool isDown)
        {
            if (DownItemList == null) return false;
            if (isDown == false)
            {
                //上架
                if (DownItemList.Count == 0|| !DownItemList.Contains(id)) return false;

                for(int i = 0; i < DownItemList.Count; i++)
                {
                    if (DownItemList[i] == id)
                    {
                        DownItemList.Remove(id);
                    }
                }
            }
            else
            {
                //下架
                if (DownItemList.Contains(id))
                {
                    return false;
                }
                DownItemList.Add(id);
            }
            return true;
        }
    }
}
