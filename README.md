# UGUI_XluaDemo
<pre>
This is an UGUI demo under MVC framework which can be used on production environment.
I also use Xlua on login moudle, both hitfix condition and common use.
</pre>
1.在hotfix>lua文件夹写lua脚本，在继承WTUIPage的C#脚本引用该lua地址，并转换成byte格式。
2.Hotfix-enable后，可以使用hotfix脚本，替换现有方法。
3.生成热更代码之后，替换lua脚本就可以，不需要再次生成。
4.全局只有一个luaenv,_luaLogin = XLuaUtil.GlobLuaEnv.NewTable()声明。
