export class InvalidQueryError implements Error {
  private _name: string;
  private _message: string;

  constructor(name: string, message: string) {
    this._name = name;
    this._message = message;
  }

  get name(): string {
    return this._name;
  }

  get message(): string {
    return this._message;
  }
}