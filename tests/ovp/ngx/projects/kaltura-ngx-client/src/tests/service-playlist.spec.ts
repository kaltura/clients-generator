import {PlaylistListAction} from "../api/types/PlaylistListAction";
import {KalturaPlaylistListResponse} from "../api/types/KalturaPlaylistListResponse";
import {KalturaPlaylist} from "../api/types/KalturaPlaylist";
import {KalturaPlaylistType} from "../api/types/KalturaPlaylistType";
import {PlaylistAddAction} from "../api/types/PlaylistAddAction";
import {PlaylistDeleteAction} from "../api/types/PlaylistDeleteAction";
import {PlaylistUpdateAction} from "../api/types/PlaylistUpdateAction";
import {getClient} from "./utils";
import {LoggerSettings, LogLevels} from "../api/kaltura-logger";
import {KalturaClient} from "../kaltura-client.service";
import "rxjs/add/operator/switchMap";

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
    kalturaClient.request(new PlaylistListAction()).subscribe(
      (response) => {
        expect(response instanceof KalturaPlaylistListResponse).toBeTruthy();

        expect(response.objects).toBeDefined();
        expect(response.objects instanceof Array).toBeTruthy();

        response.objects.forEach(entry => {
          expect(entry instanceof KalturaPlaylist).toBeTruthy();
        });

        done();
      },
      () => {
        fail(`failed to perform request`);
        done();
      }
    );
  });

  test(`invoke "createRemote:staticList" action`, (done) => {
    const playlist = new KalturaPlaylist({
      name: "tstest.PlaylistTests.test_createRemote",
      playlistType: KalturaPlaylistType.staticList
    });
    kalturaClient.request(new PlaylistAddAction({playlist}))
      .subscribe(
        (response) => {
          expect(response instanceof KalturaPlaylist).toBeTruthy();
          expect(typeof response.id).toBe("string");
          kalturaClient.request(new PlaylistDeleteAction({id: response.id}));
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
  });

  test(`invoke "update" action`, (done) => {
    const playlist = new KalturaPlaylist({
      name: "tstest.PlaylistTests.test_createRemote",
      referenceId: "tstest.PlaylistTests.test_update",
      playlistType: KalturaPlaylistType.staticList
    });
    kalturaClient.request(new PlaylistAddAction({playlist}))
      .switchMap(({id}) => {
          playlist.name = "Changed!";
          return kalturaClient.request(new PlaylistUpdateAction({id, playlist}));
        }
      )
      .switchMap(({id, name}) => {
        expect(name).toBe("Changed!");
        return kalturaClient.request(new PlaylistDeleteAction({id}));
      })
      .subscribe(() => {
            done();
        },
        error => {
          fail(error);
        });
  });

  test(`invoke "createRemote:dynamicList" action`, (done) => {
    const playlist = new KalturaPlaylist({
      name: "tstest.PlaylistTests.test_createRemote",
      playlistType: KalturaPlaylistType.dynamic,
      totalResults: 0
    });
    kalturaClient.request(new PlaylistAddAction({playlist}))
      .subscribe(
        (response) => {
          expect(response instanceof KalturaPlaylist).toBeTruthy();
          expect(typeof response.id).toBe("string");
          kalturaClient.request(new PlaylistDeleteAction({id: response.id}));
          done();
        },
        (error) => {
          fail(error);
          done();
        }
      );
  });
});
