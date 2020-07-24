function Initialize()
  commands = {["system::help"]=SYSTEMHELP,["system::shutdown"]=SYSTEMSHUTDOWN}

  sFileToRead = SELF:GetOption('ConsoleLog')

  hasInitialized = false
  mInputStr = SKIN:GetMeasure('mInputString')

  --ReadFileLines("verbose.txt")
end

function RunCommand(func)
  return commands[func]()
end

SYSTEMHELP = function ()
  SKIN:Bang('')
end

SYSTEMSHUTDOWN = function ()
  SKIN:Bang('!DeactivateConfig')
end

-- function ReadFileLines(filePath)
-- 	-- HANDLE RELATIVE PATH OPTIONS.
-- 	filePath = SKIN:GetVariable('@')..'Scripts\\'..filePath
--
-- 	-- OPEN FILE.
-- 	local file = io.open(filePath)
--
-- 	-- HANDLE ERROR OPENING FILE.
-- 	if not file then
-- 		print('ReadFile: unable to open file at ' .. filePath)
-- 		return
-- 	end
--
-- 	-- READ FILE CONTENTS AND CLOSE.
-- 	local contents = {}
-- 	for line in file:lines() do
-- 		table.insert(contents, line)
-- 	end
--
-- 	file:close()
--
-- 	return contents
-- end

function ReadConsoleLog()
  hReadingFile = io.open(sFileToRead)
	if not hReadingFile then
		print('Console: unable to open file at ' .. sFileToRead)
		return
	end
  sAllText = hReadingFile:read("*all")
	sAllText = string.gsub(sAllText, "\t", "     ")
	io.close(hReadingFile)

	return tostring(sAllText)
end

function PrintToConsole(input)
  hReadingFile = io.open(sFileToRead, "a")
  io.output(sFileToRead)
  if not hReadingFile then
    print('Console: unable to open file at ' .. sFileToRead)
		return
	end

end

function CheckCommandExist(cmdName)
  for i, v in pairs(commands) do
    if i == cmdName then
      return true
    end
  end
  return false
end

function Update()
  if hasInitialized == true then
    local inputStr = mInputStr:GetStringValue()
    -- If input is an existing command
    if inputStr ~= "" and CheckCommandExist(inputStr) then
      print("Valid Command: "..inputStr)
      RunCommand(inputStr)
    else
      print("Invalid Command: "..inputStr)
    end
    -- Print any console input
    if inputStr ~= nil then
      PrintToConsole(inputStr)
    end
  elseif hasInitialized == false then
    hasInitialized = true
    print("Initialized")
  end
  return ReadConsoleLog()
end
