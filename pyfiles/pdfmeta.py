import PyPDF2
from PyPDF2 import PdfFileReader
import sys

def impr(fileName):
	pdfFile = PdfFileReader(fileName,'rb')
	meta = pdfFile.getDocumentInfo()
	print('-Document: ' + str(fileName))
	for i in meta:
		print('-' + i[1:] + ': ' + meta[i])

def main():        
        filename = sys.argv[1]
        if filename == None:
                exit(0)
        else:
                impr(filename)

if __name__ == '__main__':
	main()
