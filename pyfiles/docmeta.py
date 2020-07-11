import olefile, os.path, sys

# Check to see if the file name was entered on the command line.
# If it wasn't prompt for the file name
try:
   sys.argv[1]
except IndexError:
   infile = input("Enter file name: ")
else:
   infile = sys.argv[1]

if os.path.isfile(infile):
   # Determine if the first bytes of the file contain the magic number for OLE 
   # files, before opening it. isOleFile returns True if it is an OLE file, 
   # False otherwise 
   if olefile.isOleFile(infile):
      ole = olefile.OleFileIO(infile)
      meta = olefile.OleFileIO.get_metadata(ole)
      print('Author:', meta.author)
      print('Last saved by:', meta.last_saved_by)
      print('Title:', meta.title)
      print('Creation date:', meta.create_time)
      # print all metadata:
      meta.dump()
   else:
      print(infile, "is not an OLE file")