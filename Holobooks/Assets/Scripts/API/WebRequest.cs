using System;
using System.Collections;
using UnityEngine;


namespace Holobook.WebRequest
{
	public class WebRequest : IEnumerator
	{
		private const float m_DefaultTimeOut = 30f;

		public enum State
		{
			NOT_STARTED = 0,
			RUNNING,
			ERROR,
			TIMEOUT,
			NO_RESULT,
			DONE,
		}

		public WWW www { get; private set; }
		public float timeOut { get; private set; }
		public float elapsedDuration { get; private set; }

		public State GetState()
		{
			if (elapsedDuration == 0.0f)
			{
				return State.NOT_STARTED;
			}
			else
			{
				if (HasTimedOut())
				{
					return State.TIMEOUT;
				}
				else
				{
					if (!www.isDone)
					{
						return State.RUNNING;
					}
					else
					{
						if (HasError())
						{
							return State.ERROR;
						}
						else
						{
							if (www.bytes.Length == 0)
							{
								return State.NO_RESULT;
							}

							return State.DONE;
						}
					}
				}
			}
		}

		private float m_InitialTime;

		public bool HasError()
		{
			return !string.IsNullOrEmpty(www.error);
		}

		public bool HasTimedOut()
		{
			return elapsedDuration > timeOut;
		}

		public WebRequest(string url) : this(new WWW(url)) { }
		public WebRequest(WWW www) : this(www, m_DefaultTimeOut) { }
		public WebRequest(string url, float timeOut) : this(new WWW(url), timeOut) { }
		public WebRequest(WWW www, float timeOut)
		{
			Reset();

			this.www = www;
			this.timeOut = timeOut;

			m_InitialTime = Time.realtimeSinceStartup;
		}

		public static void Request(string url, float timeOut, MonoBehaviour monoBehaviour, Action<WebRequest> onDone) { Request(url, timeOut, 0.0f, monoBehaviour, onDone); }
		public static void Request(string url, float timeOut, float delay, MonoBehaviour monoBehaviour, Action<WebRequest> onDone)
		{
			monoBehaviour.StartCoroutine(RequestDelayCoroutine(url, timeOut, delay, monoBehaviour, onDone));
		}

		private static IEnumerator RequestDelayCoroutine(string url, float timeOut, float delay, MonoBehaviour monoBehaviour, Action<WebRequest> onDone)
		{
			if (delay > 0.0f)
			{
				yield return new WaitForSeconds(delay);
			}

			Request(new WWW(url), timeOut, monoBehaviour, onDone);
		}

		public static void Request(WWW www, float timeOut, MonoBehaviour monoBehaviour, Action<WebRequest> onDone)
		{
			monoBehaviour.StartCoroutine(RequestCoroutine(www, timeOut, onDone));
		}

		private static IEnumerator RequestCoroutine(WWW www, float timeOut, Action<WebRequest> onDone)
		{
			float initialTime = Time.realtimeSinceStartup;

			WebRequest webRequest = new WebRequest(www, timeOut);
			yield return webRequest.www;

			webRequest.elapsedDuration = (Time.realtimeSinceStartup - initialTime);

			if (onDone != null)
			{
				onDone(webRequest);
			}
		}
			
		public bool MoveNext()
		{
			bool isDoneOrTimeOut = www.isDone || HasTimedOut();

			if (isDoneOrTimeOut)
			{
				elapsedDuration = Time.realtimeSinceStartup - m_InitialTime;
			}

			return !isDoneOrTimeOut;
		}

		public void Reset()
		{
			www = null;
			timeOut = 0f;
			elapsedDuration = 0f;
		}

		public object Current 
		{
			get
			{
				return null;
			}
		}
			
	}
}