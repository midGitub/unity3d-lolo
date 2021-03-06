﻿using System;
using UnityEngine;
using LuaInterface;


namespace ShibaInu
{

	/// <summary>
	/// 提供给 lua 调用的相关接口
	/// </summary>
	public static class LuaHelper
	{
		/// 临时使用的 Vector3 对象
		private static Vector3 tmpVec3 = new Vector3 ();




		/// <summary>
		/// 在指定的 gameObject 上添加 DestroyEventDispatcher 脚本。
		/// 当 gameObject 销毁时，在 lua 层（gameObject上）派发 DestroyEvent.DESTROY 事件。
		/// </summary>
		/// <param name="go">Go.</param>
		/// <param name="ed">Ed.</param>
		public static void AddDestroyEvent (GameObject go, LuaTable ed)
		{
			if (go.GetComponent<DestroyEventDispatcher> () == null)
				go.AddComponent<DestroyEventDispatcher> ().ed = ed;
		}


		/// <summary>
		/// 在指定的 gameObject 上添加 PointerEventDispatcher 脚本。
		/// 当 gameObject 与鼠标指针（touch）交互时，派发相关事件。
		/// </summary>
		/// <param name="go">Go.</param>
		/// <param name="ed">Ed.</param>
		public static void AddPointerEvent (GameObject go, LuaTable ed)
		{
			PointerEventDispatcher dispatcher = go.GetComponent<PointerEventDispatcher> ();
			if (dispatcher == null)
				dispatcher = go.AddComponent<PointerEventDispatcher> ();
			dispatcher.ed = ed;
		}


		/// <summary>
		/// 在指定的 gameObject 上添加 DragDropEventDispatcher 脚本。
		/// 当 gameObject 与鼠标指针（touch）交互时，派发拖放相关事件。
		/// </summary>
		/// <param name="go">Go.</param>
		/// <param name="ed">Ed.</param>
		public static void AddDragDropEvent (GameObject go, LuaTable ed)
		{
			if (go.GetComponent<DragDropEventDispatcher> () == null)
				go.AddComponent<DragDropEventDispatcher> ().ed = ed;
		}


		/// <summary>
		/// 在指定的 gameObject 上添加 AvailabilityEventDispatcher 脚本。
		/// 当 gameObject 可用性有改变时（OnEnable() / OnDisable()），派发 AvailabilityEvent.CHANGED 事件
		/// </summary>
		/// <param name="go">Go.</param>
		/// <param name="ed">Ed.</param>
		public static void AddAvailabilityEvent (GameObject go, LuaTable ed)
		{
			if (go.GetComponent<AvailabilityEventDispatcher> () == null)
				go.AddComponent<AvailabilityEventDispatcher> ().ed = ed;
		}


		/// <summary>
		/// 创建并返回一个空 GameObject
		/// </summary>
		/// <returns>The game object.</returns>
		/// <param name="name">名称</param>
		/// <param name="parent">父节点</param>
		/// <param name="notUI">是否不是 UI 对象</param>
		public static GameObject CreateGameObject (string name, Transform parent, bool notUI)
		{
			GameObject go;
			if (notUI)
				go = new GameObject (name);
			else {
				go = new GameObject (name, typeof(RectTransform));
				go.layer = LayerMask.NameToLayer ("UI");
			}
			
			if (parent != null) {
				SetParent (go.transform, parent);
			}

			return go;
		}


		/// <summary>
		/// 设置 target 的父节点为 parent。
		/// 设置 target.layer 属性。
		/// 并将 localScale, localPosition 属性重置。
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="parent">Parent.</param>
		public static void SetParent (Transform target, Transform parent)
		{
			SetLayerRecursively (target, parent.gameObject.layer);
			target.SetParent (parent, true);
			target.localScale = Vector3.one;
			target.localPosition = Vector3.zero;
		}


		/// <summary>
		/// 设置目标对象，以及子节点的所属图层
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="layer">Layer.</param>
		public static void SetLayerRecursively (Transform target, int layer)
		{
			target.gameObject.layer = layer;
			foreach (Transform child in target)
				SetLayerRecursively (child, layer);
		}


		/// <summary>
		/// 将世界（主摄像机）坐标转换成 UICanvas 坐标
		/// </summary>
		/// <returns>The to canvas point.</returns>
		/// <param name="pos">world position</param>
		public static Vector3 WorldToCanvasPoint (Vector3 pos)
		{
			pos = Camera.main.WorldToScreenPoint (pos);
			pos = Stage.uiCanvas.worldCamera.ScreenToWorldPoint (pos);
			Vector3 s = Stage.uiCanvasTra.localScale;
			tmpVec3.Set (pos.x / s.x, pos.y / s.y, Stage.uiCanvasTra.anchoredPosition3D.z);
			return tmpVec3;
		}


		/// <summary>
		/// 将屏幕坐标转换成 UICanvas 坐标
		/// </summary>
		/// <returns>The to canvas point.</returns>
		/// <param name="pos">Position.</param>
		/// <param name="parent">Parent.</param>
		public static Vector3 ScreenToCanvasPoint (Vector3 pos, RectTransform parent = null)
		{
			if (parent == null)
				parent = Stage.uiLayer;
			
			Vector2 p = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle (parent, pos, Stage.uiCanvas.worldCamera, out p);
			tmpVec3.Set (p.x, p.y, Stage.uiCanvasTra.anchoredPosition3D.z);
			return tmpVec3;
		}


		/// <summary>
		/// 发送一条 http 请求，并返回对应 HttpRequest 实例
		/// </summary>
		/// <returns>The http request.</returns>
		/// <param name="url">URL.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="postData">Post data.</param>
		public static HttpRequest SendHttpRequest (string url, LuaFunction callback, string postData)
		{
			HttpRequest req = new HttpRequest ();
			req.url = url;

			if (postData != null) {
				req.method = HttpRequestMethod.POST;
				req.postData = postData;
			}

			if (callback != null)
				req.SetLuaCallback (callback);

			req.Send ();
			return req;
		}



		/// <summary>
		/// 添加或获取 GameObject 下的组件
		/// </summary>
		/// <returns>The or get component.</returns>
		/// <param name="go">Go.</param>
		/// <param name="componentType">Component type.</param>
		public static Component AddOrGetComponent (GameObject go, Type componentType)
		{
			Component c = go.GetComponent (componentType);
			if (c == null)
				c = go.AddComponent (componentType);
			return c;
		}


		/// <summary>
		/// 获取指定名字（gameObject.name）的标记点 GameObject
		/// </summary>
		/// <returns>The mark point game object.</returns>
		/// <param name="root">根节点</param>
		/// <param name="name">需匹配的 GameObject 名称</param>
		public static GameObject GetMarkPointGameObject (GameObject root, string name)
		{
			MarkPoint[] list = root.GetComponentsInChildren<MarkPoint> (true);
			foreach (MarkPoint mp in list) {
				GameObject go = mp.gameObject;
				if (go.name == name)
					return go;
			}
			return null;
		}



		/// <summary>
		/// 在控制台打印一条错误日志
		/// </summary>
		/// <param name="msg">Message.</param>
		public static void ConsoleLogError (string msg)
		{
			Debug.LogError (msg);
		}



		#region 后处理效果


		/// <summary>
		/// 播放叠影抖动效果
		/// </summary>
		/// <returns>The double image shake.</returns>
		/// <param name="duration">Duration.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="interval">Interval.</param>
		/// <param name="cam">Cam.</param>
		public static DoubleImageShake PlayDoubleImageShake (float duration, LuaFunction callback = null, float x = 35f, float y = 10f, float interval = 0.045f, Camera cam = null)
		{
			if (cam == null)
				cam = Camera.main;

			DoubleImageShake dis = (DoubleImageShake)AddOrGetComponent (cam.gameObject, typeof(DoubleImageShake));
			if (dis.shader == null)
				dis.shader = (Shader)ResManager.LoadAsset ("Shaders/PostEffect/DoubleImageShake.shader", Stage.currentSceneName);

			Action action = null;
			if (callback != null)
				action = () => {
					callback.BeginPCall ();
					callback.Call ();
					callback.EndPCall ();
				};
			dis.Play (duration, action, x, y, interval);
			return dis;
		}


		/// <summary>
		/// 播放马赛克效果
		/// </summary>
		/// <returns>The mosaic.</returns>
		/// <param name="toTileSize">To tile size.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="cam">Cam.</param>
		public static Mosaic PlayMosaic (float toTileSize, float duration, LuaFunction callback = null, Camera cam = null)
		{
			if (cam == null)
				cam = Camera.main;
			
			Mosaic mosaic = (Mosaic)AddOrGetComponent (cam.gameObject, typeof(Mosaic));
			if (mosaic.shader == null)
				mosaic.shader = (Shader)ResManager.LoadAsset ("Shaders/PostEffect/Mosaic.shader", Stage.currentSceneName);

			Action action = null;
			if (callback != null)
				action = () => {
					callback.BeginPCall ();
					callback.Call ();
					callback.EndPCall ();
				};
			mosaic.Play (toTileSize, duration, action);
			return mosaic;
		}


		/// <summary>
		/// 播放径向模糊效果
		/// </summary>
		/// <returns>The radial blur.</returns>
		/// <param name="toBlurFactor">To blur factor.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="cam">Cam.</param>
		public static RadialBlur PlayRadialBlur (float toBlurFactor, float duration, LuaFunction callback = null, Camera cam = null)
		{
			if (cam == null)
				cam = Camera.main;

			RadialBlur radialBlur = (RadialBlur)AddOrGetComponent (cam.gameObject, typeof(RadialBlur));
			if (radialBlur.shader == null)
				radialBlur.shader = (Shader)ResManager.LoadAsset ("Shaders/PostEffect/RadialBlur.shader", Stage.currentSceneName);

			Action action = null;
			if (callback != null)
				action = () => {
					callback.BeginPCall ();
					callback.Call ();
					callback.EndPCall ();
				};
			radialBlur.Play (toBlurFactor, duration, action);
			return radialBlur;
		}


		/// <summary>
		/// 启用或禁用高斯模糊效果
		/// </summary>
		/// <returns>The gaussian blur enabled.</returns>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="blurRadius">Blur radius.</param>
		/// <param name="downSample">Down sample.</param>
		/// <param name="iteration">Iteration.</param>
		/// <param name="cam">Cam.</param>
		public static GaussianBlur SetGaussianBlurEnabled (bool enabled, float blurRadius = 0.6f, int downSample = 2, int iteration = 1, Camera cam = null)
		{
			if (cam == null)
				cam = Camera.main;

			GameObject camGO = cam.gameObject;
			GaussianBlur gaussianBlur = camGO.GetComponent<GaussianBlur> ();

			if (enabled) {
				if (gaussianBlur == null) {
					gaussianBlur = camGO.AddComponent<GaussianBlur> ();
					gaussianBlur.shader = (Shader)ResManager.LoadAsset ("Shaders/PostEffect/GaussianBlur.shader", Stage.currentSceneName);
				} else
					gaussianBlur.enabled = true;

				gaussianBlur.blurRadius = blurRadius;
				gaussianBlur.downSample = downSample;
				gaussianBlur.iteration = iteration;
			}

			//
			else {
				if (gaussianBlur != null)
					gaussianBlur.enabled = false;
			}

			return gaussianBlur;
		}

		#endregion


		//
	}
}

