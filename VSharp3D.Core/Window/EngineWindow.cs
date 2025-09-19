using Silk.NET.Windowing;
using Silk.NET.Input;

namespace VSharp3D.Core.Window;

public sealed class EngineWindow : IDisposable
{
    private readonly IWindow _window;
    private readonly IInputContext _input;
    private readonly IKeyboard _keyboard;

    public EngineWindow(int width, int height, string title)
    {
        var options = WindowOptions.Default;
        options.Size = new Silk.NET.Maths.Vector2D<int>(width, height);
        options.Title = title;

        _window = Silk.NET.Windowing.Window.Create(options);
        _window.Initialize();
        _input = _window.CreateInput();

        _keyboard = _input.Keyboards.FirstOrDefault();
    }
    
    public IWindow Window => _window;

    public bool IsKeyPressed(Silk.NET.Input.Key key) =>
        _keyboard?.IsKeyPressed(key) ?? false;

    public void Run(Action<double> onUpdate, Action<double> onRender)
    {
        _window.Update += onUpdate;
        _window.Render += onRender;
        _window.Run();
    }

    public void Close() => _window.Close();

    public void Dispose()
    {
        _input?.Dispose();
        _window?.Dispose();
    }
}