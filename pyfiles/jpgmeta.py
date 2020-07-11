from PIL import Image
from PIL.ExifTags import TAGS, GPSTAGS
from GPSPhoto import gpsphoto
import sys

# path to the image or video
imagename = sys.argv[1]

def get_exif(filename):
    image = Image.open(filename)
    image.verify()
    return image._getexif()
    
def get_labeled_exif(exif):
    labeled = {}
    for (key, val) in exif.items():        
        if isinstance(val,bytes):
            val = val.decode("utf-8")
        labeled[TAGS.get(key)] = val

    return labeled

def get_geotagging(exif):
    if not exif:
        print("No EXIF metadata found")
        return

    geotagging = {}
    for (idx, tag) in TAGS.items():
        if tag == 'GPSInfo':
            if idx not in exif:
                print("No EXIF geotagging found")
                return

            for (key, val) in GPSTAGS.items():
                if key in exif[idx]:
                    # if isinstance(exif[idx][key],bytes):
                    #    x = exif[idx][key].decode("utf-8")
                    #    geotagging[val] = x
                    #else:
                    geotagging[val] = exif[idx][key]

    return geotagging

def get_decimal_from_dms(dms, ref):

    degrees = dms[0][0] / dms[0][1]
    minutes = dms[1][0] / dms[1][1] / 60.0
    seconds = dms[2][0] / dms[2][1] / 3600.0

    if ref in ['S', 'W']:
        degrees = -degrees
        minutes = -minutes
        seconds = -seconds

    return round(degrees + minutes + seconds, 5)

def get_coordinates(geotags):
    lat = get_decimal_from_dms(geotags['GPSLatitude'], geotags['GPSLatitudeRef'])

    lon = get_decimal_from_dms(geotags['GPSLongitude'], geotags['GPSLongitudeRef'])

    return (lat,lon)

try:
    exif = get_exif(imagename)
    labeled = get_labeled_exif(exif)
    for key in labeled.keys():
        print(f"-{key}:{labeled[key]}")
    try:
        geotags = gpsphoto.getGPSData(imagename)
        f = open("gpstemp.txt", "a")
        for key in geotags.keys():
            print(f"-{key}:{geotags[key]}")    

        f.write(f"{imagename},{geotags['Latitude']},{geotags['Longitude']}\n")

        f.close()
    except Exception as e:
        pass
except Exception as e:
    print("An Error Occurred! Please try again!")