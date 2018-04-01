--
-- 与代码无关的IDE提示等定义
-- 该文件不会被require，不会被打包发布
-- 2017/9/27
-- Author LOLO
--


--=------------------------------[ Lua实现 ]------------------------------=--

-- Vector2
---@class UnityEngine.Vector2
---@field x number
---@field y number
---@field normalized Vector2 @ 返回向量的长度为1（只读）
---@field magnitude number @ 返回向量的长度（只读）
---@field sqrMagnitude number @ 返回这个向量的长度的平方（只读）
---
---@field up Vector2 @    Vector2(0, 1)
---@field right Vector2 @ Vector2(1, 0)
---@field zero Vector2 @  Vector2(0, 0)
---@field one Vector2 @   Vector2(1, 1)
---
---@field Set fun(x:number, y:number)
---@field Get fun() : x_and_y
---@field Clone fun():Vector2
---
---@class Vector2 : UnityEngine.Vector2


-- Vector3
---@class UnityEngine.Vector3
---@field x number
---@field y number
---@field z number
---@field normalized Vector3 @ 返回向量的长度为1（只读）
---@field magnitude number @ 返回向量的长度（只读）
---@field sqrMagnitude number @ 返回这个向量的长度的平方（只读）
---
---@field zero Vector3 @    Vector3(0, 0, 0)
---@field one Vector3 @     Vector3(1, 1, 1)
---@field forward Vector3 @ Vector3(0, 0, 1)
---@field up Vector3 @      Vector3(0, 1, 0)
---@field right Vector3 @   Vector3(1, 0, 0)
---
---@field Clone fun():Vector3
---@field Set fun(x:number, y:number, z:number)
---@field Get fun() : x_and_y_and_z
---
---@field New fun(x : number, y : number, z : number):Vector3
---@field Lerp fun(from : Vector3, to : Vector3, t : float) : Vector3 @ t是夹在 [0...1]之间，当t = 0时，返回from，当t = 1时，返回to。当t = 0.5 返回from和to的平均数。
---@field Slerp fun(from : Vector3, to : Vector3, t : float) : Vector3 @ 两个向量之间的弧形插值。通过t数值在from和to之间插值。返回的向量的长度将被插值到from到to的长度之间。
---@field OrthoNormalize fun(normal : Vector3, tangent : Vector3, binormal : Vector3) : void @ 规范化normal，规范化tangent并且确保它垂直于normal。规范化binormal并确保它到normal和tangent两者之间相互垂直。
---@field MoveTowards fun(current : Vector3, target : Vector3, maxDistanceDelta : float) : Vector3 @ 当前的地点移向目标。这个函数基本上和Vector3.Lerp相同，而是该函数将确保我们的速度不会超过maxDistanceDelta。maxDistanceDelta的负值从目标推开向量，就是说maxDistanceDelta是正值，当前地点移向目标，如果是负值当前地点将远离目标。
---@field RotateTowards fun(current : Vector3, target : Vector3, maxRadiansDelta : float, maxMagnitudeDelta : float) : Vector3 @ 当前的向量转向目标。该向量将旋转在弧线上，而不是线性插值。这个函数基本上和Vector3.Slerp相同，而是该函数将确保角速度和变换幅度不会超过maxRadiansDelta和maxMagnitudeDelta。maxRadiansDelta和maxMagnitudeDelta的负值从目标推开该向量。
---@field SmoothDamp fun(current : Vector3, target : Vector3, currentVelocity : Vector3, smoothTime : float) : Vector3_and_currentVelocity @ 随着时间的推移，逐渐改变一个向量朝向预期的目标。
---@field Scale fun(a : Vector3, b : Vector3) : Vector3 @ 两个矢量组件对应相乘。
---@field Cross fun(lhs : Vector3, rhs : Vector3) : Vector3 @ 两个向量的交叉乘积。返回lhs x rhs
---@field Reflect fun(inDirection : Vector3, inNormal : Vector3) : Vector3 @ 沿着法线反射向量。返回的值是被从带有法线inNormal的表面反射的inDirection。
---@field Dot fun(lhs : Vector3, rhs : Vector3) : float @ 两个向量的点乘积。对于normalized向量，如果他们指向在完全相同的方向，Dot返回1。如果他们指向完全相反的方向，返回-1。对于其他的情况返回一个数（例如：如果是垂直的Dot返回0）。
---@field Project fun(vector : Vector3, onNormal : Vector3) : Vector3 @ 投影一个向量到另一个向量。返回被投影到onNormal的vector。如果onNormal接近0，返回 0 vector。
---@field Angle fun(from : Vector3, to : Vector3) : float @ 由from和to两者返回一个角度。形象的说，from和to的连线和它们一个指定轴向的夹角。
---@field Distance fun(a : Vector3, b : Vector3) : float @ 返回a和b之间的距离。
---@field ClampMagnitude fun(vector : Vector3, maxLength : float) : Vector3 @ 返回向量的长度，最大不超过maxLength所指示的长度。也就是说，钳制向量长度到一个特定的长度。
---@field Min fun(lhs : Vector3, rhs : Vector3) : Vector3 @ 返回一个由两个向量的最小组件组成的向量。
---@field Max fun(lhs : Vector3, rhs : Vector3) : Vector3 @ 返回一个由两个向量的最大组件组成的向量。
---
---@class Vector3 : UnityEngine.Vector3


-- Quaternion
---@class UnityEngine.Quaternion @ 四元数
---@field x number
---@field y number
---@field z number
---@field w number
---@field eulerAngles Vector3 @ 旋转的欧拉角度
---
---@field identity @ Quaternion(0, 0, 0, 1)
---
---@field New fun(x : number, y : number, z : number, w : number) : Quaternion
---
---@class Quaternion : UnityEngine.Quaternion


-- Color
---@class UnityEngine.Color @ 颜色
---@field r number
---@field g number
---@field b number
---@field a number
---@field grayscale number @ 颜色的灰度值（只读）
---
---@field gamma Color @ A version of the color that has had the gamma curve applied.
---@field linear Color @ A linear value of an sRGB color. Colors are typically expressed in sRGB color space. This property returns "linearized" color value, i.e. with inverse of sRGB gamma curve applied.
---@field maxColorComponent number @ Returns the maximum color component value: Max(r,g,b).
---
---@field New fun(r:number, g:number, b:number, a:number):Color
---@field Lerp fun(a : Color, b : Color, t : float) : Color @ 通过t在颜色a和b之间插值。"t"是夹在0到1之间的值。当t是0时返回颜色a。当t是1时返回颜色b。
---@field LerpUnclamped fun(a : Color, b : Color, t : float) : Color @  Linearly interpolates between colors a and b by t.When t is 0 returns a. When t is 1 returns b.
---@field HSVToRGB fun(H:number, S:number, V:number, hdr:boolean):Color @ Color An opaque colour with HSV matching the input.
---@field RGBToHSV fun(rgbColor:Color):[H, S, V] @ Calculates the hue, saturation and value of an RGB input color.
---
---@field red Color @ 纯红色。 RGBA 是 (1, 0, 0, 1) 。
---@field green Color @ 纯绿色。 RGBA 是 (0, 1, 0, 1)。
---@field blue Color @ 纯蓝色。RGBA 是 (0, 1, 0, 1)。
---@field white Color @ 纯白色。 RGBA 是 (1, 1, 1, 1) 。
---@field black Color @ 纯黑色。RGBA 是 (0, 0, 0, 1) 。
---@field yellow Color @ 黄色。 RGBA 是怪异的 (1, 235/255, 4/255, 1) ， 但是颜色看起来漂亮！
---@field cyan Color @ 青色。 RGBA 是 (0, 1, 1, 1)。
---@field magenta Color @ 紫红色。 RGBA 是 (1, 0, 1, 1)。
---@field gray Color @ 灰色。 RGBA 是 (0.5, 0.5, 0.5, 1) 。
---@field clear Color @ 完全透明。RGBA 是 (0, 0, 0, 0) 。
---
---@class Color : UnityEngine.Color
