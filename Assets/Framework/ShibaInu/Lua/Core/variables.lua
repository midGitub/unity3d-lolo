--
-- 全局变量定义
-- 2017/10/12
-- Author LOLO
--

local require = require
local setmetatable = setmetatable

EventDispatcher = require("Events.EventDispatcher")



-- C# Class
Res = setmetatable({ _ed = EventDispatcher.New() }, { __index = ShibaInu.ResManager }) ---@type ShibaInu.ResManager
LuaHelper = ShibaInu.LuaHelper
--Stage = ShibaInu.Stage -- 已整合进 Stage.lua
--Logger = ShibaInu.Logger -- 已整合进 Logger.lua



-- UnityEngine
GameObject = UnityEngine.GameObject
Transform = UnityEngine.Transform
Camera = UnityEngine.Camera
Screen = UnityEngine.Screen
Time = UnityEngine.Time
Application = UnityEngine.Application
Input = UnityEngine.Input
KeyCode = UnityEngine.KeyCode
PlayerPrefs = UnityEngine.PlayerPrefs
Shader = UnityEngine.Shader
Material = UnityEngine.Material


-- DOTween
DOTween = DG.Tweening.DOTween ---@type DG.Tweening.DOTween
DOTween_Enum = {
    ---@type DG.Tweening.AutoPlay
    AutoPlay = DG.Tweening.AutoPlay,
    ---@type DG.Tweening.AxisConstraint
    AxisConstraint = DG.Tweening.AxisConstraint,
    ---@type DG.Tweening.Ease
    Ease = DG.Tweening.Ease,
    ---@type DG.Tweening.LogBehaviour
    LogBehaviour = DG.Tweening.LogBehaviour,
    ---@type DG.Tweening.LoopType
    LoopType = DG.Tweening.LoopType,
    ---@type DG.Tweening.PathMode
    PathMode = DG.Tweening.PathMode,
    ---@type DG.Tweening.PathType
    PathType = DG.Tweening.PathType,
    ---@type DG.Tweening.RotateMode
    RotateMode = DG.Tweening.RotateMode,
    ---@type DG.Tweening.ScrambleMode
    ScrambleMode = DG.Tweening.ScrambleMode,
    ---@type DG.Tweening.TweenType
    TweenType = DG.Tweening.TweenType,
    ---@type DG.Tweening.UpdateType
    UpdateType = DG.Tweening.UpdateType,
}
TweenParams = DG.Tweening.TweenParams ---@type DG.Tweening.TweenParams
DOTween.defaultEaseType = DOTween_Enum.Ease.Linear



-- variables
--- 是否在 LuaJIT 环境中
isJIT = jit ~= nil
isPlaying = Application.isPlaying
isEditor = Application.isEditor
isWindowEditor = Application.platform == UnityEngine.RuntimePlatform.WindowsEditor
isMacEditor = isEditor and not isWindowEditor
isMobile = Application.isMobilePlatform
isAndroid = Application.platform == UnityEngine.RuntimePlatform.Android
isIOS = isMobile and not isAndroid



-- Lua Class
Constants = require("Core.Constants")
Event = require("Events.Event")
LoadResEvent = require("Events.LoadResEvent")
LoadSceneEvent = require("Events.LoadSceneEvent")
DestroyEvent = require("Events.DestroyEvent")
AvailabilityEvent = require("Events.AvailabilityEvent")
PointerEvent = require("Events.PointerEvent")
DragDropEvent = require("Events.DragDropEvent")
TouchEvent = require("Events.TouchEvent")
DataEvent = require("Events.DataEvent")
ListEvent = require("Events.ListEvent")
HttpEvent = require("Events.HttpEvent")
SocketEvent = require("Events.SocketEvent")
AnimationEvent = require("Events.AnimationEvent")

MapList = require("Data.MapList")
LinkedList = require("Data.LinkedList")
RemainTime = require("Data.RemainTime")
Countdown = require("Utils.Countdown")

JSON = require("Utils.JSON")
Logger = require("Utils.Logging.Logger")
Validator = require("Utils.Validator")
TimeUtil = require("Utils.TimeUtil")
ObjectUtil = require("Utils.ObjectUtil")
StringUtil = require("Utils.StringUtil")
MathUtil = require("Utils.MathUtil")
Float3 = require("Utils.Float3")
Handler = require("Utils.Handler")
Timer = require("Utils.Timer")
PrefabPool = require("Utils.Optimize.PrefabPool")

HttpRequest = require("Net.HttpRequest")
HttpDownload = require("Net.HttpDownload")
HttpUpload = require("Net.HttpUpload")
TcpSocket = require("Net.TcpSocket")
UdpSocket = require("Net.UdpSocket")

Stage = require("Core.Stage")
View = require("Views.View")
Module = require("Views.Module")
Scene = require("Views.Scene")
Window = require("Views.Window")

BaseList = require("Components.BaseList")
ScrollList = require("Components.ScrollList")
Picker = require("Components.Picker")
ItemRenderer = require("Components.ItemRenderer")
Animation = require("Components.Animation")
TabNavigator = require("Components.TabNavigator")

Stats = require("UI.Stats")
Profiler = require("Utils.Optimize.Profiler")
