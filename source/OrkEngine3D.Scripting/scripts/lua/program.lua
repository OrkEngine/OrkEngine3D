print("Hello, from lua!")

function RunMath(val1, val2)
	if val1 > val2 then
		return val1 + 1
	else
		return val2 - 1
	end
end

function OnLoad()

	print("OnEngineLoad")

end

function OnUpdate()

	print("OnEngineUpdate")

end

function OnRender()

	print("OnEngineRender")

end