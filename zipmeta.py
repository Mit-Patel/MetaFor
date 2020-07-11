import zipfile,sys

zipfile = sys.argv[1]

zf = zipfile.ZipFile(zipfile, 'r')
print (zf.namelist())

def print_info(archive_name):
    zf = zipfile.ZipFile(archive_name)
    for info in zf.infolist():
        print (info.filename)
        print('-Comment: ', info.comment)
        print('-Modified: ', datetime.datetime(*info.date_time))
        print('-System: ', info.create_system, '(0 = Windows, 3 = Unix)')
        print('-ZIP version: ', info.create_version)
        print('-Compressed: ', info.compress_size, 'bytes')
        print('-Uncompressed: ', info.file_size, 'bytes')

if __name__ == '__main__':
    print_info(zipfile)


zf = zipfile.ZipFile(zipfile)
for filename in ['README.txt', 'notthere.txt']:
        try:
            info = zf.getinfo(filename)
        except KeyError:
            print('ERROR: Did not find %s in zip file' % filename)
        else:
            print('%s is %d bytes' % (info.filename, info.file_size))

