using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[LuaCallCSharp]
public class TestLuaBehaviourScript : MonoBehaviour
{
    public GameObject sprite;
    public TextAsset luaScript;

    [CSharpCallLua]
    public interface ICalc
    {
        int Add(int a, int b);
        int Mult { get; set; }
    }
    [CSharpCallLua]
    public delegate ICalc CalcNew(int mult, params string[] args);


    private LuaEnv luaEnv = new LuaEnv();
    private Action luaStart;
    private Action luaRun;
    private Action luaDestroy;
    private LuaTable scriptEnv;



    private void Awake()
    {
        var button_start = GameObject.Find("Canvas/Button_Start");
       
        
         scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", sprite);
         
        luaEnv.DoString(luaScript.text, "testXLua", scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("run", out luaRun);
        scriptEnv.Get("destroy", out luaDestroy);

      
        if (luaAwake != null)
        {
            luaAwake();
        }
        Test(luaEnv);
    }

    void Test(LuaEnv luaenv)
    {

        CalcNew calc_new = luaenv.Global.GetInPath<CalcNew>("Calc.New");
        Debug.Log("calc_new:" + calc_new);
        ICalc calc = calc_new(10, "hi", "john"); //constructor
        Debug.Log("sum(*10) =" + calc.Add(1, 2));
        calc.Mult = 100;
        Debug.Log("sum(*100)=" + calc.Add(1, 2));
    }
  
    private void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    private float speed = 0.5f;
    private void Update()
    {
       
        //sprite.transform.Rotate(new Vector3(0, 0, speed));
        if (luaRun != null)
        {
           
            luaRun();
        }
    }

    void OnDestroy()
    {
        if (luaDestroy != null)
        {
            luaDestroy();
        }
        luaDestroy = null;
        luaRun = null;
        luaStart = null;
        scriptEnv.Dispose();
       
    }
}