import sys
import json
import zipfile
from string import replace
import sys
import io
import requests

_SOLR_HOST = "http://chinkapin.pti.indiana.edu"
_SOLR_PORT = 9994
_QUERY_STUB = "/solr/meta/select/"
_MARC_STUB = "/solr/MARC/"
QUERY_BASEURL = ''.join([_SOLR_HOST, ':', str(_SOLR_PORT), _QUERY_STUB])
MARC_BASEURL = ''.join([_SOLR_HOST, ':', str(_SOLR_PORT), _MARC_STUB])

class SolrAPI(object):
    def query(self, querystring, rows=3, start=0, fields=None):
        """
        Arguments:
            rows: the maximum number of results to return
            fields: an iterable of fields to return with the
                response, eg. fields=['title', 'author']

        Return:
            JSON resource
        """
        querystring = _cleanquery(querystring)

        terms = {}
        terms['q'] = querystring
        terms['rows'] = rows
        terms['start'] = start
        terms['qt'] = 'sharding'
        terms['wt'] = 'json'

        # if fields:
            # terms['fl'] = ','.join(fields)
        r = requests.get(QUERY_BASEURL, params=terms)
        print r.url
        r.raise_for_status()

        return r.json()

    def iterquery(self, querystring, rows=10, fields=[]):
        """ Defines an generator over a query.

            This lets you stick the query in a for loop
            and iterate over all the results, like so:

            >>> for doc in iterquery(<querystring>):
            ...     print json.dumps(doc, indent=4)

            The return docs are python-interpreted json
            structures - the SOLR api spec defines the
            available fields:
            http://wiki.htrc.illinois.edu/display/COM/2.+Solr+API+User+Guide

            For now, errors get passed up from the query
            function...TODO: implement some handling.
        """
        print "world"
        for batch in self.batchquery(querystring, size=rows, fields=fields):
            for doc in batch:
                print "hello"
                print json.dumps(doc, indent=4)
                yield doc

    def batchquery(self, querystring, size=10, fields=[]):
        """ Returns a generator over batches of query results.

        Yields an iterable of len [size] with each next() call.
        """

        num_retrieved = 0
        new_iter = True
        num_found = None

        while True:
            # send a query, then iterate over ['response']['docs']
            result = self.query(querystring, rows=size, start=num_retrieved, fields=fields)

            if new_iter:
                num_found = result['response']['numFound']
                new_iter = False

            if num_found == num_retrieved:
                raise StopIteration

            batch = [doc for doc in result['response']['docs']]
            num_retrieved += len(batch)

            yield batch

    def batch_ids(self, querystring, num=10):
        """ Returns lists of ids for querystring of
            at most length [num]."""

        for batch in self.batchquery(querystring, num):
            yield [doc['id'] for doc in batch]

    def getnumfound(self, querystring):
        """ Return the total number of matches for the query. """
        return int(self.query(querystring, rows=0)['response']['numFound'])

    def getallids(self, querystring):
        """ Return an generator over all the document ids that
            match querystring."""
        for doc in self.iterquery(querystring, fields=['id']):
            yield doc['id']

    def getmarc(self, ids):
        idstring = "|".join(doc_id for doc_id in ids)
        params = {"volumeIDs": idstring}

        r = requests.get(MARC_BASEURL, params=params)
        r.raise_for_status()
        return r.content


def _cleanquery(querystring):
    return replace(querystring, '\'', '\"')


if __name__ == "__main__":
    print _cleanquery("author : 'new zealand'")




solr = SolrAPI()
# results = solr.query("Catcher in the Rye", fields=["title"])
results = solr.query(sys.argv[1], fields=[sys.argv[2]])
print results

with io.open('data.json', 'w', encoding='utf-8') as f:
  f.write(json.dumps(results, ensure_ascii=False))
# with open('searchResult.txt', 'w') as outfile:
#     json.dump(results, outfile)
