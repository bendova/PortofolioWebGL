using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WebGLMovieTexture 
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern int WebGLMovieTextureCreate (string url);

    [DllImport("__Internal")]
	private static extern int WebGLMovieTextureDispose (int video);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTextureUpdate (int video, int texture);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTexturePlay (int video);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTexturePause (int video);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTextureSeek (int video, float time);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTextureLoop (int video, bool loop);

	[DllImport("__Internal")]
	private static extern int WebGLMovieTextureWidth (int video);

	[DllImport("__Internal")]
	private static extern int WebGLMovieTextureHeight (int video);

	[DllImport("__Internal")]
	private static extern bool WebGLMovieTextureIsReady (int video);

	[DllImport("__Internal")]
	private static extern float WebGLMovieTextureTime (int video);

	[DllImport("__Internal")]
	private static extern float WebGLMovieTextureDuration (int video);

    [DllImport("__Internal")]
	private static extern bool WebGLMovieTextureHasEnded (int video);
#else
    private static int WebGLMovieTextureCreate (string url)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
    private static int WebGLMovieTextureDispose(int video)
    {
        throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
    }
    private static void WebGLMovieTextureUpdate (int video, int texture)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTexturePlay (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTexturePause (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTextureSeek (int video, float time)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTextureLoop (int video, bool loop)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static int WebGLMovieTextureWidth (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static int WebGLMovieTextureHeight (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static bool WebGLMovieTextureIsReady (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static float WebGLMovieTextureTime (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static float WebGLMovieTextureDuration (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}

    private static bool WebGLMovieTextureHasEnded(int video)
    {
        throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
    }
#endif
    private Texture2D m_Texture;
	private int m_Instance; 
	private bool m_Loop;

	public WebGLMovieTexture (string url)
	{
        m_Instance = WebGLMovieTextureCreate(url);

        m_Texture = new Texture2D(0, 0, TextureFormat.RGBA32, false);
		m_Texture.wrapMode = TextureWrapMode.Clamp;
    }

	public void Update()
	{
		var width = WebGLMovieTextureWidth(m_Instance);
		var height = WebGLMovieTextureHeight(m_Instance);
		if (width != m_Texture.width || height != m_Texture.height)
		{
            m_Texture.Resize(width, height, TextureFormat.RGBA32, false);
            m_Texture.Apply();
		}
        WebGLMovieTextureUpdate(m_Instance, m_Texture.GetNativeTexturePtr().ToInt32());
    }

    public void Dispose()
    {
        WebGLMovieTextureDispose(m_Instance);
    }

    public void Play()
	{
		WebGLMovieTexturePlay(m_Instance);
	}

	public void Pause()
	{
		WebGLMovieTexturePause(m_Instance);
	}

	public void Seek(float t)
	{
        WebGLMovieTextureSeek(m_Instance, t);
	}

	public bool loop
	{
		get 
		{
			return m_Loop;
		}
		set
		{
			if (value != m_Loop)
			{
				m_Loop = value;
				WebGLMovieTextureLoop(m_Instance, m_Loop);
			}
		}
	}

	public bool isReady
	{
		get 
		{
            return WebGLMovieTextureIsReady(m_Instance);
		}
	}

	public float time
	{
		get
		{
            return PluginMathUtils.GetSafeFloat(WebGLMovieTextureTime(m_Instance));
		}
	}

	public float duration
	{
		get
		{
            return PluginMathUtils.GetSafeFloat(WebGLMovieTextureDuration(m_Instance));
		}
	}

    public bool hasEnded
    {
        get
        {
            return WebGLMovieTextureHasEnded(m_Instance);
        }
    }

    public Texture2D GetVideoTexture()
    {
        return m_Texture;
    }
}
