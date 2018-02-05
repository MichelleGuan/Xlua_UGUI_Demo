function btn_login_call_back(account,password)
	self:CallLoginEvent(account,password);
end

function init_control()
	print("c"..1)

	self._uilogin.EventLogin = btn_login_call_back + self._uilogin.EventLogin

	print("c"..2)
	
	print("c"..3)

	
	print("c"..4)
end

