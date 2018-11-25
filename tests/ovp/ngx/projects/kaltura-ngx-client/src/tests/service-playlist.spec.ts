import {PlaylistListAction} from "../lib/api/types/PlaylistListAction";
import {KalturaPlaylistListResponse} from "../lib/api/types/KalturaPlaylistListResponse";
import {KalturaPlaylist} from "../lib/api/types/KalturaPlaylist";
import {KalturaPlaylistType} from "../lib/api/types/KalturaPlaylistType";
import {PlaylistAddAction} from "../lib/api/types/PlaylistAddAction";
import {PlaylistDeleteAction} from "../lib/api/types/PlaylistDeleteAction";
import {PlaylistUpdateAction} from "../lib/api/types/PlaylistUpdateAction";
import { asyncAssert, getClient } from "./utils";
import {LoggerSettings, LogLevels} from "../lib/api/kaltura-logger";
import {KalturaClient} from "../lib/kaltura-client.service";
import { switchMap } from 'rxjs/operators';


describe(`service "Playlist" tests`, () => {
  let kalturaClient: KalturaClient = null;

  beforeAll(async () => {
    LoggerSettings.logLevel = LogLevels.error; // suspend warnings

    return new Promise((resolve => {
      getClient()
        .subscribe(client => {
          kalturaClient = client;
          resolve(client);
        });
    }));
  });

  afterAll(() => {
    kalturaClient = null;
  });

  test(`invoke "list" action`, (done) => {
    expect.assertions(4);
    kalturaClient.request(new PlaylistListAction()).subscribe(
      (response) => {
        asyncAssert(() => {
          expect(response instanceof KalturaPlaylistListResponse).toBeTruthy();
          expect(response.objects).toBeDefined();
          expect(response.objects instanceof Array).toBeTruthy();
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
    kalturaClient.request(new PlaylistAddAction({playlist}))
      .subscribe(
        (response) => {
          kalturaClient.request(new PlaylistDeleteAction({id: response.id})).subscribe(
            () => {
              asyncAssert(() => {
                expect(response instanceof KalturaPlaylist).toBeTruthy();
                expect(typeof response.id).toBe("string");
              });
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
    kalturaClient.request(new PlaylistAddAction({playlist}))
      .pipe(
      switchMap(({id}) => {
          playlist.name = "Changed!";
          return kalturaClient.request(new PlaylistUpdateAction({id, playlist}));
        }
      ),
      switchMap(({id, name}) => {
        asyncAssert(() => {
          expect(name).toBe("Changed!");
        });
        return kalturaClient.request(new PlaylistDeleteAction({id}));
      }))
      .subscribe(() => {
            done();
        },
        error => {
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
    kalturaClient.request(new PlaylistAddAction({playlist}))
      .subscribe(
        (response) => {
          kalturaClient.request(new PlaylistDeleteAction({id: response.id}))
            .subscribe(() => {
              asyncAssert(() => {
                expect(response instanceof KalturaPlaylist).toBeTruthy();
                expect(typeof response.id).toBe("string");
              });
              done();
            });
        },
        (error) => {
          done.fail(error);
        }
      );
  });
});
