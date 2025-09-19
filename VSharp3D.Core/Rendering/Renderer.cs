using VSharp3D.Core.Window;

namespace VSharp3D.Core.Rendering;

public sealed class Renderer : IDisposable
{
    private VkContext _vkContext;

    public Renderer(EngineWindow engineWindow)
    {
        _vkContext = new VkContext(engineWindow.Window);
    }

    public void Render()
    {
        // For now: nothing drawn, just keeps the window alive.
    }

    public void Dispose()
    {
        _vkContext.Dispose();
    }
}