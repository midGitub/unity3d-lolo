﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using UnityEngine;


namespace ShibaInu
{
	/// <summary>
	/// 写入日志文件
	/// </summary>
	public class LogFileWriter
	{
		
		#if UNITY_EDITOR
		public static readonly string FILE_PATH = Application.dataPath + "/../Log/running.log";
		#else
		public static readonly string FILE_PATH = Application.persistentDataPath + "/Log/running.log";
		#endif

		public static readonly string DIR_PATH = Path.GetDirectoryName (FILE_PATH);

		/// 写入文件间隔（毫秒）
		private const int WRITE_INTERVAL = 3000;

		/// 还未写入到文件中的日志列表
		private static List<LogData> s_list = new List<LogData> ();
		/// 锁对象
		private static readonly System.Object LOCK_OBJECT = new System.Object ();
		/// 写入文件定时器是否已经启动了
		private static bool s_running = false;
		/// 是否追加写入文件（日志内容是否已经被清空过了）
		private static bool s_isAppend = false;



		/// <summary>
		/// 添加一条日志
		/// </summary>
		/// <param name="data">Data.</param>
		public static void Append (LogData data)
		{
			lock (LOCK_OBJECT) {
				
				s_list.Add (data);
				if (!s_running) {
					Timer.Once (WRITE_INTERVAL, (Timer timer) => {
						ThreadPool.QueueUserWorkItem (new WaitCallback (WriteFile));
					});
					s_running = true;
				}

			}
		}


		/// <summary>
		/// [线程函数] 写入日志文件
		/// </summary>
		/// <param name="stateInfo">State info.</param>
		private static void WriteFile (System.Object stateInfo = null)
		{
			lock (LOCK_OBJECT) {

				if (s_list.Count > 0) {
					if (!s_isAppend && !Directory.Exists (DIR_PATH))
						Directory.CreateDirectory (DIR_PATH);
					
					using (StreamWriter sw = new StreamWriter (FILE_PATH, s_isAppend)) {
						if (!s_isAppend) {
							sw.WriteLine (DateTime.Now.ToString ("[yyyy/MM/dd]"));
							s_isAppend = true;
						}
						foreach (LogData data in s_list) {
							sw.WriteLine ("");
							sw.WriteLine (data.ToString ());
						}
						s_list.Clear ();
					}
				}
				s_running = false;

			}
		}


		/// <summary>
		/// 销毁。游戏结束时
		/// </summary>
		public static void Destroy ()
		{
			WriteFile ();
		}

		//
	}
}

