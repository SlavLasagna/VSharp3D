# VSharp3D

> A modular 3D engine written in **C#** with **Vulkan** bindings via [Silk.NET](https://github.com/dotnet/Silk.NET).

---

## Overview

VSharp3D is a 3D engine designed to be:

* **Reusable**: the engine core is a separate library (`VSharp3D.Core`).
* **Modular**: rendering, scene management, assets, and utilities are isolated.
* **Extensible**: future projects can reference the core without modification.
* **Practical**: examples show how to use the engine in small demos.

---

## Disclaimer ⚠️

This project is primarily for personal learning, but suggestions and issues are welcome!

---

## Features (planned / in progress)

* Vulkan-based rendering (via Silk.NET).
* Scene graph with transforms, cameras, and lights.
* Model loading (Wavefront `.obj` to start).
* Texture support (using STB).
* Simple animations (keyframes, later skeletal).
* Input handling (keyboard + mouse).
* Utilities: math helpers, logging, engine lifecycle.
* Examples project with runnable scenes: spinning cube, model viewer, textured object, etc.

---

## Project Structure

```
VSharp3D/
 ├─ VSharp3D.sln
 ├─ VSharp3D.Core/
 ├─ VSharp3D.Examples/
 ├─ README.md
 └─ .gitignore
```

---

## Tech Stack

* **Language:** C# 12 (.NET 8)
* **Graphics API:** Vulkan (via Silk.NET)
* **Math:** System.Numerics.Vectors
* **Assets:** AssimpNet (models), StbImageSharp (textures)
* **IDE:** JetBrains Rider

---

## Getting Started

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* A Vulkan-compatible GPU + drivers

### Build & Run

```bash
# Clone the repo
git clone https://github.com/your-username/VSharp3D.git
cd VSharp3D

# Build engine + examples
dotnet build

# Run the examples project
dotnet run --project VSharp3D.Examples
```

---

## Roadmap

* [ ] Basic Vulkan window + clear screen
* [ ] Render a triangle
* [ ] Spinning cube scene
* [ ] Camera controls (WASD + mouse look)
* [ ] Model + texture loading
* [ ] Directional lighting demo
* [ ] Simple animations

---

## License

MIT License – do whatever you want, just credit the project.