package com.kaltura.client.utils.request;

import com.kaltura.client.utils.response.base.GeneralResponse;

/**
 * Created by tehilarozin on 21/08/2016.
 */
public class FileRequest extends ActionBase<Object> {
    @Override
    public void onComplete(GeneralResponse<Object> response) {

    }

    @Override
    public String getUrlTail() {
        return null;
    }

    @Override
    public String getAction() {
        return null;
    }

    /*
    * 
	KalturaFiles files;
public KalturaFiles getFiles() {
        return files;
    }

    public ActionBase setFiles(KalturaFiles files) {
        this.files = files;
        return getThis();
    }
	
	public String serve() throws KalturaAPIException {

		KalturaParams kParams = new KalturaParams();
		String url = extractParamsFromCallQueue(kParams, new KalturaFiles());
		String kParamsString = kParams.toQueryString();
		url += "?" + kParamsString;

		return url;
	}

public KalturaFiles getFilesForMultiRequest(int multiRequestNumber) {

        KalturaFiles multiRequestFiles = new KalturaFiles();
        multiRequestFiles.add(Integer.toString(multiRequestNumber), files);
        return multiRequestFiles;
    }

    private HttpPost getPostMultiPartWithFiles(HttpPost method, KalturaParams kparams, KalturaFiles kfiles) {

		String boundary = "---------------------------" + System.currentTimeMillis();
		List <Part> parts = new ArrayList<Part>();
		parts.add(new StringPart (HttpMethodParams.MULTIPART_BOUNDARY, boundary));

		parts.add(new StringPart ("json", kparams.toString()));

		for (String key : kfiles.keySet()) {
			final KalturaFile kFile = kfiles.get(key);
			parts.add(new StringPart(key, "filename=" + kFile.getName()));
			if (kFile.getFile() != null) {
				// use the file
				File file = kFile.getFile();
				try {
					parts.add(new FilePart(key, file));
				} catch (FileNotFoundException e) {
					// TODO this sort of leaves the submission in a weird
					// state... -AZ
					if (logger.isEnabled())
						logger.error("Exception while iterating over kfiles", e);
				}
			} else {
				// use the input stream
				PartSource fisPS = new PartSource() {
					public long getLength() {
						return kFile.getSize();
					}

					public String getFileName() {
						return kFile.getName();
					}

					public InputStream createInputStream() throws IOException {
						return kFile.getInputStream();
					}
				};
				parts.add(new FilePart(key, fisPS));
			}
		}

		Part allParts[] = new Part[parts.size()];
		allParts = parts.toArray(allParts);

		method.setRequestEntity(new MultipartRequestEntity(allParts, method.getParams()));

		return method;
	}
	*/
}
