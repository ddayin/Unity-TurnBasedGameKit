
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using System;
using System.Linq;

namespace WanzyeeStudio{
	
	/// <summary>
	/// Base class to implement the singleton pattern for <c>UnityEngine.MonoBehaviour</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// This works with <c>Singleton</c> to minimize coding, if you don't need custom inheritance.
	/// Derive from this to make a <c>UnityEngine.MonoBehaviour</c> singleton.
	/// Only allow the class of current singleton to derive.
	/// This applies the singleton's root <c>Object.DontDestroyOnLoad()</c>
	/// And handle duplicates when <c>Awake()</c>.
	/// </remarks>
	/// 
	/// <example>
	/// Example to implement singleton, just derive from this:
	/// </example>
	/// 
	/// <code>
	/// using WanzyeeStudio;
	/// public class SomeComp : BaseSingleton<SomeComp>{}
	/// </code>
	/// 
	/// <example>
	/// Example to access from another class:
	/// </example>
	/// 
	/// <code>
	/// public class AnotherClass{
	/// 	private void DoSomething(){
	/// 		Debug.Log(SomeComp.instance);
	/// 	}
	/// }
	/// </code>
	/// 
	public abstract class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>, new(){
		
		/// <summary>
		/// Flag if to destroy excess instance automatically when it <c>Awake()</c>.
		/// </summary>
		protected static bool autoDestroy;

		/// <summary>
		/// Get the singleton instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T instance{
			get{ return Singleton<T>.instance; }
		}
		
		/// <summary>
		/// Static constructor to check inheritance, since we can't constraint subclasses.
		/// </summary>
		static BaseSingleton(){

			var _t = AppDomain.CurrentDomain.GetAssemblies(
				).SelectMany(_v => _v.GetTypes()
				).Where(_v => _v.IsSubclassOf(typeof(BaseSingleton<T>)) && (_v != typeof(T))
			);

			if(!_t.Any()) return;
			
			var _f = "BaseSingleton<{0}> can be inherited by {0} only, list below not allowed:\n{1}";
			var _l = string.Join("\n", _t.Select(_v => _v.ToString()).ToArray());

			throw new InvalidProgramException(string.Format(_f, typeof(T), _l));

		}

		/// <summary>
		/// Awake, check and handle duplicated instances.
		/// </summary>
		/// 
		/// <remarks>
		/// Apply <c>Object.DontDestroyOnLoad()</c> to the root if this's the singleton.
		/// Otherwise check if <c>autoDestroy</c> to suicide or log error.
		/// Call <c>base.Awake()</c> if you implement <c>Awake()</c> in the subclass.
		/// </remarks>
		/// 
		/*
		 * Not to place in ctor, to ensure only work in play mode.
		 */
		protected virtual void Awake(){

			if(this == instance) DontDestroyOnLoad(transform.root.gameObject);

			else if(autoDestroy) Destroy(this);

			else Debug.LogErrorFormat(this, "Singleton<{0}> has excess instance {1}.", typeof(T), name);

		}

	}

}
