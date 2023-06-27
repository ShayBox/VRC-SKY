# VRC-SKY
VRChat Remote Image Loading Skyboxes

- Extracted using [CubemapExtractor]
    - VRChat's Remote Image Loader doesn't allow setting image types as Cubemap
    - They would also be much lower resolution (2048x1024 for 6-Sides)
    - Using `Unity/Editor/CubemapExtractor.cs` (Assets -> Extract Cubemap Images)
- Resized using [ImageMagick]
    - VRChat's Remote Image Loader allows a maximum of `2048x2048`
    - `$ magick mogrify -resize 2048x2048 -quality 100 *.png`
- Tinified using [TinyPNG]
    - I recommend using [TinyGUI] or [Tinifier]
- Compressed using [OxiPNG]
    - `$ oxipng -o 6 -s all **/*.png`

[CubemapExtractor]: https://example.tld
[ImageMagick]: https://imagemagick.org
[TinyPNG]: https://tinypng.com
[TinyGUI]: https://github.com/chenjing1294/TinyGUI
[Tinifier]: https://github.com/tarampampam/tinifier
[OxiPNG]: https://github.com/shssoichiro/oxipng