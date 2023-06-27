# VRC-SKY
VRChat Remote Image Loading Skyboxes

[![Demo](https://img.youtube.com/vi/1dXKexeS-uA/maxresdefault.jpg)](https://youtu.be/1dXKexeS-uA)

I included:
- [Skybox.cs] which is a specialized bulk image downloader for Skyboxes
- [Skybox.mat] which is the material used by Skybox.cs, don't set this as your scene skybox
- [SkyboxSwapper.cs] which is simple swapper to cycles the skyboxes
- [SkyboxSwapper.prefab] which is a prefab already setup with all the Skyboxes

I plan on making a more useful Skybox Manager when Udon UI releases  
For now you can test the Skybox Swapper in my [Test World](https://vrchat.com/home/world/wrld_e694ad3f-2ada-46c7-9100-b8c1f09e4dbd)

All [Skyboxes] have been:
- Extracted using [CubemapExtractor.cs]
    - VRChat's Remote Image Loader doesn't allow setting image types as Cubemap
    - They would also be much lower resolution (2048x1024 for 6-Sides)
    - Using `Editor/CubemapExtractor.cs` (Assets -> Extract Cubemap Images)
- Resized using [ImageMagick]
    - VRChat's Remote Image Loader allows a maximum of `2048x2048`
    - `$ magick mogrify -resize 2048x2048 -quality 100 *.png`
- Tinified using [TinyPNG]
    - I recommend using [TinyGUI] or [Tinifier]
- Compressed using [OxiPNG]
    - `$ oxipng -o 6 -s all **/*.png`

Additional Skyboxes are welcome, Please process them and submit a pull request or issue

[Skyboxes]: Skyboxes
[Skybox.cs]: Scripts/Skybox.cs
[Skybox.mat]: Materials/Skybox.mat
[SkyboxSwapper.cs]: Scripts/SkyboxSwapper.cs
[SkyboxSwapper.prefab]: Prefabs/SkyboxSwapper.prefab
[CubemapExtractor.cs]: Editor/CubemapExtractor.cs
[ImageMagick]: https://imagemagick.org
[TinyPNG]: https://tinypng.com
[TinyGUI]: https://github.com/chenjing1294/TinyGUI
[Tinifier]: https://github.com/tarampampam/tinifier
[OxiPNG]: https://github.com/shssoichiro/oxipng