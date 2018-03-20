import { KalturaBrowserHttpClient } from "../kaltura-clients/kaltura-browser-http-client";
import { MediaListAction } from "../types/MediaListAction";
import { KalturaMediaListResponse } from "../types/KalturaMediaListResponse";
import { KalturaMediaEntry } from "../types/KalturaMediaEntry";
import { KalturaMediaType } from "../types/KalturaMediaType";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../kaltura-logger";

describe(`service "Media" tests`, () => {
  let kalturaClient: KalturaBrowserHttpClient = null;

  beforeAll(async () => {
    LoggerSettings.logLevel = LogLevels.error; // suspend warnings

    return getClient()
      .then(client => {
        kalturaClient = client;
      }).catch(error => {
          // can do nothing since jasmine will ignore any exceptions thrown from before all
      });
  });

  afterAll(() => {
    kalturaClient = null;
  });

  test(`invoke "list" action`, (done) => {

      if (!kalturaClient)
      {
          fail(`failure during 'SessionStart'. aborting test`);
          return;
      }

    kalturaClient.request(new MediaListAction()).then(
      (response) => {
        expect(response instanceof KalturaMediaListResponse).toBeTruthy();

        expect(response.objects).toBeDefined();
        expect(response.objects instanceof Array).toBeTruthy();

        response.objects.forEach(entry => {
          expect(entry instanceof KalturaMediaEntry).toBeTruthy();
        });

        done();
      },
      () => {
        fail(`failed to perform request`);
        done();
      }
    );
  });

  /*
    def test_createRemote(self):
        mediaEntry = KalturaMediaEntry()
        mediaEntry.setName("pytest.MediaTests.test_createRemote")
        mediaEntry.setMediaType(KalturaMediaType(KalturaMediaType.VIDEO))

        ulFile = getTestFile("DemoVideo.flv")
        uploadTokenId = self.client.media.upload(ulFile)

        mediaEntry = self.client.media.addFromUploadedFile(mediaEntry, uploadTokenId)

        self.assertIsInstance(mediaEntry.getId(), six.text_type)

        #cleanup
        self.client.media.delete(mediaEntry.id)
  */
  xtest(`invoke "createRemote" action`, () => {
    const media = new KalturaMediaEntry({
      name: "typescript.MediaTests.test_createRemote",
      mediaType: KalturaMediaType.video
    });
  });

  describe(`utf-8 tests`, () => {
    /*
      def test_utf8_name(self):
          test_unicode = six.u('\u03dd\xf5\xf6')  #an odd representation of the word 'FOO'
          mediaEntry = KalturaMediaEntry()
          mediaEntry.setName(u'pytest.MediaTests.test_UTF8_name'+test_unicode)
          mediaEntry.setMediaType(KalturaMediaType(KalturaMediaType.VIDEO))
          ulFile = getTestFile('DemoVideo.flv')
          uploadTokenId = self.client.media.upload(ulFile)

          #this will throw an exception if fail.
          mediaEntry = self.client.media.addFromUploadedFile(mediaEntry, uploadTokenId)

          self.addCleanup(self.client.media.delete, mediaEntry.getId())
     */
    xtest(`support utf-8 name`, () => {
      const media = new KalturaMediaEntry({
        name: "typescript.MediaTests.test_UTF8_name" + "\u03dd\xf5\xf6",
        mediaType: KalturaMediaType.video
      });
    });

    /*
      def test_utf8_tags(self):

          test_unicode = u'\u03dd\xf5\xf6'  #an odd representation of the word 'FOO'
          mediaEntry = KalturaMediaEntry()
          mediaEntry.setName('pytest.MediaTests.test_UTF8_tags')
          mediaEntry.setMediaType(KalturaMediaType(KalturaMediaType.VIDEO))
          ulFile = getTestFile('DemoVideo.flv')
          uploadTokenId = self.client.media.upload(ulFile)

          mediaEntry.setTags(test_unicode)

          #this will throw an exception if fail.
          mediaEntry = self.client.media.addFromUploadedFile(mediaEntry, uploadTokenId)

          self.addCleanup(self.client.media.delete, mediaEntry.getId())
     */
    xtest(`support utf-8 tags`, () => {
      const media = new KalturaMediaEntry({
        name: "typescript.MediaTests.test_UTF8_tags",
        mediaType: KalturaMediaType.video
      });
    });
  });
});
