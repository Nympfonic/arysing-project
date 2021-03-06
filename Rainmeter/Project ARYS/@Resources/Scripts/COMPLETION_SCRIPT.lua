local UpdateValues, compare, getList, sortList, calcSpaces, getWeight, split
local keys = {}
local pressed = {}
local choices = {}
local weightTable = {}
local cur = ""
local running = false
local predicted = ""
local timing = 0
local selection = 1
local maxim = 1
local cursor = ""
local max_shown=0
local scroll = 0
local cursor_active="▁"
local fileQueue=0
local folderQueue=0
local loading_files = true
local browser = "firefox"

local scroll_dir = 0
local scroll_timing = 0

local cursor_offset = 0

function Update()
	if (loading_files) then return "Loading Applications..." end

	if (not running) then
		if (cur == '') then
			SKIN:Bang('!HideMeter', 'METER_COMPLETE_STRING')
			SKIN:Bang('!SetVariable', 'COMMAND', "")
			SKIN:Bang('!SetVariable', 'CURRENT', "")
			SKIN:Bang('!HideMeter', 'METER_DISPLAY_FINAL_COMMAND')
			return SKIN:GetVariable('DefaultText', '...') 
		else
			return calcSpaces() .. cursor_active
		end
	else
		return calcSpaces() .. cursor
	end
	
end

function RestartScroll()
	if scroll_dir ~= 0 then
		SKIN:Bang("!CommandMeasure", "MEASURE_SCROLL", "Stop 1")
		SKIN:Bang("!CommandMeasure", "MEASURE_SCROLL", "Execute 1")
	end
end

function ScrollOn()
	if (scroll_dir == -1) then
		if (selection > 1) then
			selection = selection - 1
			
			if (selection - scroll <= 0) then scroll = scroll - 1 end
			UpdateValues()
		end
	elseif (scroll_dir == 1) then
		if (selection < maxim) then
			selection = selection + 1
					
			if (selection > max_shown + scroll) then scroll = scroll + 1 end
			UpdateValues()
		end
	elseif (scroll_dir == 2) then
		if cursor_offset > 0 then
			cursor_offset = cursor_offset - 1
			UpdateValues()
		end
	elseif (scroll_dir == 3) then
		if cursor_offset < string.len(cur) then
			cursor_offset = cursor_offset + 1
			UpdateValues()
		end
	end
end

function RestartCursor()
	if running then		
		SKIN:Bang("!CommandMeasure", "MEASURE_CURSOR", "Stop 1")
		SKIN:Bang("!CommandMeasure", "MEASURE_CURSOR", "Execute 1")
	end
end

function CursorOn()
	cursor = cursor_active
end

function CursorOff()
	cursor = " "
end

function compare(a,b)
	if (string.find(a, cur, 1, true) ~= string.find(b, cur, 1, true)) then return string.find(a, cur, 1, true) < string.find(b, cur, 1, true) end
	if (string.len(a) == string.len(b)) then return a < b end
	return string.len(a) < string.len(b)
end

function getList()
	local pred = {}
	for c, value in pairs(choices) do 
		if (string.find(c, cur, 1, true) ~= nil) then table.insert(pred, c) end
	end
	return pred
end

function sortList(pred)
	table.sort(pred, compare)
	for i = 1,#pred do
		for e = i, #pred do
			local a = weightTable[pred[e]] or 0
			local b = weightTable[pred[i]] or 0
			if a > b then pred[e],pred[i] = pred[i],pred[e] end
		end
	end
	maxim = #pred
end

function calcSpaces()
	s = ""
	for i = 1, string.len(cur) - cursor_offset do s = s.. string.sub(cur, i, i) end
	return s
end

function UpdateValues()
	local commandvar = ""
	local selectionvar = ""
	local curvar = cur
	predicted = ""
	
	if (cur ~= "") then
		local pred = getList()
		
		if (#pred > 0) and (selection <= #pred) then
			sortList(pred)
			predicted = pred[selection]
			selectionvar = predicted
			
			if (string.find(predicted, cur, 1, true) > 1) then SKIN:Bang('!HideMeter', 'METER_COMPLETE_STRING') else SKIN:Bang('!ShowMeter', 'METER_COMPLETE_STRING') end
			
			commandvar = ""
			if (scroll > 0) then commandvar = '...\n' end
			for i = 1 + scroll, math.min(max_shown + scroll, maxim + scroll) do commandvar = commandvar .. pred[i] .. '\n' end			
			if (math.min(max_shown, maxim) < maxim) and (scroll < maxim - max_shown) then commandvar = commandvar .. '...' end
			
			SKIN:Bang('!SetOption', 'METER_DISPLAY_FINAL_COMMAND', 'InlineSetting', 'Underline')
			SKIN:Bang('!SetOption', 'METER_DISPLAY_FINAL_COMMAND', 'SolidColor', [[#ColorResults#]])
		elseif (string.sub(cur, 1, 2) == "$ ") then
			commandvar = cur
			selectionvar = cur
			SKIN:Bang('!SetOption', 'METER_DISPLAY_FINAL_COMMAND', 'InlineSetting', [[Color | #CodeColor#]])
			SKIN:Bang('!SetOption', 'METER_DISPLAY_FINAL_COMMAND', 'SolidColor', [[#CodeBackgroundColor#]])
			SKIN:Bang('!HideMeter', 'METER_COMPLETE_STRING')
		elseif (string.match(cur, "^!" )) then
			commandvar = cur
			selectionvar = cur
			SKIN:Bang('!SetOption', 'METER_DISPLAY_FINAL_COMMAND', 'InlineSetting', [[Color | #CommandColor#]])
			SKIN:Bang('!SetOption', 'METER_DISPLAY_FINAL_COMMAND', 'SolidColor', [[#CommandBackgroundColor#]])
			SKIN:Bang('!HideMeter', 'METER_COMPLETE_STRING')		
		end
	end

	SKIN:Bang('!SetVariable', 'CURRENT', curvar)
	SKIN:Bang('!SetVariable', 'COMMAND', commandvar)
	SKIN:Bang('!SetVariable', 'SELECTION', selectionvar)
	if (SKIN:GetVariable('COMMAND') == "") then SKIN:Bang('!HideMeter', 'METER_DISPLAY_FINAL_COMMAND') else SKIN:Bang('!ShowMeter', 'METER_DISPLAY_FINAL_COMMAND') end
	SKIN:Bang('!Update')
end

function Suspend()
	if running then
		running = false
		print('Suspending Input')
		SKIN:Bang([[!CommandMeasure RUN_KEYPRESS "Kill"]]) -- Don't send args to stop AHK script
	end
	timing = 0
end

function End()
	if (running) and (cur ~= "") then
		print("Ending Input")
		keys = {}
		pressed = {}
		running = false
		SKIN:Bang([[!CommandMeasure RUN_KEYPRESS "Kill"]]) -- Don't send args to stop AHK script
		cursor_offset = 0
		
		if choices[predicted] ~= nil then
			cur = predicted
			getWeight()
			if (weightTable[cur] == nil) then weightTable[cur] = 1 else weightTable[cur] = weightTable[cur] + 1 end
			local weightStorage = io.open(SKIN:MakePathAbsolute("WEIGHT_STORAGE.txt"),"w+")
			for ke,v in pairs(weightTable) do weightStorage:write(ke .. ";" .. v .. "\n") end
			weightStorage:close()
			
			local command = choices[cur]
			
			if string.match(command,"^break>") or string.match(command,"^start") then
				print(os.execute(command))
				SKIN:Bang("!Refresh")
			else
				SKIN:Bang('\["' .. command .. '"\]')
			end
		elseif string.match(cur, "^$ ") then
			print(os.execute([[start cmd /k ]] .. string.sub(cur, 3, string.len(cur))))
		elseif string.match(cur, "^!") then
			if string.match(cur, "^!search ") then
				print(os.execute("start " .. browser .. " google.com/search?q=\"" .. string.sub(cur, 9, string.len(cur)) .. "\""))
			elseif string.match(cur, "^!goto ") then
				print(os.execute("start " .. browser .. " \"" .. string.sub(cur, 7, string.len(cur)) .. "\""))
			end
		end
		cur = ""
		print("Input Ended")
	end
end

function split(inputstr, sep)
        if sep == nil then
                sep = "%s"
        end
        local t={} ; i=1
        for str in string.gmatch(inputstr, "([^"..sep.."]+)") do
                t[i] = str
                i = i + 1
        end
        return t
end

function Start()
	if not running and not loading_files then
		print('Starting Input')
		SKIN:Bang("!CommandMeasure", "MEASURE_CURSOR", "Execute 1")
		max_shown = SKIN:GetVariable('MaxItems')
		browser = SKIN:GetVariable('Browser')
		SKIN:Bang([[!CommandMeasure RUN_KEYPRESS "Run"]])
		running = true
		timing = 0
		UpdateValues()
	end
end

function Initialize()
	cursor_active = SKIN:GetVariable('CURSOR')

	fileView = SKIN:GetMeasure('MeasureChild1')
	pathView = SKIN:GetMeasure('MeasureChild2')
	fileCount = SKIN:GetMeasure('MeasureFileCount')

--GET COMMAND LIST
	local FilePath = SKIN:MakePathAbsolute("COMMAND_LIST.txt")
	local File = io.open(FilePath)
	local Contents = File:read('*all')
	File:close()
	
	local s = split(Contents, "\n")
	
	for i = 1, table.getn(s) do
		local com = split(s[i], ";")
		choices[com[1]] = com[2]
	end
	
	choices["# edit command list"] = "start notepad \"" .. FilePath .. "\""
	choices["# reset frequent list"] = 'break>"' .. SKIN:MakePathAbsolute('WEIGHT_STORAGE.txt') .. '"'
	folderQueue = 1
	fileQueue = 2
	getWeight()
end

function gatherShortcutFile()
	local curFile = fileView:GetStringValue()
	local curPath = pathView:GetStringValue()
	if curFile ~= '' and curFile ~= '..' then
		choices[curFile:lower()] = curPath
	end
	
	fileQueue = fileQueue+1
	if fileQueue <= fileCount:GetValue() + 1 then
		SKIN:Bang('[!SetOption MeasureChild1 Index '..fileQueue..'][!SetOption MeasureChild2 Index '..fileQueue..'][!CommandMeasure MeasureFolder "Update"][!Update]')
	end
	
	if fileQueue > fileCount:GetValue() + 1 then
		folderQueue = folderQueue + 1
		folderPath = SKIN:GetVariable('ShortcutFolder'..folderQueue)
		if folderPath and folderPath ~= '' then
			fileQueue = 2
			SKIN:Bang('[!SetOption MeasureFileCount Folder "#ShortcutFolder'..folderQueue..'#"][!UpdateMeasure MeasureFileCount]'
					..'[!SetOption MeasureFolder Path "#ShortcutFolder'..folderQueue..'#"][!SetOption MeasureChild1 Index 2][!SetOption MeasureChild2 Index 2][!CommandMeasure MeasureFolder "Update"]')
		else
			loading_files = false
			SKIN:Bang("!Update")
		end
	end
end

function KeyDown(k)
	if (running) then
		if (k == 'Up') then
			scroll_dir = -1
			SKIN:Bang("!CommandMeasure", "MEASURE_SCROLL", "Execute 1")
		elseif (k == 'Down') then
			scroll_dir = 1
			SKIN:Bang("!CommandMeasure", "MEASURE_SCROLL", "Execute 1")
		elseif (k == 'Right') then
			scroll_dir = 2
			SKIN:Bang("!CommandMeasure", "MEASURE_SCROLL", "Execute 1")
		elseif (k == 'Left') then
			scroll_dir = 3
			SKIN:Bang("!CommandMeasure", "MEASURE_SCROLL", "Execute 1")
		else
			keys[k] = true
		end
	end
end

function KeyUp(k)
	if (running) then
		if (k == 'Up') or (k == 'Down') or (k == 'Right') or (k == 'Left') then
			scroll_dir = 0
		else
			keys[k] = nil
			KeyPressed(k)
		end
	end
end

function KeyPressed(k)
	if (running) then
		if (k == 'BS') then
			if (string.len(cur) - cursor_offset > 0) then
				cur = string.sub(cur, 0, (string.len(cur) - 1) - cursor_offset) .. string.sub(cur, (string.len(cur) + 1) - cursor_offset, string.len(cur))
				scroll = 0
				selection = 1
			end
		elseif (k == 'Delete') then
			if (cursor_offset > 0) then
				cur = string.sub(cur, 0, (string.len(cur)) - cursor_offset) .. string.sub(cur, (string.len(cur) + 2) - cursor_offset, string.len(cur))
				cursor_offset = cursor_offset - 1
				scroll = 0
				selection = 1
			end
		elseif (k == '\t') then
			if not pcall(End) then print('Error ending input (Please Report This!)') end
			scroll = 0
			selection = 1
		elseif (k == '\n') then
			if not pcall(End) then print('Error ending input (Please Report This!)') end
			scroll = 0
			selection = 1
		else
			if (k == "QUOT") then k = "\"" end
			cur = string.sub(cur, 0, (string.len(cur)) - cursor_offset) .. k .. string.sub(cur, (string.len(cur) + 1) - cursor_offset, string.len(cur))
			scroll = 0
			selection = 1
		end
		
		if not pcall(UpdateValues) then print('Error updating values (Please Report This!)') end
	end
end

function getWeight()
	print('Getting Weights')
	local weightStorage = io.open(SKIN:MakePathAbsolute('WEIGHT_STORAGE.txt'),'r')
	weightTable = {}
	if not weightStorage then return end
	local line = weightStorage:read('*line')
	while line do
		local t = split(line, ';')
		local ke,v = t[1],t[2]
		print(ke,v)
		weightTable[ke] = tonumber(v)
		line = weightStorage:read('*line')
	end
	weightStorage:close()
	print('Weights Ended')
end