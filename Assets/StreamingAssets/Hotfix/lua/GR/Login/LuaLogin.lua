local label_account
local label_password


function init_ui()
	print(1)
	label_account = CS.UnityEngine.GameObject.Find("Content/Account/Text")
	label_password = CS.UnityEngine.GameObject.Find("Content/Password/Text")

	print(2)
	CS.UnityEngine.GameObject.Find("Content/Loginregister"):GetComponent("Button").onClick:AddListener(function()
		self:Hide()
	end)
	print(3)

	CS.UnityEngine.GameObject.Find("Content/LoginConfirm"):GetComponent("Button").onClick:AddListener(on_click_login_call_back)
	print(4)
end

function on_click_login_call_back()
	print("account:"..label_account:GetComponent("Text").text)
	print("password:"..label_password:GetComponent("Text").text)

	self:CallEventLogin(label_account:GetComponent("Text").text,label_password:GetComponent("Text").text)
end