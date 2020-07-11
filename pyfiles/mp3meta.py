from tinytag import TinyTag
import sys
tag=TinyTag.get(sys.argv[1])

print(f"-album: {tag.album}")         # album as string
print(f"-album artist: {tag.albumartist}")   # album artist as string
print(f"-artist: {tag.artist}")        # artist name as string
print(f"-audio offset: {tag.audio_offset}")  # number of bytes before audio data begins
print(f"-bitrate: {tag.bitrate}")      # bitrate in kBits/s
print(f"-comment: {tag.comment}")       # file comment as string
print(f"-composer: {tag.composer}")      # composer as string 
print(f"-disc: {tag.disc}")          # disc number
print(f"-disc total: {tag.disc_total}")    # the total number of discs
print(f"-duration: {tag.duration}")      # duration of the song in seconds
print(f"-file size: {tag.filesize}")      # file size in bytes
print(f"-genre: {tag.genre}")         # genre as string
print(f"-sample rate: {tag.samplerate}")    # samples per second
print(f"-title: {tag.title}")         # title of the song
print(f"-track: {tag.track}")         # track number as string
print(f"-track total: {tag.track_total}")   # total number of tracks as string
print(f"-year: {tag.year}")          # year or data as string
