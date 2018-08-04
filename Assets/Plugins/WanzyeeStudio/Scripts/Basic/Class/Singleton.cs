
/*WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW*\     (   (     ) )
|/                                                      \|       )  )   _((_
||  (c) Wanzyee Studio  < wanzyeestudio.blogspot.com >  ||      ( (    |_ _ |=n
|\                                                      /|   _____))   | !  ] U
\.ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ All rights reserved./  (_(__(S)   |___*/

using UnityEngine;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace WanzyeeStudio{

	/// <summary>
	/// Agent to implement the singleton pattern for <c>UnityEngine.MonoBehaviour</c>.
	/// </summary>
	/// 
	/// <remarks>
	/// Not required to derive from any specific class, since C# inheritance is so precious.
	/// This finds the existed instance and check if multiple, creates one if not found.
	/// Set <c>Object.DontDestroyOnLoad()</c> if the instance is created by this.
	/// The others found, created manually or by scripts, should be maintained by the creator.
	/// It might need to check and destroy duplicated when <c>Awake()</c>.
	/// </remarks>
	/// 
	/// <remarks>
	/// In the general case, singleton should be implemented in the certain class for better enclosing.
	/// And sometimes, we do need to manually create one to edit in the Inspector.
	/// Make an agent for <c>UnityEngine.MonoBehaviour</c> to make it easier in the common case.
	/// This'll throw exception to remind the user to correct, when the code or scene setup wrong.
	/// And pop up a dialog if tries to create in edit mode, to avoid making scene dirty silently.
	/// </remarks>
	/// 
	/// <remarks>
	/// Since we can't protect the constructor without inheritance.
	/// For the purpose of better enclosed programming, here's usage limitations:
	/// 	1. Only allow the class of current singleton to access.
	/// 	2. Only allow one method to access to keep the code clean.
	/// 	3. Don't assign to a delegate, otherwise we can't keep the same accessor.
	/// 	4. Don't access from any constructor or assign to a field, that makes out-of-date.
	/// </remarks>
	/// 
	/// <example>
	/// Example to wrap to access, call this in a method or property:
	/// </example>
	/// 
	/// <code>
	/// using WanzyeeStudio;
	/// public class SomeComp : MonoBehaviour{
	/// 	public static SomeComp instance{
	/// 		get{ return Singleton<SomeComp>.instance; }
	/// 	}
	/// }
	/// </code>
	/// 
	/// <example>
	/// Example to maintain in <c>Awake()</c> to avoid duplicated, check to keep or destroy:
	/// </example>
	/// 
	/// <code>
	/// private void Awake(){
	/// 	if(this != instance) Destroy(this);
	/// 	else DontDestroyOnLoad(gameObject);
	/// }
	/// </code>
	/// 
	public static class Singleton<T> where T : MonoBehaviour, new(){

		#region Fields and Properties
		
		/// <summary>
		/// The method accesses and owns this.
		/// Only allow one method to access one singleton of type.
		/// </summary>
		private static MethodBase _accessor;

		/// <summary>
		/// The instance stored.
		/// </summary>
		private static T _instance;
		
		/// <summary>
		/// Get the singleton instance.
		/// </summary>
		/// <value>The instance.</value>
		/*
		 * Make this entry as property to avoid assigning to delegate.
		 */
		public static T instance{

			get{

				CheckAccessor(new StackFrame(1).GetMethod());
				
				if(null == _instance) FindInstance();

				return _instance;

			}

		}

		#endregion
		
		
		#region Methods
		
		/// <summary>
		/// Check if the type and the accessor valid.
		/// The component type T can't be generic, it's not creatable.
		/// And only allow "one member method" of the "class of current singleton" to access.
		/// </summary>
		/// <param name="accessor">Accessor.</param>
		private static void CheckAccessor(MethodBase accessor){
			
			if(accessor == _accessor) return; //it's always fine from the same method

			if(typeof(T).IsGenericType){
				var _f = "Singleton doesn't support generic type {0}.";
				throw new NotSupportedException(string.Format(_f, typeof(T)));
			}

			if(!accessor.DeclaringType.IsAssignableFrom(typeof(T))){
				var _f = "Singleton<{0}> can be called by {0} only, {1} isn't allowed.";
				throw new MethodAccessException(string.Format(_f, typeof(T), accessor.DeclaringType));
			}
			
			if(accessor.IsConstructor){
				var _f = "Singleton<{0}> can't be called by constructor nor assigned to a field.";
				throw new MethodAccessException(string.Format(_f, typeof(T)));
			}
			
			if(null != _accessor){
				var _f = "Singleton<{0}> has been called by [{1}], [{2}] isn't allowed.";
				throw new MethodAccessException(string.Format(_f, typeof(T), _accessor, accessor));
			}
			
			_accessor = accessor; //store the first valid accessor
			
		}

		/// <summary>
		/// Find the instance in current scene, create one if none.
		/// </summary>
		private static void FindInstance(){

			var _c = Resources.FindObjectsOfTypeAll<T>();

			#if UNITY_EDITOR

			_c = _c.AsEnumerable(
				).Where(_v => !UnityEditor.EditorUtility.IsPersistent(_v)
				).Where(_v => 0 == (_v.hideFlags & HideFlags.DontSaveInBuild)
			).ToArray();

			#endif
			
			foreach(var _v in _c) Debug.LogFormat(_v, "Singleton<{0}> found: {1}.", typeof(T), _v.name);

			if(0 == _c.Length){
				CreateInstance();
			}else if(1 == _c.Length){
				_instance = _c[0];
			}else{
				var _f = "Singleton<{0}> is ambiguous between multiple existing instances.";
				throw new OperationCanceledException(string.Format(_f, typeof(T)));
			}

		}

		/// <summary>
		/// Create the new instance with <c>Object.DontDestroyOnLoad()</c>.
		/// Show dialog to ask user to create only in edit mode.
		/// </summary>
		/*
		 * Assign instance in methods instead of returning, to wrap creating between switching active.
		 * To avoid the instance property being called when Awake() before assigning.
		 */
		private static void CreateInstance(){

			#if UNITY_EDITOR
			var _t = "Create Singleton";
			var _m = string.Format("Instance of {0} is not found! Create now?", typeof(T));
			if(!Application.isPlaying && !UnityEditor.EditorUtility.DisplayDialog(_t, _m, "Create", "Ignore")) return;
			#endif

			var _g = new GameObject(string.Format("[Singleton {0}]", typeof(T).Name));
			if(Application.isPlaying) Object.DontDestroyOnLoad(_g);

			#if UNITY_EDITOR
			if(!Application.isPlaying) UnityEditor.Undo.RegisterCreatedObjectUndo(_g, "Create Singleton");
			#endif

			_g.SetActive(false);
			Debug.LogFormat(_g, "Singleton<{0}> created.", typeof(T));

			_instance = _g.AddComponent<T>();
			_g.SetActive(true);

		}
		
		#endregion

	}

}
