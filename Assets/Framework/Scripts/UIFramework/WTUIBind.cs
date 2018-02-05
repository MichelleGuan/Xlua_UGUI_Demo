namespace WT.UI
{
    using UnityEngine;
    using System.Collections;

  
    public class WTUIBind : MonoBehaviour
    {
        static bool isBind = false;

        /// <summary>
        /// 绑定自已定义的加载器
        /// </summary>
        public static void Bind()
        {
            if (!isBind)
            {
                isBind = true;
                

                //bind for your loader api to load UI.
                //TTUIPage.delegateSyncLoadUIByLocal = Resources.Load;
                //TTUIPage.delegateSyncLoadUIByRemote = FileIO.LoadUIAssetBundle;

                WTUIPage.delegateSyncLoadUIByLocal = FileIO.LoadPrefab;

                //TTUIPage.delegateSyncLoadUIByLocalStringPath = FileIO.LoadPrefab;
                //TTUIPage.delegateAsyncLoadUI = UILoader.Load;

            }
        }
    }
}