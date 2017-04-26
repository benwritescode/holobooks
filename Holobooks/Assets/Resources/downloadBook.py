import requests
from requests_oauthlib import OAuth1
import sys

_STUB = 'babel.hathitrust.org/cgi/htd/'
DATA_BASEURL = ''.join(['http://', _STUB])
SECURE_DATA_BASEURL = ''.join(['https://', _STUB])

class DataAPI(object):

    def __init__(self, client_key, client_secret, secure=True):
        self.client_key = client_key
        self.client_secret = client_secret
        self.oauth = OAuth1(client_key=client_key,
                            client_secret=client_secret,
                            signature_type='query')

        self.rsession = requests.Session()
        self.rsession.auth = self.oauth

        if secure:
            self.baseurl = SECURE_DATA_BASEURL
        else:
            self.baseurl = DATA_BASEURL

    def _makerequest(self, resource, doc_id, doc_type='volume', sequence=None,
                     v=2, json=False, callback=None):


        url = "".join([self.baseurl, doc_type, '/', resource, '/', doc_id])
        print(url)
        if sequence:
            url += '/' + str(sequence)

        params = {'v': str(v)}
        if json:
            params['format'] = 'json'
            if callback:
                params['callback'] = callback

        r = self.rsession.get(url, params=params)
        r.raise_for_status()

        return r.content

    def getdocumentocr(self, doc_id, start_page=1, end_page=1e5, doc_type='volume'):

        outPages = []
        i = start_page
        while (i <= end_page):
            try:
                outPages.append(self.getpageocr(doc_id, i, doc_type=doc_type))
                i += 1
            except:
                break
        return outPages

    def getdocumentimage(self, doc_id, start_page = 1, end_page = 1e5 , doc_type = 'volume'):
        """  Get image for an entire document.
        Return:
            List of UTF-8 encoded OCR plain text from start_page to end_page (or entire work if no bounds provided)
        """
        outPages = []
        i = start_page
        while (i <= end_page):
            try:
                outPages.append(self.getpageimage(doc_id, i, doc_type=doc_type))
                i += 1
            except:
                break
        return outPages


    def getpageimage(self, doc_id, seq, doc_type='volume'):
        """ Retrieve Single Page Image.
        Return:
            response with tiff, jp2, or jpeg file
        """

        return self._makerequest('pageimage', doc_id, doc_type = doc_type, sequence=seq)


    def getpageocr(self, doc_id, sequence, doc_type='volume'):
        """  Get single-page OCR.
        Return:
            UTF-8 encoded OCR plain text
        """
        return self._makerequest('pageocr', doc_id, doc_type = doc_type, sequence=sequence)





data_api = DataAPI("210c9e0e03","7606c62ba9157d5d66f541b044b0")
# image= data_api.getpageocr('coo.31924069448102', 44)
# print sys.argv[1]
image= data_api.getpageimage(str(sys.argv[1]), 1)
# image= data_api.getpageimage('hvd.32044020104550', 1)
# f1= open("filename","w+")
# f1.write("hello")
# f1.write(str(sys.argv[1]))

# image = data_api.getpageimage('nyp.33433082228226',1)
f = open('333.jpg','wb')
f.write(image)
f.close()
#print image
