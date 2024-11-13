# Описание
Программа предназначена для создания видео из изображений. Написана на языке C# и использует FFMediaToolkit (который использует ffmpeg). 


# Использование
## Установка
Для использования программы вам понадобятся:
- .exe файл приложения - консоль или gui;
- отдельно скачанные ffmpeg shared .dll файлы: avcodec, avformat, avutil, swresample, swscale версии 6.x. Скачать можно здесь: https://github.com/BtbN/FFmpeg-Builds/releases. Вам нужны только файлы .dll из каталога .\bin (не .\lib) shared ZIP-пакета. Путь к этим файлам определяется параметром FFmpegBinaresDirectory (см. ниже);   
- (рекомендуется) файл .bat для запуска.
- собственно ваши кадры для видео в формате .jpg / .png (некоторые другие могут сработать, но не .webp).

Ниже описано использование на примере консольного приложения, с gui в целом взаимодействие аналогичное.

## Параметры
Программа имеет ряд параметров, все они имеют значения по умолчанию и могут быть проигнорированы.  
На вход принимаются следующие аргументы:  

##### Основные
|Параметр|Описание|По умолчанию|Допустимые|
|---|---|---|---|
|FFmpegBinaresDirectory|Директория, в которой находятся ffmpeg shared .dll файлы: avcodec, avformat, avutil, swresample, swscale.|ffmpeg\x86_64|*|
|OutputVideoName|Имя создаваемого видео файла **без расширения файла**.|Video||
|OutputDirectory|Директория, куда будет помещён создаваемый видео файл.|VideoResults|*|
|ReverseInputFilesOrder|Определяет, следует ли добавить к имени файла информацию о параметрах видео.|false|true / false|
|AddVideoInfoToFilename|Определяет, следует ли инверсировать порядок кадров.|false|true / false|
|InputDirectory|Директория, откуда будут взяты кадры для видео.|FramesSource|*|
|DebugMode|Определяет, использовать ли режим отладки (выводить ли отладочные сообщения и т.п.).|false|true / false|

**Все пути можно указывать в форматах: C:\example\example , C:\\\example\\\example , "C:\example with spaces\example" , subfolder , subfolder\example , subfolder\\\example , "subfolder with spaces" , ну вы поняли...*
##### Видео
|Параметр|Описание|По умолчанию|Допустимые|
|---|---|---|---|
|FrameMilliseconds|Длительность одного кадра в миллисекундах.|41.666666666666666666666666666667|(c# double parse from string)|
|Framerate|Частота кадров. Не используется, если не установить UseFramerate true.|24||
|UseFramerate| Определяет, нужно ли игнорировать значение FrameMilliseconds и использовать только Framerate.|false|true / false|
|CRF|Constant Rate Factor. Имеет значение для только для кодеков x264 и x265.|17|0–51 (0 - без потерь качества, большой размер; 51 - худшее качество, малый размер)|
|Width|Ширина видео.|1920||
|Height|Ширина видео.|1080||
|EncoderPresetSpeed|Режим кодирования. Для кодеков x264 и x265. Влияет на скорость создания видео, немного на его размер и самую малость на качество.|4 (быстро)|0-8 (0 - быстро, минимальное сжатие; 8 - медленно, сильное сжатие)|
|Codec|Кодек. Рекоммендуется H264. Поддерживаются, но не тестировались: H265, MPEG4.|H264|H264 / H265 / MPEG4|

##### Надстройки
|Параметр|Описание|По умолчанию|Допустимые|
|---|---|---|---|
|LightnessThreshold|Минимальное значение средней светлоты в кадре для его валидации|0.2|0 - 1|

## Запуск
### Приготовления
Убедитесь, что вас устраивает сортировка кадров по имени файла.  
Убедитесь, что все файлы в директории кадров соответствуют требуемому формату изображений.  
По желанию скорректируйте параметры запуска в .bat файле (например, разрешение видео).
### Собственно запуск
Рекомендуемый вариант: запускаете .exe через .bat, не забыв указать верный путь к .dll файлам.  
Пример .bat файла:

    ImagesToVideoCrafter.exe -FFmpegBinaresDirectory ffmpeg\x86_64 -OutputDirectory C:\Users\%username%\Desktop -ReverseInputFilesOrder false -InputDirectory Frames -FrameMilliseconds 41.666666666666666666666666666667 -Framerate 24 -UseFramerate false -CRF 17 -Width 1920 -Height 1080 -EncoderPresetSpeed 4 -Codec H264 -AddVideoInfoToFilename false
    pause
     
