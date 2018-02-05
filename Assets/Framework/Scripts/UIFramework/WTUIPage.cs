namespace WT.UI
{
    using System;
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using Object = UnityEngine.Object;


    #region define

    public enum UIType
    {
        Normal,
        Fixed,

        /// <summary>
        /// 弹出窗口，需要关闭当前才可操作其他
        /// </summary>
        PopUp,

        /// <summary>
        /// 独立的窗口
        /// </summary>
        None,
    }

    public enum UIMode
    {
        /// <summary>
        /// 
        /// </summary>
        DoNothing,

        /// <summary>
        /// 关闭其他界面
        /// </summary>
        HideOther,

        /// <summary>
        ///  点击返回按钮关闭当前,不关闭其他界面(需要调整好层级关系)
        /// </summary>
        NeedBack,

        /// <summary>
        /// 关闭TopBar,关闭其他界面,不加入backSequence队列
        /// </summary>
        NoNeedBack,
    }

    public enum UICollider
    {
        /// <summary>
        /// 该界面不包含碰撞背景
        /// </summary>
        None,

        /// <summary>
        /// 碰撞透明背景
        /// </summary>
        Normal,

        /// <summary>
        /// 碰撞非透明背景
        /// </summary>
        WithBg,
    }
    #endregion

    public abstract class WTUIPage
    {
        public string name = string.Empty;

        //页面id 
        public int id = -1;

        //页面类型
        public UIType type = UIType.Normal;

        //页面打开模式
        public UIMode mode = UIMode.DoNothing;

        //背景碰撞模式
        public UICollider collider = UICollider.None;

        /// <summary>
        /// ui路径，已经改为使用索引
        /// </summary>
        public string uiPath = string.Empty;

        /// <summary>
        /// ui预制体索引
        /// </summary>
        protected int uiIndex;

        /// <summary>
        /// ui的gameObject
        /// </summary>
        public GameObject gameObject;
        public Transform transform;

        //所有的页面
        private static Dictionary<string, WTUIPage> m_allPages;
        //public static Dictionary<string, TTUIPage> allPages
        //{ get { return m_allPages; } }

         
        private static List<WTUIPage> m_currentPageNodes;
        public static List<WTUIPage> currentPageNodes
        { get { return m_currentPageNodes; } }

        /// <summary>
        /// 是否异步加载模式
        /// </summary>
        private bool isAsyncUI = false;

       /// <summary>
       /// 是否激活
       /// </summary>
        protected bool isActived = false;

       /// <summary>
       /// 界面数据，重新打开时刷新
       /// </summary>
        private object m_data = null;
        protected object data { get { return m_data; } }
         
        /// <summary>
        /// 本地加载器
        /// </summary>
        public static Func<int, Object> delegateSyncLoadUIByLocal = null;
        public static Func<string, Object> delegateSyncLoadUIByLocalStringPath = null;
        /// <summary>
        /// 远程加载器
        /// </summary>
         //public static Func<string, Object> delegateSyncLoadUIByRemote = null;

        public static Action<string, Action<Object>> delegateAsyncLoadUI = null;

        #region virtual api

      /// <summary>
      /// 当初始化ui时调用一次
      /// </summary>
      /// <param name="go"></param>
        public virtual void Awake(GameObject go) { }

        /// <summary>
        /// 刷新界面，在Awake之后调用，每次打开界面时都会调用
        /// </summary>
        public virtual void Refresh() { }

       /// <summary>
       /// 激活ui
       /// </summary>
        public virtual void Active()
        {
            this.gameObject.SetActive(true);
            isActived = true;
        }

        
        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
            isActived = false;
            
            this.m_data = null;
        }

        #endregion

        #region internal api

        private WTUIPage() { }
        public WTUIPage(UIType type, UIMode mod, UICollider col)
        {
            this.type = type;
            this.mode = mod;
            this.collider = col;
            this.name = this.GetType().ToString();

           
            WTUIBind.Bind();
            
        }
  
        protected void Show()
        {
            if (this.gameObject == null && uiIndex >= 0)
            {
                GameObject go = null;

                if (delegateSyncLoadUIByLocal != null)
                {
                    Object o = delegateSyncLoadUIByLocal(uiIndex);

                    go = o != null ? GameObject.Instantiate(o) as GameObject : null;
                }
                else
                {
                    go = GameObject.Instantiate(Resources.Load(uiPath)) as GameObject;
                }

                //protected.
                if (go == null)
                {
                    Debug.LogError("[UI] Cant sync load your ui prefab,uiPath:" + uiPath);
                    return;
                }

                AnchorUIGameObject(go);

                //第一次初始化的时候调用Awake
                Awake(go);
                 
                isAsyncUI = false;
            }

           
            Active();

           
            Refresh();

           
            PopNode(this);
        }
 
        /// <summary>
        /// 异步显示
        /// </summary>
        protected void Show(Action callback)
        {
            WTUIRoot.Instance.StartCoroutine(AsyncShow(callback));
        }

        /// <summary>
        /// 异步显示
        /// </summary>
        IEnumerator AsyncShow(Action callback)
        {
            if (this.gameObject == null && string.IsNullOrEmpty(uiPath) == false)
            {
                GameObject go = null;
                bool _loading = true;
                delegateAsyncLoadUI(uiPath, (o) =>
                {
                    go = o != null ? GameObject.Instantiate(o) as GameObject : null;
                    AnchorUIGameObject(go);
                    Awake(go);
                    isAsyncUI = true;
                    _loading = false;

                  
                    Active();

                  
                    Refresh();

                 
                    PopNode(this);

                    if (callback != null) callback();
                });

                float _t0 = Time.realtimeSinceStartup;
                while (_loading)
                {
                    if (Time.realtimeSinceStartup - _t0 >= 10.0f)
                    {
                        Debug.LogError("[UI] WTF async load your ui prefab timeout!");
                        yield break;
                    }
                    yield return null;
                }
            }
            else
            {
               
                Active();

               
                Refresh();

              
                PopNode(this);

                if (callback != null) callback();
            }
        }

        internal bool CheckIfNeedBack()
        {
            if (type == UIType.Fixed || type == UIType.PopUp || type == UIType.None) return false;
            else if (mode == UIMode.NoNeedBack || mode == UIMode.DoNothing) return false;
            return true;
        }

        protected void AnchorUIGameObject(GameObject ui)
        {
            if (WTUIRoot.Instance == null || ui == null) return;

            this.gameObject = ui;
            this.transform = ui.transform;

            
            Vector3 anchorPos = Vector3.zero;
            Vector2 sizeDel = Vector2.zero;
            Vector3 scale = Vector3.one;
            if (ui.GetComponent<RectTransform>() != null)
            {
                anchorPos = ui.GetComponent<RectTransform>().anchoredPosition;
                sizeDel = ui.GetComponent<RectTransform>().sizeDelta;
                scale = ui.GetComponent<RectTransform>().localScale;
            }
            else
            {
                anchorPos = ui.transform.localPosition;
                scale = ui.transform.localScale;
            }

            //Debug.Log("anchorPos:" + anchorPos + "|sizeDel:" + sizeDel);

            if (type == UIType.Fixed)
            {
                ui.transform.SetParent(WTUIRoot.Instance.fixedRoot);
            }
            else if (type == UIType.Normal)
            {
                ui.transform.SetParent(WTUIRoot.Instance.normalRoot);
            }
            else if (type == UIType.PopUp)
            {
                ui.transform.SetParent(WTUIRoot.Instance.popupRoot);
            }


            if (ui.GetComponent<RectTransform>() != null)
            {
                ui.GetComponent<RectTransform>().anchoredPosition = anchorPos;
                ui.GetComponent<RectTransform>().sizeDelta = sizeDel;
                ui.GetComponent<RectTransform>().localScale = scale;
            }
            else
            {
                ui.transform.localPosition = anchorPos;
                ui.transform.localScale = scale;
            }
        }

        public override string ToString()
        {
            return ">Name:" + name + ",ID:" + id + ",Type:" + type.ToString() + ",ShowMode:" + mode.ToString() + ",Collider:" + collider.ToString();
        }

        public bool isActive()
        {
            //考虑如果不是一个gameobject的情况
            bool ret = gameObject != null && gameObject.activeSelf;
            return ret || isActived;
        }

        #endregion

        #region 静态方法

        private static bool CheckIfNeedBack(WTUIPage page)
        {
            return page != null && page.CheckIfNeedBack();
        }

        /// <summary>
        ///  
        /// 将目标弹到顶层
        /// </summary>
        private static void PopNode(WTUIPage page)
        {
            if (m_currentPageNodes == null)
            {
                m_currentPageNodes = new List<WTUIPage>();
            }

            if (page == null)
            {
                Debug.LogError("[UI] page popup is null.");
                return;
            }

            //sub pages should not need back.
            if (CheckIfNeedBack(page) == false)
            {
                return;
            }

            bool _isFound = false;
            for (int i = 0; i < m_currentPageNodes.Count; i++)
            {
                if (m_currentPageNodes[i].Equals(page))
                {
                    m_currentPageNodes.RemoveAt(i);
                    m_currentPageNodes.Add(page);
                    _isFound = true;
                    break;
                }
            }

            //if dont found in old nodes
            //should add in nodelist.
            if (!_isFound)
            {
                m_currentPageNodes.Add(page);
            }

            //弹出新界面的时候隐藏其他界面
            HideOldNodes();
        }

        /// <summary>
        /// 隐藏其他可隐藏的界面
        /// </summary>
        private static void HideOldNodes()
        {
            if (m_currentPageNodes.Count < 0) return;
            WTUIPage topPage = m_currentPageNodes[m_currentPageNodes.Count - 1];
            if (topPage.mode == UIMode.HideOther)
            {
                 
                for (int i = m_currentPageNodes.Count - 2; i >= 0; i--)
                {
                    if (m_currentPageNodes[i].isActive())
                        m_currentPageNodes[i].Hide();
                }
            }
        }

        public static void ClearNodes()
        {
            m_currentPageNodes.Clear();
        }

        private static void ShowPage<T>(Action callback, object pageData, bool isAsync) where T : WTUIPage, new()
        {
            Type t = typeof(T);
            string pageName = t.ToString();

            if (m_allPages != null && m_allPages.ContainsKey(pageName))
            {
                ShowPage(pageName, m_allPages[pageName], callback, pageData, isAsync);
            }
            else
            {
                T instance = new T();
                ShowPage(pageName, instance, callback, pageData, isAsync);
            }
        }

        private static void ShowPage(string pageName, WTUIPage pageInstance, Action callback, object pageData, bool isAsync)
        {
            if (string.IsNullOrEmpty(pageName) || pageInstance == null)
            {
                Debug.LogError("[UI] show page error with :" + pageName + " maybe null instance.");
                return;
            }

            if (m_allPages == null)
            {
                m_allPages = new Dictionary<string, WTUIPage>();
            }

            WTUIPage page = null;
            if (m_allPages.ContainsKey(pageName))
            {
                page = m_allPages[pageName];
            }
            else
            {
                m_allPages.Add(pageName, pageInstance);
                page = pageInstance;
            }

            
            //if (page.isActive() == false)
            {
               
                page.m_data = pageData;

                if (isAsync)
                    page.Show(callback);
                else
                    page.Show();
            }
        }

        /// <summary>
        /// 同步显示
        /// </summary>
        public static void ShowPage<T>() where T : WTUIPage, new()
        {
            ShowPage<T>(null, null, false);
        }

        /// <summary>
        /// 有数据输入的同步显示
        /// </summary>
        public static void ShowPage<T>(object pageData) where T : WTUIPage, new()
        {
            ShowPage<T>(null, pageData, false);
        }

        public static void ShowPage(string pageName, WTUIPage pageInstance)
        {
            ShowPage(pageName, pageInstance, null, null, false);
        }

        public static void ShowPage(string pageName, WTUIPage pageInstance, object pageData)
        {
            ShowPage(pageName, pageInstance, null, pageData, false);
        }

        /// <summary>
        /// 异步显示界面
        /// </summary>
        public static void ShowPage<T>(Action callback) where T : WTUIPage, new()
        {
            ShowPage<T>(callback, null, true);
        }

        /// <summary>
        /// 有数据输入的异步显示界面
        /// </summary>
        public static void ShowPage<T>(Action callback, object pageData) where T : WTUIPage, new()
        {
            ShowPage<T>(callback, pageData, true);
        }

        /// <summary>
        /// 异步显示制定界面
        /// </summary>
        public static void ShowPage(string pageName, WTUIPage pageInstance, Action callback)
        {
            ShowPage(pageName, pageInstance, callback, null, true);
        }

        public static void ShowPage(string pageName, WTUIPage pageInstance, Action callback, object pageData)
        {
            ShowPage(pageName, pageInstance, callback, pageData, true);
        }

        /// <summary>
        /// 关闭顶层界面
        /// </summary>
        public static void ClosePage()
        {
            //Debug.Log("Back&Close PageNodes Count:" + m_currentPageNodes.Count);

            if (m_currentPageNodes == null || m_currentPageNodes.Count <= 1) return;

            WTUIPage closePage = m_currentPageNodes[m_currentPageNodes.Count - 1];
            m_currentPageNodes.RemoveAt(m_currentPageNodes.Count - 1);//从list中移除目前最后一个界面

           
            if (m_currentPageNodes.Count > 0)//还有其他界面
            {
                WTUIPage page = m_currentPageNodes[m_currentPageNodes.Count - 1];//移除过后最后一个界面
                if (page.isAsyncUI)
                    ShowPage(page.name, page, () =>
                    {
                        closePage.Hide();
                    });
                else
                {
                    ShowPage(page.name, page);

                  
                    closePage.Hide();
                }
            }
        }

        /// <summary>
        /// 关闭目标界面
        /// </summary>
        public static void ClosePage(WTUIPage target)
        {
            if (target == null) return;
            if (target.isActive() == false)
            {
                if (m_currentPageNodes != null)
                {
                    for (int i = 0; i < m_currentPageNodes.Count; i++)
                    {
                        if (m_currentPageNodes[i] == target)
                        {
                            m_currentPageNodes.RemoveAt(i);
                            break;
                        }
                    }
                    return;
                }
            }

            if (m_currentPageNodes != null && m_currentPageNodes.Count >= 1 && m_currentPageNodes[m_currentPageNodes.Count - 1] == target)
            {
                m_currentPageNodes.RemoveAt(m_currentPageNodes.Count - 1);
 
                if (m_currentPageNodes.Count > 0)
                {
                    WTUIPage page = m_currentPageNodes[m_currentPageNodes.Count - 1];
                    if (page.isAsyncUI)
                        ShowPage(page.name, page, () =>
                        {
                            target.Hide();
                        });
                    else
                    {
                        ShowPage(page.name, page);
                        target.Hide();
                    }

                    return;
                }
            }
            else if (target.CheckIfNeedBack())
            {
                for (int i = 0; i < m_currentPageNodes.Count; i++)
                {
                    if (m_currentPageNodes[i] == target)
                    {
                        m_currentPageNodes.RemoveAt(i);
                        target.Hide();
                        break;
                    }
                }
            }

            target.Hide();
        }

        public static void ClosePage<T>() where T : WTUIPage
        {
            Type t = typeof(T);
            string pageName = t.ToString();

            if (m_allPages != null && m_allPages.ContainsKey(pageName))
            {
                ClosePage(m_allPages[pageName]);
            }
            else
            {
                Debug.LogError(pageName + "havnt show yet!");
            }
        }

        public static void ClosePage(string pageName)
        {
            if (m_allPages != null && m_allPages.ContainsKey(pageName))
            {
                ClosePage(m_allPages[pageName]);
            }
            else
            {
                Debug.LogError(pageName + " havnt show yet!");
            }
        }

        #endregion

    } 
} 