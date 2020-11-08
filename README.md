# BasicEncoder
The purpose of this program is encoding files from one to another format, keeping file/directory structure unchanged. The program uses the ffmpeg binary to do the actual transcoding.
## Basic usage
By default the command will assume ffmpeg is in the PATH variable or in the same directory.
```
./BasicEncoder.exe -i <source directory> -o <destination directory> [-if <input format>] [-of <output format>] [-opt <ffmpeg options>] [-ffmpeg <path to ffmpeg binary>]
```
## Example usage
In this example we will use a directory structure as follows, we want to convert the files to opus for easier playback on mobile devices and/or to save space.
```
- Source
 |- Folder1
 | |- File.wav
 |- Folder2
 | |- File.wav
- Destination
```
We can use the command `./BasicEncoder.exe -i ./Source -o ./Destination -if wav -of opus -opt "-b:a 128k"`. This command will now transcode all the files into 128kbps opus files.
This will result in following new structure.
```
- Source
 |- Folder1
 | |- File.wav
 |- Folder2
 | |- File.wav
- Destination
 |- Folder1
 | |- File.opus
 |- Folder2
 | |- File.opus
```


## Dependencies
- ffmpeg
