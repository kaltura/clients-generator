export class KalturaAPIException {
  constructor(public message: string, public code: string, public args?: any) {
  }
}
