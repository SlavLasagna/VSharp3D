using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using Silk.NET.Core;
using Silk.NET.Core.Native;
using Silk.NET.Windowing;

namespace VSharp3D.Core.Rendering;

public sealed unsafe class VkContext : IDisposable
{
    private readonly Vk _vk;
    private Instance _instance;
    private Device _device;
    private PhysicalDevice _physicalDevice;
    private Queue _graphicsQueue;
    private IWindow _window;
    private SurfaceKHR _surface;
    private KhrSurface _khrSurface;
    
    // Queue family indices
    private struct QueueFamilyIndices
    {
        public uint? GraphicsFamily;
        public uint? PresentFamily;
                
        public bool IsComplete => GraphicsFamily.HasValue &&  PresentFamily.HasValue;
    }

    public VkContext(IWindow window)
    {
        _vk = Vk.GetApi();
        _window = window;
        _khrSurface = new KhrSurface(_vk.Context);

        CreateInstance();
        //CreateSurface();
        SelectPhysicalDevice();
        CreateLogicalDevice();
    }

    private void CreateInstance()
    {
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

    /*private void CreateSurface()
    {
        if (_surface.Handle == 0)
        {
            throw new Exception("Failed to create surface!");
        }
    }*/

    private void SelectPhysicalDevice()
    {
        uint deviceCount = 0;
        _vk.EnumeratePhysicalDevices(_instance, ref deviceCount, null);

        if (deviceCount == 0)
        {
            throw new Exception("Failed to enumerate physical devices!");
        }
        
        var devices = new PhysicalDevice[deviceCount];
        fixed (PhysicalDevice* devicesPtr = devices)
        {
            _vk.EnumeratePhysicalDevices(_instance, ref deviceCount, devicesPtr);
        }

        foreach (var device in devices)
        {
            if (IsDeviceSuitable(device))
            {
                _physicalDevice = device;
                break;
            }
        }

        if (_physicalDevice.Handle == 0)
        {
            throw new Exception("Failed to find a suitable GPU!");
        }
    }
    
    private QueueFamilyIndices FindQueueFamilies(PhysicalDevice device)
        {
            QueueFamilyIndices indices = default;
            
            uint queueFamilyCount;
            _vk.GetPhysicalDeviceQueueFamilyProperties(device, &queueFamilyCount, null);

            var queueFamilies = new QueueFamilyProperties[queueFamilyCount];
            _vk.GetPhysicalDeviceQueueFamilyProperties(device, &queueFamilyCount, queueFamilies);
            
            uint i = 0;
            foreach (var queueFamily in queueFamilies)
            {
                Bool32 presentSupport = false;
                
                
                if ((queueFamily.QueueFlags & QueueFlags.GraphicsBit) != 0)
                {
                    indices.GraphicsFamily = i;
                }

                if (indices.IsComplete) break;

                i++;
            }
            
            return indices;
        }

    private bool IsDeviceSuitable(PhysicalDevice device)
    {
        QueueFamilyIndices queueFamily = FindQueueFamilies(device);
        //PhysicalDeviceProperties deviceProperties;
        //PhysicalDeviceFeatures deviceFeatures;
        
        //_vk.GetPhysicalDeviceProperties(device, &deviceProperties);
        //_vk.GetPhysicalDeviceFeatures(device, &deviceFeatures);
        
        return queueFamily.IsComplete;
    }

    private void CreateLogicalDevice()
    {
        QueueFamilyIndices indices = FindQueueFamilies(_physicalDevice);

        var queueCreateInfo = new DeviceQueueCreateInfo
        {
            SType = StructureType.DeviceQueueCreateInfo,
            QueueFamilyIndex = indices.GraphicsFamily.Value,
            QueueCount = 1,
        };

        var queuePriority = 1.0f;
        queueCreateInfo.PQueuePriorities = &queuePriority;
        
        var deviceFeatures = new PhysicalDeviceFeatures{};
        
        var createInfo = new DeviceCreateInfo
        {
            SType = StructureType.DeviceCreateInfo,
            PQueueCreateInfos = &queueCreateInfo,
            QueueCreateInfoCount = 1,
            PEnabledFeatures = &deviceFeatures,
            EnabledExtensionCount = 0,
        };

        if (_vk.CreateDevice(_physicalDevice, &createInfo, null, out _device) != Result.Success)
        {
            throw new Exception("Failed to create logical device!");
        }
        
        _vk.GetDeviceQueue(_device, indices.GraphicsFamily.Value, 0, out _graphicsQueue);
    }

    public void Dispose()
    {
        _vk.DestroyInstance(_instance, null);
        _vk.DestroyDevice(_device, null);
    }
}