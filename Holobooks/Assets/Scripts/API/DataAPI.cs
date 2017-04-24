using UnityEngine;
using System.Collections;
using Holobook.WebRequest;
using OAuth;


	public class DataAPI:MonoBehaviour
	{
		public const string url = "https://babel.hathitrust.org/cgi/htd/";
		public string consumer_key;
		public string consumer_secret;
		public string signature;

		public DataAPI(string consumer_key, string consumer_secret, string signature)  {
			this.consumer_key = consumer_key;
			this.consumer_secret = consumer_secret;
			this.signature = signature;
//			OAuth["consumer_key"] = consumer_key;
//			OAuth["consumer_secret"] = consumer_secret;
//			OAuthResponse requestToken = OAuth.AcquireRequestToken(url, "POST");
		}

		private void OnRequestDone(WebRequest request)
		{
			switch (request.GetState())
			{
			case WebRequest.State.DONE:
				Debug.Log("RECIEVE : "+request.www.text);
				break;

			}
		}

		// aggregate
		public void getAggregate(string docID) {
			string _url = url + "aggregate/" + docID + "?v=2";
				// REPLACE
				WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
					{
						OnRequestDone(request);
					});
		}
		// structure
		public void getStructure(string docID, string format = null) {
			string _url = "";
			if (format == null) {
				_url = url + "structure/" + docID + "?format=xml&v=2";
			}
			else {
				_url = url + "structure/" + docID + "?format=" + format + "&v=2";
			}
			// REPLACE
			WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
				{
					OnRequestDone(request);
				});
		}
		// volume
		public void getVolume(string docID) {
			string _url = url + "volume/" + docID + "/" + "format=ebm&v=2";

			// REPLACE
			WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
				{
					OnRequestDone(request);
				});
		}
		// volume/type
		public void makeVolumeRequest(string resource, string docID, string seq = null, string format = null) {
			string _url = url + "volume/";

			if(resource == "meta") {
				if (format == null) {
					_url = _url + "meta/" + docID + "?format=xml&v=2";
				}
				else {
					_url = _url + "meta/" + docID + "?format=" + format + "&v=2";
				}
			}

			if(resource == "pagemeta" && seq != null) {
				if (format == null) {
					_url = _url + "meta/" + docID + "/" + seq + "?format=xml&v=2";
				}
				else {
					_url = _url + "meta/" + docID + "/" + seq +  "?format=" + format + "&v=2";
				}
			}

			if(resource == "pageocr" && seq != null) {
				_url = _url + resource + "/" + docID + "/" + seq + "?v=2";
			}

			if(resource == "pagecoordocr" && seq != null) {
				_url = _url + resource + "/" + docID + "/" + seq + "?v=2";
			}
			Debug.Log(_url);
			// REPLACE
			WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
				{
					OnRequestDone(request);
				});
		}
		// volume/meta
		public void getVolumeMeta(string docID, string format = null) {
			makeVolumeRequest("meta", docID, null, format);
		}
		// volume/pagemeta
		public void getPageMeta(string docID, string seq, string format = null) {
			makeVolumeRequest("pagemeta", docID, seq, format);
		}
		// volume/pageocr
		public void getPageOCR(string docID, string seq) {
			makeVolumeRequest("pageocr",docID,seq, null);
		}
		// volume/pagecoordocr
		public void getPageCoordOCR(string docID, string seq) {
			makeVolumeRequest("pagecoordocr",docID,seq, null);
		}
		// volume/pageimage
		public void getPageImage(string docID, string seq, string format = null, string width = null, string height = null, string res = null, string size = null, string watermark = null) {
			string _url = url + "volume/";

				if(width==null && height==null) {
					if(res!=null) {
						if(watermark!=null) {
							if(format!=null) {
								_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&res=" + res + "&watermark=" + watermark + "&v=2";
							}
							else {
								_url = _url + "pageimage/" + docID + "/" + seq + "?res=" + res + "&watermark=" + watermark + "&v=2";
							}
						}
						else {
							if(format!=null) {
								_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&res=" + res + "&v=2";
							}
							else {
								_url = _url + "pageimage/" + docID + "/" + seq + "?res=" + res + "&v=2";
							}
						}  
					}

					if(size!=null) {
						if(watermark!=null) {
							if(format!=null) {
								_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&size=" + size + "&watermark=" + watermark + "&v=2";
							}
							else {
								_url = _url + "pageimage/" + docID + "/" + seq + "?size=" + size + "&watermark=" + watermark + "&v=2";
							}
						}
						else {
							if(format!=null) {
								_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&size=" + size + "&v=2";
							}
							else {
								_url = _url + "pageimage/" + docID + "/" + seq + "?size=" + size + "&v=2";
							}
						}  
					}

					if(res == null && size == null) {
						if(watermark!=null) {
							if(format!=null) {
								_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&watermark=" + watermark + "&v=2";
							}
							else {
								_url = _url + "pageimage/" + docID + "/" + seq + "&watermark=" + watermark + "&v=2";
							}
						}
						else {
							if(format!=null) {
								_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&v=2";
							}
							else {
								_url = _url + "pageimage/" + docID + "/" + seq + "?v=2";
							}
						}  
					}
				}

				if(width != null && height == null) {
					if(watermark!=null) {
						if(format!=null) {
							_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&width=" + width + "&watermark=" + watermark + "&v=2";
						}
						else {
							_url = _url + "pageimage/" + docID + "/" + seq + "?width=" + width + "&watermark=" + watermark + "&v=2";
						}
					}
					else {
						if(format!=null) {
							_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&width=" + width + "&v=2";
						}
						else {
							_url = _url + "pageimage/" + docID + "/" + seq + "?width=" + width + "&v=2";
						}
					}  
				}

				if(width == null && height != null) {
					if(watermark!=null) {
						if(format!=null) {
							_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&height=" + height + "&watermark=" + watermark + "&v=2";
						}
						else {
							_url = _url + "pageimage/" + docID + "/" + seq + "?height=" + height + "&watermark=" + watermark + "&v=2";
						}
					}
					else {
						if(format!=null) {
							_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&height=" + height + "&v=2";
						}
						else {
							_url = _url + "pageimage/" + docID + "/" + seq + "?height=" + height + "&v=2";
						}
					}  
				}

				if(width != null && height != null) {
					if(watermark!=null) {
						if(format!=null) {
							_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&width=" + width + "&height=" + height + "&watermark=" + watermark + "&v=2";
						}
						else {
							_url = _url + "pageimage/" + docID + "/" + seq + "?width=" + width + "&height=" + height + "&watermark=" + watermark + "&v=2";
						}
					}
					else {
						if(format!=null) {
							_url = _url + "pageimage/" + docID + "/" + seq + "?format=" + format + "&width=" + width + "&height=" + height + "&v=2";
						}
						else {
							_url = _url + "pageimage/" + docID + "/" + seq + "?width=" + width + "&height=" + height + "&v=2";
						}
					}  
				}
					System.Console.WriteLine(_url);
				// REPLACE
				WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
					{
						OnRequestDone(request);
					});

		}
		// /article
		public void getArticle(string docID, string format = null) {
			string _url = url + "article/";

			if (format == null) {
				_url = _url + docID + "?format=xml&v=2";
			}
			else {
				_url = _url + docID + "?format=" + format + "&v=2";
			}
			System.Console.WriteLine(_url);
			// REPLACE
			WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
				{
					OnRequestDone(request);
				});
		}

		public void getArticleAlt(string docID, string seq = null ){
			string _url = url + "article/alternate/";
			//can seq be null?
			if (seq == null) {
				_url = _url + docID + "?v=2";
			}
			else {
				_url = _url + docID + "/" + seq + "?v=2";
			}
			System.Console.WriteLine(_url);
			// REPLACE
			WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
				{
					OnRequestDone(request);
				});
		}

		// article/assets/
		public void getArticleAssets(string docID, string resource, string seq = null) {
			string _url = url + "article/assets/";

			if(resource == "embedded") {
				if (seq == null) {
					_url = _url+"embedded/" + docID + "?v=2";
				}
				else {
					_url = _url + "embedded/" + docID + "/" + seq + "?v=2";
				}
			}

			if(resource == "supplementary") {
				if (seq == null) {
					_url = _url +"supplementary/" + docID + "?v=2";
				}
				else {
					_url = _url + "supplementary/" + docID + "/ASSETIMG_I" + seq + "?v=2";
				}
			}

			System.Console.WriteLine(_url);
			// REPLACE
			WebRequest.Request(_url, 3.5f, this, (WebRequest request) =>
				{
					OnRequestDone(request);
				});
		}

		// article/assets/embedded
		public void getAssetsEmbedded(string docID, string seq = null) {
			getArticleAssets(docID, "embedded", seq);
		}

		// article/assets/supplementary
		public void getAssetsSupplementary(string docID, string seq = null) {
			getArticleAssets(docID, "supplementary", seq);
		}

		// should return type of resource
		

		// Request Token
		 

	}

