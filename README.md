# EZContrast

Easy way to measure contrast and WCAG compliance of it on Windows

## How to run

Download the executable from the Releases on Github and run it. It requires no further installation and is a portable executable.

Please note that there might be errors regarding the untrusted nature of an unsigned executable - As a small developer I cannot afford a signing certificate, thus the software is not digitally signed.

## How to use

Pick 2 colors using the corresponding buttons on the left, and hover the cursor over the color. The contrast ratio of two selected colors and the compliance of that contrast ratio will be displayed. You can increase the delay with the slider to make it easier to pick the color

## Problems with Windows

Because Windows has some issues with scaling the cursor positions on HI-DPI screens, you need to set the correct DPI compatibility mode.

1. Right-click on the downloaded executable
2. Select "Properties"
3. Select the "Compatibility" tab
4. Select "Change high DPI settings"
5. Enable "Override high DPI scaling behavior"
6. Set "Scaling performed by" to "Application"

## Compiling

0. Download Visual Studio community edition. This app was compiled with VS 2019 but should be compatible with newer versions
1. Open ezcontrast.sln
2. Select "Release" and architecture of the target platform
3. Select "Compilation" on top and select "Compile Solution"
4. The resulting executable will be placed in `./ezcontrast/bin/Release/ezcontrast.exe`

## Liability

The developer (jakubiakdev) and contributors to this project are not liable for miscalculations of the contrast, and thus not liable for any wrong accessibility report that used this program (EZContrast)

This software is being provided "as is", without any express or implied warranty. These machine-generated evaluations of contrast are not conclusive. In particular, The developer and contributors make no representation or warranty of any kind concerning the reliability, quality, or merchantability of this software or its fitness for any particular purpose. Additionally, The developer and contributors do not guarantee that the use of this service will ensure the accessibility of your content or that your content will comply with any specific web accessibility standard or law. You understand and agree that you download and/or use the EZContrast service at your own discretion and risk and that you will be solely responsible for any damages to your computer system or loss of data that results from the download or use of EZContrast.

The use of EZContrast or EZContrast results/data for any purpose related to legal action is not allowed without written permission.

Note: This notice was inspired and partially copied from [WAVE's TOS](https://wave.webaim.org/terms)
