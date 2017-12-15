export class InvalidQueryError implements Error {
  name: string;
  stack?: string | undefined;
  private _message: string;

  constructor(message: string) {
    this._message = message;
  }

  get message(): string {
    return this._message;
  }
}