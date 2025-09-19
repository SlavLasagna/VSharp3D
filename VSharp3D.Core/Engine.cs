using VSharp3D.Core.Window;
using VSharp3D.Core.Rendering;

namespace VSharp3D.Core;

public sealed class Engine : IDisposable
{
    private EngineWindow _engineWindow;
    private Renderer _renderer;

    public Engine()
    {
        _engineWindow = new EngineWindow(800, 600, "VSharp3D");
        _renderer = new Renderer(_engineWindow);
    }

    public void Run()
    {
        _engineWindow.Run(OnUpdateFrame, OnRenderFrame);
    }

    private void OnUpdateFrame(double deltaTime)
    {
        if (_engineWindow.IsKeyPressed(Silk.NET.Input.Key.Escape))
        {
            _engineWindow.Close();
        }
    }

    private void OnRenderFrame(double deltaTime)
    {
        _renderer.Render();
    }

    public void Dispose()
    {
        _renderer.Dispose();
        _engineWindow.Dispose();
    }
}