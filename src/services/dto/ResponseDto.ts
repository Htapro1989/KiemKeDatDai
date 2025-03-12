export interface ResponseDto<T> {
    code: number;
    errorCode: string;
    returnValue: T;
  }
  