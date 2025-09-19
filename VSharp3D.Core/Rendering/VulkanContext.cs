using Silk.NET.Vulkan;
using Silk.NET.Core;
using Silk.NET.Core.Native;
using Silk.NET.Windowing;

namespace VSharp3D.Core.Rendering;

public sealed unsafe class VulkanContext : IDisposable
{
    private readonly Vk _vk;
    private readonly Instance _instance;

    public VulkanContext(IWindow window)
    {
        _vk = Vk.GetApi();

        var appInfo = new ApplicationInfo
        {
            SType = StructureType.ApplicationInfo,
            PApplicationName = (byte*)SilkMarshal.StringToPtr("VSharp3D"),
            ApplicationVersion = new Version32(1, 0, 0),
            PEngineName = (byte*)SilkMarshal.StringToPtr("VSharp3D"),
            EngineVersion = new Version32(1, 0, 0),
            ApiVersion = Vk.Version11
        };

        var createInfo = new InstanceCreateInfo
        {
            SType = StructureType.InstanceCreateInfo,
            PApplicationInfo = &appInfo
        };

        if (_vk.CreateInstance(&createInfo, null, out _instance) != Result.Success)
        {
            throw new Exception("Failed to create Vulkan instance!");
        }

        SilkMarshal.Free((nint)appInfo.PApplicationName);
        SilkMarshal.Free((nint)appInfo.PEngineName);
    }

    public void Dispose()
    {
        _vk.DestroyInstance(_instance, null);
    }
}