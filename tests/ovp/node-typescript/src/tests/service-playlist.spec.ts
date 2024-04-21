import { KalturaClient } from "../kaltura-client-service";
import { PlaylistListAction } from "../api/types/PlaylistListAction";
import { KalturaPlaylistListResponse } from "../api/types/KalturaPlaylistListResponse";
import { KalturaPlaylist } from "../api/types/KalturaPlaylist";
import { KalturaPlaylistType } from "../api/types/KalturaPlaylistType";
import { PlaylistAddAction } from "../api/types/PlaylistAddAction";
import { PlaylistDeleteAction } from "../api/types/PlaylistDeleteAction";
import { PlaylistUpdateAction } from "../api/types/PlaylistUpdateAction";
import { getClient } from "./utils";
import { LoggerSettings, LogLevels } from "../api/kaltura-logger";
import { asyncAssert } from "./utils";

describe(`service "Playlist" tests`, () => {
  let kalturaClient: KalturaClient = null;

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
	  expect.assertions(5);
    kalturaClient.request(new PlaylistListAction()).then(
      (response) => {
	      asyncAssert(() => {
		      expect(response instanceof KalturaPlaylistListResponse).toBeTruthy();
		      expect(response.objects).toBeDefined();
		      expect(response.objects instanceof Array).toBeTruthy();
		      expect(response.objects.length).toBeGreaterThan(0);
		      expect(response.objects[0] instanceof KalturaPlaylist).toBeTruthy();
	      });

        done();
      },
      () => {
        done.fail(`failed to perform request`);
      }
    );
  });

  test(`invoke "createRemote:staticList" action`, (done) => {
    const playlist = new KalturaPlaylist({
      name: "tstest.PlaylistTests.test_createRemote",
      playlistType: KalturaPlaylistType.staticList
    });
	  expect.assertions(2);
    kalturaClient.request(new PlaylistAddAction({ playlist }))
      .then(
        (response) => {
	        asyncAssert(() => {
		        expect(response instanceof KalturaPlaylist).toBeTruthy();
		        expect(typeof response.id).toBe("string");
	        });

	        kalturaClient.request(new PlaylistDeleteAction({ id: response.id })).then(
                () => {
	                done();
                },
                () => {
	                done();
                }
            );
        },
        (error) => {
          done.fail(error);
        }
      );
  });

  test(`invoke "update" action`, (done) => {
    const playlist = new KalturaPlaylist({
      name: "tstest.PlaylistTests.test_createRemote",
      referenceId: "tstest.PlaylistTests.test_update",
      playlistType: KalturaPlaylistType.staticList
    });
	  expect.assertions(1);
    kalturaClient.request(new PlaylistAddAction({ playlist }))
      .then(({ id }) => {
          playlist.name = "Changed!";
          return kalturaClient.request(new PlaylistUpdateAction({ id, playlist }));
        }
      )
      .then(({ id, name }) => {
	      asyncAssert(() => {
		      expect(name).toBe("Changed!");
	      });
            kalturaClient.request(new PlaylistDeleteAction({ id })).then(
	            () => {
		            done();
	            },
	            () => {
		            done();
	            });
      })
      .catch((error) => {
        done.fail(error);
      });
  });

  test(`invoke "createRemote:dynamicList" action`, (done) => {
    const playlist = new KalturaPlaylist({
      name: "tstest.PlaylistTests.test_createRemote",
      playlistType: KalturaPlaylistType.dynamic,
      totalResults: 0
    });
	  expect.assertions(2);
    kalturaClient.request(new PlaylistAddAction({ playlist }))
      .then(
        (response) => {
	        asyncAssert(() => {
		        expect(response instanceof KalturaPlaylist).toBeTruthy();
		        expect(typeof response.id).toBe("string");
	        });
          kalturaClient.request(new PlaylistDeleteAction({ id: response.id })).then(
	          () => {
		          done();
	          },
	          () => {
		          done();
	          }
          );
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
