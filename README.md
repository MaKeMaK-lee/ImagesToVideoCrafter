# Описание
Программа предназначена для создания видео из изображений. Написана на языке C# и использует FFMediaToolkit (который использует ffmpeg). 


# Использование
## Установка
Для использования программы вам понадобятся:
- .exe файл консольного приложения;
- отдельно скачанные ffmpeg shared .dll файлы: avcodec, avformat, avutil, swresample, swscale версии 6.x. Скачать можно здесь: https://github.com/BtbN/FFmpeg-Builds/releases. Вам нужны только файлы .dll из каталога .\bin (не .\lib) shared ZIP-пакета. Путь к этим файлам определяется параметром FFmpegBinaresDirectory (см. ниже);   
- (рекомендуется) файл .bat для запуска.
- (если вам нужны кадры в видео) ваши кадры для видео в формате .jpg / .png (некоторые другие могут сработать, но не .webp).

## Параметры
Программа имеет ряд параметров, все они имеют значения по умолчанию и могут быть проигнорированы.  
На вход принимаются следующие аргументы:  

##### Основные
|Параметр|Описание|По умолчанию|Допустимые|
|---|---|---|---|
|FFmpegBinaresDirectory|Директория, в которой находятся ffmpeg shared .dll файлы: avcodec, avformat, avutil, swresample, swscale.|`C:\ffmpeg\x86_64`|`[том]:\[имя]\[имя]`|
|OutputDirectory|Директория, куда будет помещён создаваемый видео файл.|`C:\ImagesToVideoCrafter\Out`|`[том]:\[имя]\[имя]`|
|ReverseInputFilesOrder|Определяет, следует ли добавить к имени файла информацию о параметрах видео.|false|true / false|
|AddVideoInfoToFilename|Определяет, следует ли инверсировать порядок кадров.|false|true / false|
|InputDirectory|Директория, откуда будут взяты кадры для видео.|`C:\ImagesToVideoCrafter\In`|`[том]:\[имя]\[имя]`|
##### Видео
|Параметр|Описание|По умолчанию|Допустимые|
|---|---|---|---|
|FrameMilliseconds|Длительность одного кадра в миллисекундах.|41.666666666666666666666666666667|(c# double parse from string)|
|Framerate|Частота кадров. Не используется, если не установить UseFramerate true.|24||
|UseFramerate| Флаг, определяющий, нужно ли игнорировать значение FrameMilliseconds и использовать только Framerate.|false|true / false|
|CRF|Constant Rate Factor. Имеет значение для только для кодеков x264 и x265.|17|0–51 (0 - без потерь качества, большой размер; 51 - худшее качество, малый размер)|
|Width|Ширина видео.|1920||
|Height|Ширина видео.|1080||
|EncoderPresetSpeed|Режим кодирования. Для кодеков x264 и x265. Влияет на скорость создания видео, немного на его размер и самую малость на качество.|4 (быстро)|0-8 (0 - быстро, минимальное сжатие; 8 - медленно, сильное сжатие)|
|Codec|Кодек. Рекоммендуется H264. Поддерживаются, но не тестировались: H265, MPEG4.|H264|H264 / H265 / MPEG4|
 
## Запуск
### Приготовления
Убедитесь, что вас устраивает сортировка кадров по имени файла.  
Убедитесь, что все файлы в директории кадров соответствуют требуемому формату изображений. 
### Собственно запуск
Рекомендуемый вариант: запускаете .exe через .bat, не забыв указать верный путь к .dll файлам.  
Пример .bat файла:

    ImagesToVideoCrafter.exe -FFmpegBinaresDirectory C:\Users\Username\Desktop\ffmpeg\x86_64 -OutputDirectory C:\Users\Username\Desktop -ReverseInputFilesOrder false -InputDirectory C:\Users\Username\Desktop\frames -FrameMilliseconds 41.666666666666666666666666666667 -Framerate 24 -UseFramerate false -CRF 17 -Width 1920 -Height 1080 -EncoderPresetSpeed 1 -Codec H264 -AddVideoInfoToFilename true
    pause
