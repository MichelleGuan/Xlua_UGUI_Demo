using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[Hotfix]
public class TestHotfixScript : MonoBehaviour {
    public TextAsset luaScript;
    public GameObject text;


    private LuaEnv luaEnv = new LuaEnv();
    private LuaTable scriptEnv;
    // Use this for initialization
    void Start () {
      

        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", text);
        luaEnv.DoString(luaScript.text, "Hotfix", scriptEnv);

        Debug.Log("c#_Start");
        Action luaAwake = scriptEnv.Get<Action>("Start");
        if (luaAwake != null)
        {
            luaAwake();
        }
    }
	
	// Update is called once per frame
	void Update () {
        test();
    }

    private void test()
    {
        print("txt");
    }
}
